using PCSC;
using PCSC.Iso7816;
using PCSC.Monitoring;
using System.Diagnostics;

namespace MauiBlazor.Services;

public class CardReaderService
{
    private readonly IContextFactory _contextFactory;
    private readonly SCardMonitor _cardMonitor;

    public CardReaderService(IContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
        _cardMonitor = new SCardMonitor(_contextFactory, SCardScope.System);
        _cardMonitor.CardInserted += CardMonitor_CardInserted;
        _cardMonitor.CardRemoved += CardMonitor_CardRemoved;
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

    private void CardMonitor_CardInserted(object sender, CardStatusEventArgs args)
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
