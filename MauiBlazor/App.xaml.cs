using MauiBlazor.Shared.Services;
using Plugin.Maui.Audio;

namespace MauiBlazor
{
    public partial class App : Application
    {
        private readonly Iカード読み取りService _cardReaderService;
        private readonly IAudioManager _audioManager;


        public App(Iカード読み取りService cardReaderService, IAudioManager audioManager)
        {
            InitializeComponent();
            _cardReaderService = cardReaderService;
            _audioManager = audioManager;
        }


        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new MainPage(_cardReaderService, _audioManager)) { Title = "MauiBlazor" };
        }
    }
}
