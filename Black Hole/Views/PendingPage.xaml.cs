using Black_Hole.Enums;
using Black_Hole.Helpers;
using Black_Hole.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.IO;

namespace Black_Hole.Views
{
    public sealed partial class PendingPage : Page
    {
        private PendingType _pageContext;
        private MagicWormholeService _magicWormholeService;

        public PendingPage()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // O parâmetro é uma tupla que contém o tipo de espera e o caminho para o arquivo
            if (e.Parameter is Tuple<PendingType, MagicWormholeService> parameter)
            {
                _pageContext = parameter.Item1;
                _magicWormholeService = parameter.Item2;
                LoadPageContext();
            }
        }

        // Altera a page de acordo com o tipo de espera (send ou receive)
        private void LoadPageContext()
        {
            switch (_pageContext)
            {
                case PendingType.SenderWaitingReceiverAcceptance:
                    Title.Text = "Your Transmission Code";
                    FileDescription.Text = $"{_magicWormholeService.FileName} {_magicWormholeService.FileSize}MB";

                    QrOrIcon.Child = new Image
                    {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Width = 200,
                        Height = 200,
                        Stretch = Stretch.Fill,
                        Source = new BitmapImage(new Uri(QrCodeHelper.GenerateQrCode("ExampleCode", Path.GetTempPath() + "BlackHole")))
                    };

                    var stackPanel = new StackPanel()
                    {
                        Margin = new Thickness(0, 12, 0, 0),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Orientation = Orientation.Horizontal,
                        Spacing = 10
                    };

                    stackPanel.Children.Add
                    (
                        new TextBox()
                        {
                            Width = 260,
                            IsReadOnly = true,
                            Text = "test"
                        }
                    );

                    stackPanel.Children.Add
                    (
                        new Button
                        {
                            Width = 35,
                            Height = 35,
                            Padding = new Thickness(0),
                            Content = new FontIcon()
                            {
                                Glyph = "\uE8C8"
                            }
                        }
                    );

                    PageVariableContent.Children.Add(stackPanel);
                    PageVariableContent.Children.Add
                    (
                        new TextBlock()
                        {
                            Margin = new Thickness(0, 12, 0, 0),
                            HorizontalAlignment = HorizontalAlignment.Center,
                            FontSize = 16,
                            Style = (Style)Application.Current.Resources["BodyTextBlockStyle"],
                            Text = "Send the Code to the Receiver."
                        }
                    );

                    var cancelButtonSender = new Button()
                    {
                        Margin = new Thickness(0, 20, 0, 0),
                        Width = 120,
                        Height = 30,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Content = "Cancel"
                    };
                    cancelButtonSender.Click += CancelButton_Click;
                    PageVariableContent.Children.Add(cancelButtonSender);

                    break;

                case PendingType.ReceiverRespondingToConfirmation:
                    Title.Text = "Accept File?";
                    FileDescription.Text = $"{_magicWormholeService.FileName} {_magicWormholeService.FileSize}MB"; // Vai causar uma exceção

                    var acceptButtonReceiver = new Button()
                    {
                        Width = 200,
                        Height = 45,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Content = "Accept",
                        Style = (Style)Application.Current.Resources["AccentButtonStyle"]
                    };
                    acceptButtonReceiver.Click += AcceptButton_Click;
                    PageVariableContent.Children.Add(acceptButtonReceiver);

                    var cancelButtonReceiver = new Button()
                    {
                        Width = 200,
                        Height = 45,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Content = "Cancel"
                    };
                    cancelButtonReceiver.Click += CancelButton_Click;
                    PageVariableContent.Children.Add(cancelButtonReceiver);

                    break;
            }
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            // To Do: Implementar a lógica para aceitar o arquivo
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // To Do: Implementar a lógica para cancelar o arquivo
        }
    }
}
