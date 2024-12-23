using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Black_Hole.Services;
using Black_Hole.Views;


namespace Black_Hole
{
    public partial class App : Application
    {
        private static Window? MainWindow;

        public App()
        {
            this.InitializeComponent();
            Microsoft.Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = "en-US"; // Altera o Idioma padrão da aplicação
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            // Carrega a janela principal
            MainWindow = new Window
            {
                SystemBackdrop = new MicaBackdrop(),
                ExtendsContentIntoTitleBar = true,
                Title = "Yukari",
                Content = new MainPage()
            };

            // MainWindow.AppWindow.SetIcon("Assets/AppIcon.ico");

            var win32WindowService = new Win32WindowService(MainWindow);
            win32WindowService.SetWindowMinMaxSize(new Win32WindowService.POINT() { x = 600, y = 500 });

            MainWindow.Activate();
        }
    }
}
