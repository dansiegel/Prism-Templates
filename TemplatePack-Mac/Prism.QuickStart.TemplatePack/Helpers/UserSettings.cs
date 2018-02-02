using System;
using System.Runtime.CompilerServices;
using MonoDevelop.Core;

namespace Prism.QuickStart.TemplatePack.Helpers
{
    public class UserSettings
    {
        static Lazy<UserSettings> _lazy = new Lazy<UserSettings>(() => new UserSettings());
        static UserSettings _current;
        public static UserSettings Current =>
            _current ?? (_current = _lazy.Value);

        private UserSettings() { }

        public int MinDroidSDK
        {
            get => GetProperty<int>(21);
            set => SetProperty(value);
        }

        public string AppIdBase
        {
            get
            {
                var result = GetProperty<string>();
                return string.IsNullOrWhiteSpace(result) ? "com.company." : result;
            }
            set => SetProperty(value);
        }

        public string DIContainer
        {
            get => GetProperty("DryIoc", nameof(DIContainer));
            set => SetProperty(value);
        }

        public bool CreateEmptyQuickStart
        {
            get => GetProperty<bool>();
            set => SetProperty(value);
        }

        public bool UseMvvmHelpersLibrary
        {
            get => GetProperty<bool>();
            set => SetProperty(value);
        }

        private T GetProperty<T>([CallerMemberName]string name = null) =>
            GetProperty<T>(default(T), name);

        private T GetProperty<T>(T defaultValue, [CallerMemberName]string name = null) =>
            PropertyService.Get<T>($"QuickStartTemplate_{name}", defaultValue);

        private void SetProperty<T>(T value, [CallerMemberName]string name = null)
        {
            PropertyService.Set($"QuickStartTemplate_{name}", value);
        }
    }
}
