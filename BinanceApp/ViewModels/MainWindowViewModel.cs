using Avalonia.Threading;
using Binance.Net.Clients;
using Binance.Net.Interfaces;
using CryptoExchange.Net;
using CryptoExchange.Net.CommonObjects;
using Microsoft.CodeAnalysis;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;


namespace BinanceApp.ViewModels
{
  public class MainWindowViewModel : ViewModelBase
  {
    private BinanceClient _binanceClient = new BinanceClient();

    DispatcherTimer timer = new DispatcherTimer();

    private IEnumerable<IBinanceRecentTrade> _Trades;

    public IEnumerable<IBinanceRecentTrade> Trades
    {
      get => _Trades;
      set => this.RaiseAndSetIfChanged( ref _Trades, value );
    }

    private string _Bid;
    public string Bid
    {
      get => _Bid;
      set => this.RaiseAndSetIfChanged( ref _Bid, value );
    }
    private string _Ask;
    public string Ask
    {
      get => _Ask;
      set => this.RaiseAndSetIfChanged( ref _Ask, value );
    }

    public async Task GetData()
    {      
      var trades = await _binanceClient.SpotApi.ExchangeData.GetRecentTradesAsync( "BTCUSDT" );
      Trades = trades.Data;

      var t = await _binanceClient.SpotApi.ExchangeData.GetTickerAsync( "BTCUSDT" );
      Bid = t.Data.BestBidPrice.ToString( "N3" );
      Ask = t.Data.BestAskPrice.ToString( "N3" );

      if( string.IsNullOrWhiteSpace( Bid ) )
      {
        Bid = "00";
      }

      if( string.IsNullOrWhiteSpace( Ask ) )
      {
        Ask = "00";
      }
    }

    public void Start()
    {
      //Dispatcher.UIThread.InvokeAsync( ( Action )delegate
      //{
      //  this.GetData();
      //}, DispatcherPriority.Render );

      timer.Interval = new TimeSpan( 0, 0, 1 );
      timer.Tick += ( a, b ) =>
      {
        this.GetData();
      };

      timer.Start();
    }
  }
}