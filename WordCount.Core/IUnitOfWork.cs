using System;
using System.Threading.Tasks;
using WordCount.Core.Repositories;

namespace WordCount.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ITextRepository Texts { get; }
        Task<int> CommitAsync();
    }
}