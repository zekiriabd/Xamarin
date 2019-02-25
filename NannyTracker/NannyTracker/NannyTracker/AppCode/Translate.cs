using System;
using System.Resources;
using Xamarin.Forms;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms.Xaml;

namespace NannyTracker
{
    [ContentProperty("Text")]
    public class Translate : IMarkupExtension
    {
        const string ResourceId = "NannyTracker.Resources.AppResource";
        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null) return null;
            ResourceManager resourceManager = new ResourceManager(ResourceId, typeof(Translate).GetTypeInfo().Assembly);
            return resourceManager.GetString(Text, CultureInfo.CurrentCulture);
        }

    }
}
