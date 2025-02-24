using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Black_Hole.Services;
using Black_Hole.Views;
using Windows.Graphics;


namespace Black_Hole
{
    public partial class App : Application
    {
        private static Window? _mainWindow;

        public static Window MainWindow
        {
            get => _mainWindow;
        }

        public App()
        {
            this.InitializeComponent();
            Microsoft.Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = "en-US"; // Altera o Idioma padrão da aplicação
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            // Carrega a janela principal
            _mainWindow = new Window
            {
                SystemBackdrop = new MicaBackdrop(),
                ExtendsContentIntoTitleBar = true,
                Title = "Black Hole",
                Content = new MainPage()
            };

            _mainWindow.AppWindow.SetIcon("Assets/AppIcon.ico");

            // Tamanho mínimo da janela
            var win32WindowService = new Win32WindowService(_mainWindow);
            win32WindowService.SetWindowMinMaxSize(new Win32WindowService.POINT() { x = 430, y = 480 });

            // Tamanho inicial da janela
            var scaleFactor = win32WindowService.GetSystemDPI() / 96.0;
            _mainWindow.AppWindow.Resize(new SizeInt32((int)(430 * scaleFactor), (int)(680 * scaleFactor)));

            _mainWindow.Activate();
        }
    }
}
