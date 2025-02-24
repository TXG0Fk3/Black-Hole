using Microsoft.UI.Xaml;
using System;
using System.Threading.Tasks;
using Windows.Storage.Pickers;
using WinRT.Interop;

namespace Black_Hole.Helpers
{
    static class FilePickerHelper
    {
        private static void InitializePicker(dynamic picker, Window window)
        {
            picker.FileTypeFilter.Add("*");
            InitializeWithWindow.Initialize(picker, WindowNative.GetWindowHandle(window));
        }

        public async static Task<string?> PickFile(Window window)
        {
            var openPicker = new FileOpenPicker();

            InitializePicker(openPicker, window);

            var file = await openPicker.PickSingleFileAsync();

            return file?.Path;
        }

        public async static Task<string?> PickFolder(Window window)
        {
            var openPicker = new FolderPicker();

            InitializePicker(openPicker, window);

            var folder = await openPicker.PickSingleFolderAsync();

            return folder?.Path;
        }
    }
}
