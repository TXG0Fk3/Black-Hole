using Black_Hole.Enums;
using Black_Hole.Helpers;
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
        private Tuple<PendingType, String> _pageContext;

        public PendingPage()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // O par�metro � uma tupla que cont�m o tipo de espera e o caminho para o arquivo
            if (e.Parameter is Tuple<PendingType, string> parameter)
            {
                _pageContext = parameter;
                ProcessPageContext();
            }
        }

        // Altera a page de acordo com o tipo de espera (send ou receive)
        private void ProcessPageContext()
        {
            string value = _pageContext.Item2;

            switch (_pageContext.Item1)
            {
                case PendingType.SenderWaitingReceiverAcceptance:
                    Title.Text = "Your Transmission Code";
                    FileDescription.Text = "ExampleFile.zip";

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

                    break;

                case PendingType.ReceiverRespondingToConfirmation:
                    Title.Text = "Accept File?";
                    FileDescription.Text = "ExampleFile.zip";
                    
                    PageVariableContent.Children.Add
                    (
                        new Button()
                        {
                            Width = 200,
                            Height = 45,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            Content = "Accept",
                            Style = (Style)Application.Current.Resources["AccentButtonStyle"]
                        }
                    );

                    PageVariableContent.Children.Add
                    (
                        new Button()
                        {
                            Width = 200,
                            Height = 45,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            Content = "Cancel"
                        }
                    );

                    break;
            }
        }
    }
}
