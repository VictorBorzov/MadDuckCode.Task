namespace StoreChain;

public interface ILogger
{
    public string GetLog();
    public void Log(LogData data);
}

