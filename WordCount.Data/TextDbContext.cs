using Microsoft.EntityFrameworkCore;
using WordCount.Core.Models;
using WordCount.Core.Services;
using WordCount.Data.Configurations;

namespace WordCount.Data
{
    public class TextDbContext : DbContext
    {
        public DbSet<Text> Texts { get; set; }

        public TextDbContext(DbContextOptions<TextDbContext> options, IDataInit dataInit) : base(options)
        {
            if (dataInit.RetrieveData() == null)
                dataInit.Init();
            foreach (var text in dataInit.RetrieveData())
                if (Texts != null) Texts.Add(text);

            SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .ApplyConfiguration(new TextConfiguration());
        }
    }
}