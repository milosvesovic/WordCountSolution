using System.Collections.Generic;
using System.Threading.Tasks;
using WordCount.Core.Resources;

namespace WordCount.Core.Services
{
    public interface ITextService
    {
        Task<IEnumerable<TextResource>> GetAllTexts();
        // Task<Text> GetTextById(int id);
        Task<TextResource> CreateTextTyping(SaveTextResource saveTextResource);
        Task<TextResource> CreateTextDb(int textId);

        Task<TextResource> CreateTextFile(string path);
        Task UpdateText(int id, SaveTextResource text);
        Task DeleteText(int id);
    }
}
