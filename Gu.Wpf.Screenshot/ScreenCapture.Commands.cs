namespace Gu.Wpf.Screenshot
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Windows.Input;

    public static partial class ScreenCapture
    {
        public static ICommand ShowSnippingToolCommand { get; } = _ShowSippingToolCommand.Default;

        private class _ShowSippingToolCommand : ICommand
        {
            public static readonly ICommand Default = new _ShowSippingToolCommand();

            private readonly ProcessStartInfo startInfo;

            private _ShowSippingToolCommand()
            {
                var fileName = Environment.Is64BitProcess
                    ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "SnippingTool.exe")
                    : @"C:\Windows\sysnative\SnippingTool.exe";

                if (File.Exists(fileName))
                {
                    this.startInfo = new ProcessStartInfo(fileName);
                }
            }

            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter) => this.startInfo != null;

            public void Execute(object parameter)
            {
                if (startInfo != null)
                {
                    Process.Start(startInfo);
                }
            }
        }
    }
}