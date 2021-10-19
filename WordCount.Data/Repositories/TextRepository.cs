using WordCount.Core.Models;
using WordCount.Core.Repositories;

namespace WordCount.Data.Repositories
{
    public class TextRepository : Repository<Text>, ITextRepository
    {
        private TextDbContext TextDbContext => Context as TextDbContext;

        public TextRepository(TextDbContext context) : base(context)
        {
        }
    }
}