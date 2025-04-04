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
        public enum PendingType
        {
            SenderWaitingReceiverAcceptance, // Indica que o remetente est� aguardando que o receptor aceite o envio
            ReceiverRespondingToConfirmation // Indica que o receptor est� respondendo a confirma��o (aceitando ou negando)
        }

        private PendingType _pageContext;
        private MagicWormholeService _magicWormholeService;

        public PendingPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // O par�metro � uma tupla que cont�m o tipo de espera e um servi�o
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
                    FileDescription.Text = $"{_magicWormholeService.Name} {_magicWormholeService.Size}MB";

                    QrOrIcon.Child = new Image
                    {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Width = 200,
                        Height = 200,
                        Stretch = Stretch.Fill,
                        Source = new BitmapImage(new Uri(QrCodeHelper.GenerateQrCode(_magicWormholeService.Code, Path.GetTempPath() + "BlackHole")))
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
                            Text = _magicWormholeService.Code
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
                    FileDescription.Text = $"{_magicWormholeService.Name} {_magicWormholeService.Size}MB"; // Vai causar uma exce��o

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
            // To Do: Implementar a l�gica para aceitar o arquivo
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // To Do: Implementar a l�gica para cancelar o arquivo
        }
    }
}
