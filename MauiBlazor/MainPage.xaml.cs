using MauiBlazor.Shared.Services;

namespace MauiBlazor;

public partial class MainPage : ContentPage
{
    private readonly Iカード読み取りService _cardReaderService;

    public MainPage(Iカード読み取りService cardReaderService)
    {
        InitializeComponent();

        _cardReaderService = cardReaderService;
        _cardReaderService.StartMonitoring();

    }

}
