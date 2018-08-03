using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using Foundation;
using NanyTracker;
using NanyTracker.iOS;

[assembly: ExportRenderer(typeof(SwapScrollView), typeof(SwapListViewRenderer))]
namespace NanyTracker.iOS
{
	[Preserve(AllMembers = true)]
	public class SwapListViewRenderer : ScrollViewRenderer
	{
		public static void Initialize()
		{
		}

		protected override void OnElementChanged(VisualElementChangedEventArgs e)
		{
			base.OnElementChanged(e);

			if (e.NewElement != null)
			{
				ShowsHorizontalScrollIndicator = false;
				ShowsVerticalScrollIndicator = false;

				DraggingEnded -= OnDraggingEnded;
				DraggingStarted -= OnDraggingStarted;

				DraggingEnded += OnDraggingEnded;
				DraggingStarted += OnDraggingStarted;

				DecelerationStarted -= OnDecelerationStarted;
				DecelerationStarted += OnDecelerationStarted;

				DecelerationRate = DecelerationRateFast;

				Bounces = false;
			}
		}


		private void OnDecelerationStarted(object sender, EventArgs e)
		{
			(Element as SwapScrollView)?.OnFlingStarted();
		}

		private void OnDraggingEnded(object sender, EventArgs e)
		{
			(Element as SwapScrollView)?.OnTouchEnded();
		}

		private void OnDraggingStarted(object sender, EventArgs e)
		{
			(Element as SwapScrollView)?.OnTouchStarted();
		}
	}
}
