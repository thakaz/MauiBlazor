using PCSC;
using PCSC.Iso7816;
using PCSC.Monitoring;
using System.Diagnostics;

using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Alerts;

namespace MauiBlazor.Services;

public class カード読み取りService
{
    private readonly IContextFactory _contextFactory;
    private readonly SCardMonitor _cardMonitor;
    private readonly 打刻Service _打刻Service;
    private readonly 社員Service _社員Service;

    public enum 読み取り時の処理Code
    {
        打刻,
        社員マスタ登録
    }

    public 読み取り時の処理Code 読み取り時の処理 { get; set; } = 読み取り時の処理Code.打刻;
    public int? 社員id { get; set; } = null;

    public カード読み取りService(打刻Service 打刻Service,社員Service 社員Service)
    {
        _contextFactory = ContextFactory.Instance;

        _cardMonitor = new SCardMonitor(_contextFactory, SCardScope.System);
        _cardMonitor.CardInserted += CardMonitor_CardInserted;
        _cardMonitor.CardRemoved += CardMonitor_CardRemoved;
        _打刻Service = 打刻Service;
        _社員Service = 社員Service;

    }

    public void StartMonitoring()
    {
        using var context = _contextFactory.Establish(SCardScope.System);
        var readerNames = context.GetReaders();
        if (readerNames.Length == 0)
        {
            Debug.WriteLine("リーダーが見つかりません。");
            return;
        }
        foreach (var readerName in readerNames)
        {
            Debug.WriteLine(readerName);
            _cardMonitor.Start(readerName);
        }
    }

    private async void CardMonitor_CardInserted(object sender, CardStatusEventArgs args)
    {
        //カードをかざした時の処理
        try
        {
            using var context = _contextFactory.Establish(SCardScope.System);
            using (var reader = context.ConnectReader(args.ReaderName, SCardShareMode.Shared, SCardProtocol.Any))
            {
                //カードの情報を取得
                byte[] atr = reader.GetAttrib(SCardAttribute.AtrString);
                Debug.WriteLine("ATR: {0}", BitConverter.ToString(atr));
            }
            //カードのシリアル番号(UID)を取得。UIDはカードによって取得方法が異なるが気にしない。
            using var isoReader = new IsoReader(context, args.ReaderName, SCardShareMode.Shared, SCardProtocol.Any);
            var apdu = new CommandApdu(IsoCase.Case2Short, isoReader.ActiveProtocol)
            {
                CLA = 0xFF,
                Instruction = InstructionCode.GetData,
                P1 = 0x00,
                P2 = 0x00,
                Le = 0x08
            };

            var response = isoReader.Transmit(apdu);
            Debug.WriteLine("SW1 SW2 = {0:X2} {1:X2}", response.SW1, response.SW2);
            Debug.WriteLine(BitConverter.ToString(response.GetData()));

            //IDm
            var idm = BitConverter.ToString(response.GetData());

            //ここで取得したIDmを使って何か処理をする。
            //通常は打刻。社員マスタでICの登録となっている時だけ別の処理

            if (読み取り時の処理 == 読み取り時の処理Code.社員マスタ登録)
            {
                if(社員id == null)
                {
                    Debug.WriteLine("社員IDが指定されていません。");
                    return;
                }
                await _社員Service.カードの登録(idm,社員id??0);

            }
            else
            {
                await _打刻Service.打刻byIDm(idm);
            }

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    private void CardMonitor_CardRemoved(object sender, CardStatusEventArgs args)
    {
        //カードを取り出した時の処理
        Debug.WriteLine("取り外した。");
    }
}
