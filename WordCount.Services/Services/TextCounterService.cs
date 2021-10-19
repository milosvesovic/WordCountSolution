using System;
using System.Linq;
using WordCount.Core.Services;

namespace WordCount.Services.Services
{
    public class TextCounterService : ITextCounterService
    {
        /**
         * First we trim textString, then we split by space character and
         * count every non-empty word)
         */
        public int CountWords(string textString)
        {
            var trimmedText = textString.Trim();
            var lines = trimmedText.Split(Environment.NewLine.ToCharArray());
            return lines.Sum(line => line.Split(' ').Count(av => av != ""));
        }
    }
}