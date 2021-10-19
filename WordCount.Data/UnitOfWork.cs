using System.Threading.Tasks;
using WordCount.Core;
using WordCount.Core.Repositories;
using WordCount.Data.Repositories;

namespace WordCount.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TextDbContext _context;
        private ITextRepository _textRepository;

        public UnitOfWork(TextDbContext context)
        {
            _context = context;
        }

        public ITextRepository Texts => _textRepository = _textRepository ?? new TextRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}