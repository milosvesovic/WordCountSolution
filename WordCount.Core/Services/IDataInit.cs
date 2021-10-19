using System.Collections.Generic;
using WordCount.Core.Models;

namespace WordCount.Core.Services
{
    public interface IDataInit
    {
        void Init();
        IList<Text> RetrieveData();
        void AddData(Text text);
    }
}