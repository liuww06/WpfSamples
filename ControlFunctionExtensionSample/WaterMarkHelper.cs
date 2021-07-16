using System.Windows;

namespace ControlFunctionExtensionSample
{
    public class WaterMarkHelper
    {
        public static string GetWaterMark(DependencyObject obj)
        {
            return (string)obj.GetValue(WaterMarkProperty);
        }

        public static void SetWaterMark(DependencyObject obj, string value)
        {
            obj.SetValue(WaterMarkProperty, value);
        }

        public static readonly DependencyProperty WaterMarkProperty =
            DependencyProperty.RegisterAttached("WaterMark", typeof(string), typeof(WaterMarkHelper), new PropertyMetadata(null));


    }
}