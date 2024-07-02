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
        public DbSet<Shelves> Shelves { get; set; }
        public DbSet<BookShelves> BookShelves { get; set; }

        public DbSet<UserShelves> UserShelves { get; set; }

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

            modelBuilder.Entity<BookShelves>()
                .HasKey(bs => new { bs.BookId, bs.ShelvesId });
            modelBuilder.Entity<BookShelves>()
                .HasOne(bs => bs.Book)
                .WithMany(b => b.BookShelves)
                .HasForeignKey(bs => bs.BookId);
            modelBuilder.Entity<BookShelves>()
                .HasOne(bs => bs.Shelves)
                .WithMany(s => s.BookShelves)
                .HasForeignKey(bs => bs.ShelvesId);

            modelBuilder.Entity<UserShelves>()
             .HasOne(us => us.User)
             .WithMany(u => u.UserShelves)
             .HasForeignKey(us => us.UserGuid)
             .HasPrincipalKey(u => u.UserGuid);

            modelBuilder.Entity<UserShelves>()
                .HasOne(us => us.Shelves)
                .WithMany(s => s.UserShelves)
                .HasForeignKey(us => us.ShelvesGuid)
                .HasPrincipalKey(s => s.Guid);


        }
    }
}
