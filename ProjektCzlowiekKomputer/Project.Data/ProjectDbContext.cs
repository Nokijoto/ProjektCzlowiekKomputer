using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.Data.Entities;
using Project.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data
{
    public class ProjectDbContext : IdentityDbContext<UserModel>
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BooksAuthors> BooksAuthors { get; set; }
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            BaseConfig(builder);
        }

        public void BaseConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BooksAuthors>()
                .HasKey(ba => new { ba.BookId, ba.AuthorId });

            modelBuilder.Entity<BooksAuthors>()
                .HasOne(ba => ba.Book)
                .WithMany(b => b.BooksAuthors)
                .HasForeignKey(ba => ba.BookId);

            modelBuilder.Entity<BooksAuthors>()
                .HasOne(ba => ba.Author)
                .WithMany(a => a.BooksAuthors)
                .HasForeignKey(ba => ba.AuthorId);


        }
    }
}
