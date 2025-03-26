using Black_Hole.Services;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Media.Animation;
using System;
using System.Linq;


namespace Black_Hole.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            NavigationService.Instance.Initialize(ContentFrame);
            NavigationService.Instance.NavigateTo(typeof(SendPage), null);

            NavigationService.Instance.NavigateTo(typeof(PendingPage),
                new Tuple<PendingPage.PendingType, MagicWormholeService>
                (PendingPage.PendingType.SenderWaitingReceiverAcceptance, new MagicWormholeService("a")));
        }

        private void ToggleButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            ToggleButton newSelectedToggleButton = sender as ToggleButton;

            ToggleButton[] footerNavigationToggleButtons = (newSelectedToggleButton.Parent as Grid).Children
                .OfType<ToggleButton>()
                .ToArray();

            ToggleButton? previousSelectedNavigationToggleButton = footerNavigationToggleButtons
                .FirstOrDefault(TB => TB.IsChecked == true && TB != newSelectedToggleButton);

            if (previousSelectedNavigationToggleButton != null) // Se for NULL significa que o usu�rio s� selecionou um bot�o que j� estava selecionado
            {
                NavigationTransitionInfo transitionInfo = new SlideNavigationTransitionInfo()
                {
                    Effect = Array.IndexOf(footerNavigationToggleButtons, newSelectedToggleButton)
                        < Array.IndexOf(footerNavigationToggleButtons, previousSelectedNavigationToggleButton)
                        ? SlideNavigationTransitionEffect.FromLeft
                        : SlideNavigationTransitionEffect.FromRight
                };

                previousSelectedNavigationToggleButton.IsChecked = false;

                NavigationService.Instance.NavigateTo(Type.GetType(newSelectedToggleButton.Tag.ToString()), null, transitionInfo);
            }

            newSelectedToggleButton.IsChecked = true;
        }
    }
}
