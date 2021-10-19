namespace WordCount.Core.Services
{
    public interface ITextCounterService
    {
        int CountWords(string textString);
    }
}