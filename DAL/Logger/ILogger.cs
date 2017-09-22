namespace DAL.Logger
{
    public interface ILogger
    {
        void Trace(string message);
        void Debug(string message);
        void Info(string messge);
        void Warn(string message);
        void Error(string message);
        void Fatal(string message);
    }
}