using Black_Hole.Helpers;
using Black_Hole.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;


namespace Black_Hole.Views
{
    public sealed partial class SendPage : Page
    {
        public SendPage()
        {
            this.InitializeComponent();
        }

        private async void FileButton_Click(object sender, RoutedEventArgs e)
        {
            HandlePathSelection(await FilePickerHelper.PickFile(App.MainWindow), ProgressPage.ProgressType.LoadingSendInfo);
        }

        private async void FolderButton_Click(object sender, RoutedEventArgs e)
        {
            HandlePathSelection(await FilePickerHelper.PickFolder(App.MainWindow), ProgressPage.ProgressType.LoadingSendFolderInfo);
        }

        private void HandlePathSelection(string? path, ProgressPage.ProgressType progressType)
        {
            if (path != null)
            {
                MagicWormholeService magicWormholeService = new(path: path);
                NavigationService.Instance.NavigateTo(
                    typeof(ProgressPage),
                    new Tuple<ProgressPage.ProgressType, MagicWormholeService>(progressType, magicWormholeService));
            }
        }
    }
}
