namespace FTR.Services
{
    public interface IOutput
    {
        void WriteLine(string text);
        string ReadLine();
    }
}