using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using System;
using System.Linq;


namespace Black_Hole.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            ContentFrame.Navigate(typeof(SendPage), null);
        }

        private void ToggleButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            ToggleButton TB_Sender = sender as ToggleButton;
            TB_Sender.IsChecked = true;

            foreach (ToggleButton i in Footer.Children.OfType<ToggleButton>())
            {
                if (i != TB_Sender)
                {
                    i.IsChecked = false;
                }
            }

            ContentFrame.Navigate(Type.GetType(TB_Sender.Tag.ToString()), null);
        }
    }
}
