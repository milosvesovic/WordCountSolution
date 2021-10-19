using System.Collections.Generic;
using WordCount.Core.Models;
using WordCount.Core.Services;

namespace WordCount.Services.Services
{
    public class DataInit : IDataInit
    {
        private IList<Text> _textList;

        public void Init()
        {
            _textList = new List<Text>();

            _textList.Add(new Text
            {
                TextString = "test test test",
                TextWordCount = 3
            });

            _textList.Add(new Text
            {
                TextString = "   space   test ",
                TextWordCount = 2
            });
        }

        public IList<Text> RetrieveData()
        {
            return _textList;
        }

        public void AddData(Text text)
        {
            _textList.Add(text);
        }
    }
}