using System;
using Xwt;

namespace Prism.QuickStart.TemplatePack.Widgets
{
    public static class WidgetHelpers
    {
        public static void DisposeElement(ref CheckBox cb, EventHandler e)
        {
            if (cb != null)
            {
                cb.Clicked -= e;
                cb.Dispose();
                cb = null;
            }
        }

        public static void DisposeElement(ref ComboBox cb, EventHandler e)
        {
            if (cb != null)
            {
                cb.SelectionChanged -= e;
                cb.Dispose();
                cb = null;
            }
        }

        public static void DisposeElement(ref TextEntry entry, EventHandler<TextInputEventArgs> e)
        {
            if (entry != null)
            {
                entry.TextInput -= e;
                entry.Dispose();
                entry = null;
            }
        }
    }
}
