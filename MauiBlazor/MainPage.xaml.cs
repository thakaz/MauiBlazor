using Microsoft.EntityFrameworkCore.Internal;
using PCSC;
using MauiBlazor.Services;
using System.Diagnostics;

namespace MauiBlazor;

public partial class MainPage : ContentPage
{
    private readonly CardReaderService _cardReaderService;

    public MainPage()
    {
        InitializeComponent();

        var contextFactory = ContextFactory.Instance;
        _cardReaderService = new CardReaderService(contextFactory);
        _cardReaderService.StartMonitoring();
    }
}
