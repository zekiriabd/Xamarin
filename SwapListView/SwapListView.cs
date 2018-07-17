using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NanyTracker
{
	public enum Scrolling { Begin, End }
	public enum ScrollingState { IsEnd, IsBegin, IsMoving }

	public class SwapScrollView : ScrollView
	{
		private View _contentView;
		private View _contextView = new ContentView { WidthRequest = 1 };

        public event Action StartedAction;
        public event Action ShowMenuAction;
        public StackLayout ViewStack { get; }
		public Scrolling SwapDirection { get; private set; }
		public ScrollingState SwapState { get; private set; }
		public bool HasBeenAccelerated { get; private set; }
		public double PrevScrollX { get; private set; }
		public bool IsOpenDirection => SwapDirection == Scrolling.End;
		public bool IsDirectionAndStateSame => (int)SwapDirection == (int)SwapState;
		public SwapScrollView()
		{
			Orientation = ScrollOrientation.Horizontal;
			VerticalOptions = LayoutOptions.Fill;
			HorizontalOptions = LayoutOptions.Fill;

			Content = ViewStack = new StackLayout
			{
				VerticalOptions = LayoutOptions.Fill,
				HorizontalOptions = LayoutOptions.Fill,
				Spacing = 0,
				Orientation = StackOrientation.Horizontal,
				Children = { _contextView }
			};

			Scrolled += OnScrolled;
		}

		public View ContentView
		{
			get => _contentView;
			set
			{
				if (value != _contentView)
				{
					if (_contentView != null)
					{
						ViewStack.Children.Remove(_contentView);
					}
					if (value != null)
					{
						ViewStack.Children.Insert(0, value);
					}
				}
				_contentView = value;
			}
		}
		public View ContextView
		{
			get => _contextView;
			set
			{
				value = value ?? new ContentView { WidthRequest = 1 };
				if (value != _contextView)
				{
					value.IsVisible = false;
					ViewStack.Children.Remove(_contextView);
					ViewStack.Children.Add(value);
					value.IsVisible = true;
				}
				_contextView = value;
			}
		}
		public bool IsClosed => SwapState == ScrollingState.IsEnd;
		public async void HideSwapMenu(SwapScrollView view, bool animated)
		{
			if (view == null) { return; }
			try
			{
				if (view.ScrollX > 0)
				{
					var task = view.ScrollToAsync(0, 0, animated);
					if (view == null)
					{
						var completionSource = new TaskCompletionSource<bool>();
						Device.BeginInvokeOnMainThread(async () =>
						{
							await task;
							completionSource.SetResult(true);
						});
						await completionSource.Task;
						return;
					}
					await task;
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
		}

		public virtual void OnTouchStarted()
		{
			StartedAction?.Invoke();
			HasBeenAccelerated = false;
		}

		public virtual async void OnTouchEnded()
		{
			if (Device.RuntimePlatform == Device.Android)
			{
				await Task.Delay(10);
			}

			if (HasBeenAccelerated)
			{
				return;
			}

			var width = GetContextViewWidth();
			var isOpen = IsOpenDirection
						? ScrollX > GetMovingWidth(width)
						: ScrollX > width - GetMovingWidth(width);
			await ScrollToAsync(isOpen ? width : 0, 0, true);

			if (!HasBeenAccelerated && CheckIsOpen())
			{
				ShowMenuAction?.Invoke();
			}
		}

		public virtual async Task OnFlingStarted(bool needScroll = true, bool animated = true, bool inMainThread = false)
		{
			HasBeenAccelerated = true;
			if (needScroll)
			{
				ShowMenuAction?.Invoke();
				var task = ScrollToAsync(IsOpenDirection ? GetContextViewWidth() : 0, 0, animated);
				if (inMainThread)
				{
					var completionSource = new TaskCompletionSource<bool>();
					Device.BeginInvokeOnMainThread(async () =>
					{
						await task;
						completionSource.SetResult(true);
					});
					await completionSource.Task;
					return;
				}
				await task;
			}
		}

		public async Task MoveSideMenu(bool isOpen = false, bool animated = true)
		{
			SwapDirection = isOpen ? Scrolling.End : Scrolling.Begin;
			await OnFlingStarted(!IsDirectionAndStateSame, animated, true);
		}

		protected async void OnMoveActionInvoked(bool isOpen = false)
		{
			await MoveSideMenu(isOpen, true);
		}

		protected virtual void OnScrolled(object sender, ScrolledEventArgs args)
		{
			SwapDirection = Math.Abs(PrevScrollX - ScrollX) < double.Epsilon ? SwapDirection : PrevScrollX > ScrollX ? Scrolling.Begin : Scrolling.End;
			PrevScrollX = ScrollX;
			CheckScrollState();
		}

		protected virtual void CheckScrollState()
		{
			if (Math.Abs(ScrollX) <= double.Epsilon)
			{
				SwapState = ScrollingState.IsEnd;
			}
			else if (Math.Abs(ScrollX - GetContextViewWidth()) <= double.Epsilon)
			{
				SwapState = ScrollingState.IsBegin;
			}
			else
			{
				SwapState = ScrollingState.IsMoving;
			}
		}

		protected virtual double GetContextViewWidth() => ContextView.Width;

		protected virtual double GetMovingWidth(double contextWidth) => contextWidth * 0.3;

		protected bool CheckIsOpen()
		{
			var width = GetContextViewWidth();
			return IsOpenDirection ? ScrollX > GetMovingWidth(width) : ScrollX > width - GetMovingWidth(width);
		}
	}

    public class SwapViewCell : ViewCell
    {
        public static readonly BindableProperty ContentProperty = BindableProperty.Create(nameof(Content), typeof(View), typeof(SwapViewCell), null, propertyChanged: (bindable, oldValue, newValue) =>
        {
            (bindable as SwapViewCell).SetContentView(newValue as View);
        });

        public static readonly BindableProperty MenuProperty = BindableProperty.Create(nameof(Menu), typeof(DataTemplate), typeof(SwapViewCell), null, propertyChanged: (bindable, oldValue, newValue) =>
        {
            (bindable as SwapViewCell).IsContextChanged = true;
        });

        public event Action<SwapViewCell> ContextMenuOpened;

        public event Action<SwapViewCell> TouchStarted;

        private bool IsContextChanged { get; set; }

        public View Content
        {
            get => GetValue(ContentProperty) as View;
            set => SetValue(ContentProperty, value);
        }

        public DataTemplate Menu
        {
            get => GetValue(MenuProperty) as DataTemplate;
            set => SetValue(MenuProperty, value);
        }

        protected SwapScrollView Scroll { get; } = new SwapScrollView();

        public SwapViewCell()
        {
            View = Scroll;
        }

        public void ForceClose(bool animated = true) => Scroll.HideSwapMenu(Scroll, animated);
        protected void SetContentView(View content)  => (View as SwapScrollView).ContentView = content;
        protected void SetContextView(View context)  => (View as SwapScrollView).ContextView = context;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (View is SwapScrollView sideContextBar)
            {
                sideContextBar.StartedAction += OnTouchStarted;
                sideContextBar.ShowMenuAction += OnContextMenuOpened;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (View is SwapScrollView sideContextBar)
            {
                sideContextBar.StartedAction -= OnTouchStarted;
                sideContextBar.ShowMenuAction -= OnContextMenuOpened;
            }
        }

        protected override void OnBindingContextChanged()
        {
            IsContextChanged = true;
            ForceClose(false);
            base.OnBindingContextChanged();
        }

        private void OnTouchStarted()
        {
            TouchStarted?.Invoke(this);
            if (IsContextChanged)
            {
                IsContextChanged = false;
                var template = Menu is DataTemplateSelector selector ? selector.SelectTemplate(BindingContext, this) : Menu;
                if (template == null) { return; }
                SetContextView(template.CreateContent() as View);
            }
        }

        private void OnContextMenuOpened() => ContextMenuOpened?.Invoke(this);
    }
}
