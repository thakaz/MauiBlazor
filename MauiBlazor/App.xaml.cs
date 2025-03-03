using MauiBlazor.Shared.Services;

namespace MauiBlazor
{
    public partial class App : Application
    {
        private readonly Iカード読み取りService _cardReaderService;

        public App(Iカード読み取りService cardReaderService)
        {
            InitializeComponent();
            _cardReaderService = cardReaderService;
        }


        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new MainPage(_cardReaderService)) { Title = "MauiBlazor" };
        }
    }
}
