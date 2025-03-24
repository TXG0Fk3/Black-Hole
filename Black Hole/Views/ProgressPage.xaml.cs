using Black_Hole.Services;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;

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
                LoadPageContext();
            }
        }

        private void LoadPageContext()
        {
            switch (_pageContext)
            {
                case ProgressType.LoadingSendInfo:
                    // TO-DO
                    break;

                case ProgressType.LoadingSendFolderInfo:
                    // TO-DO
                    break;
                
                case ProgressType.LoadingReceiveInfo:
                    // TO-DO
                    break;

                case ProgressType.SendingFile:
                    // TO-DO
                    break;

                case ProgressType.ReceivingFile:
                    // TO-DO
                    break;
            }
        }
    }
}
