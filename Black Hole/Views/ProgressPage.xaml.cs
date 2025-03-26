using Black_Hole.Services;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Black_Hole.Views
{
    public sealed partial class ProgressPage : Page
    {
        public enum ProgressType
        {
            LoadingSendInfo,
            LoadingSendFolderInfo,
            LoadingReceiveInfo,
            SendingFile,
            ReceivingFile
        }

        private ProgressType _pageContext;
        private MagicWormholeService _magicWormholeService;

        public ProgressPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // O parâmetro é uma tupla que contém o tipo de progresso e um serviço
            if (e.Parameter is Tuple<ProgressType, MagicWormholeService> parameter)
            {
                _pageContext = parameter.Item1;
                _magicWormholeService = parameter.Item2;
                _ = LoadPageContextAsync();
            }
        }

        private async Task LoadPageContextAsync()
        {
            Debug.WriteLine("LoadPageContextAsync started");
            switch (_pageContext)
            {
                case ProgressType.LoadingSendInfo:
                    Title.Text = "Loading File Info";

                    _magicWormholeService.LoadSendInfo();
                    await Task.Delay(1000); // TO-DO: Trocar por algo melhor (sem esse delay o Navigate seguinte não funciona);

                    NavigateToPendingPage(PendingPage.PendingType.SenderWaitingReceiverAcceptance);
                    break;

                case ProgressType.LoadingSendFolderInfo:
                    Icon.Glyph = "\uF012";
                    Title.Text = "Compressing Folder";

                    await _magicWormholeService.LoadSendFolderInfo();

                    NavigateToPendingPage(PendingPage.PendingType.SenderWaitingReceiverAcceptance);
                    break;

                case ProgressType.LoadingReceiveInfo:
                    Title.Text = "Loading File Info";

                    _magicWormholeService.LoadReceiveInfo();

                    NavigateToPendingPage(PendingPage.PendingType.ReceiverRespondingToConfirmation);
                    break;

                case ProgressType.SendingFile:
                    Title.Text = "Sending File";
                    break;

                case ProgressType.ReceivingFile:
                    Title.Text = "Receiving File";
                    break;
            }
            Debug.WriteLine("LoadPageContextAsync ended");
        }

        private void NavigateToPendingPage(PendingPage.PendingType pendingType)
        {
            NavigationService.Instance.NavigateTo(typeof(PendingPage),
                new Tuple<PendingPage.PendingType, MagicWormholeService>(pendingType, _magicWormholeService));
        }
    }
}
