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
            HandlePathSelection(await FilePickerHelper.PickFile(App.MainWindow));
        }

        private async void FolderButton_Click(object sender, RoutedEventArgs e)
        {
            HandlePathSelection(await FilePickerHelper.PickFolder(App.MainWindow));
        }

        private void HandlePathSelection(string? Path)
        {
            if (Path != null)
            {
                MagicWormholeService magicWormholeService = new(Path);
                NavigationService.Instance.NavigateTo(
                    typeof(PendingPage),
                    new Tuple<PendingPage.PendingType, MagicWormholeService>(PendingPage.PendingType.SenderWaitingReceiverAcceptance, magicWormholeService));
            }
        }
    }
}
