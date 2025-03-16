using MauiBlazor.Shared.Services;
using Plugin.Maui.Audio;

namespace MauiBlazor;

public partial class MainPage : ContentPage
{
    private readonly Iカード読み取りService _cardReaderService;
    private readonly IAudioManager _audioManager;


    public MainPage(Iカード読み取りService cardReaderService, IAudioManager audioManager)
    {
        InitializeComponent();

        _cardReaderService = cardReaderService;
        _cardReaderService.StartMonitoring();

        _audioManager = audioManager;

    }

}
