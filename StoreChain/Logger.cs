namespace StoreChain;

public class Logger : ILogger
{
    private readonly List<string> _logs = new();

    public string GetLog()
    {
        return string.Join("\n", _logs);
    }

    public void Log(LogData data)
    {
        _logs.Add(
            $"{nameof(data.StoreType)}:{data.StoreType}, {nameof(data.ProductType)}:{data.ProductType}, {nameof(data.ProductPrice)}:{data.ProductPrice}, {nameof(data.AmountBeforePurchase)}:{data.AmountBeforePurchase}, {nameof(data.AmountAfterPurchase)}:{data.AmountAfterPurchase}, {nameof(data.BillDateTime)}:{data.BillDateTime}");
    }
}

public struct LogData
{
    public DateTime BillDateTime { get; init; }

    public int AmountAfterPurchase { get; init; }

    public int AmountBeforePurchase { get; init; }

    public float ProductPrice { get; init; }

    public Type ProductType { get; init; }

    public Type StoreType { get; init; }
}
