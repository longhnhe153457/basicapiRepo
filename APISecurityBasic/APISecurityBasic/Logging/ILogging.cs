using Serilog;

namespace APISecurityBasic.Logging
{
    public interface ILogging
    {
        public void Log(string message, string type);
    }
}
