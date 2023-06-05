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

    public IEnumerable<IBinanceRecentTrade> Trades { get; set; }

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
      Bid = t.Data.BestBidPrice.ToString();
      Ask = t.Data.BestAskPrice.ToString();

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
      timer.Interval = new TimeSpan( 0, 0, 5 );
      timer.Tick += ( a, b ) =>
      {
        this.GetData();
      };

      timer.Start();
    }
  }
}