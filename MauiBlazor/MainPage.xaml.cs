using Microsoft.EntityFrameworkCore.Internal;
using PCSC;
using MauiBlazor.Services;
using System.Diagnostics;

namespace MauiBlazor;

public partial class MainPage : ContentPage
{
    private readonly カード読み取りService _cardReaderService;

    public MainPage(カード読み取りService cardReaderService)
    {
        InitializeComponent();

        _cardReaderService = cardReaderService;
        _cardReaderService.StartMonitoring();
    }
}
