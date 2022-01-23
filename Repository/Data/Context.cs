using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository.Data
{
    public class Context : DbContext
    {
		public DbSet<Author> Authors { get; set; }
		public DbSet<Book> Books { get; set; }
		public DbSet<BookCopy> BookCopies { get; set; }
		public DbSet<Country> Countries { get; set; }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<Language> Languages { get; set; }
		public DbSet<LiteratureType> LiteratureTypes { get; set; }
		public DbSet<Ownership> Ownerships { get; set; }
		public DbSet<Reader> Readers { get; set; }
		public DbSet<Subscription> Subscriptions { get; set; }

		public Context(DbContextOptions<Context> options) : base(options) 
		{
			Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			foreach (var rel in modelBuilder.Model
				.GetEntityTypes()
				.Where(e => !e.IsOwned())
				.SelectMany(e => e.GetForeignKeys()))
            {
				rel.DeleteBehavior = DeleteBehavior.Restrict;
            }

			ConfigureSchema(modelBuilder);

			base.OnModelCreating(modelBuilder);
		}

		private void ConfigureSchema(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<LiteratureType>().HasData(
				new LiteratureType[]
				{
					new LiteratureType { Id = 1, Name = "Художественная", Price = 100 },
					new LiteratureType { Id = 2, Name = "Научная", Price = 60 },
					new LiteratureType { Id = 3, Name = "Учебная", Price = 40 }
				});

			modelBuilder.Entity<Language>().HasData(
				new Language[]
				{
					new Language { Id = 1, Name = "Русский"},
					new Language { Id = 2, Name = "Английский"},
					new Language { Id = 3, Name = "Французский"},
					new Language { Id = 4, Name = "Немецкий"},
					new Language { Id = 5, Name = "Испанский"},
					new Language { Id = 6, Name = "Итальянский"},
					new Language { Id = 7, Name = "Польский"},
					new Language { Id = 8, Name = "Китайский"},
					new Language { Id = 9, Name = "Японский"}
				});

			modelBuilder.Entity<Country>().HasData(
				new Country[]
				{
					new Country { Id = 1, Name = "Россия"},
					new Country { Id = 2, Name = "Великобритания"},
					new Country { Id = 3, Name = "США"},
					new Country { Id = 4, Name = "Франция"},
					new Country { Id = 5, Name = "Германия"},
					new Country { Id = 6, Name = "Испания"},
					new Country { Id = 7, Name = "Португалия"},
					new Country { Id = 8, Name = "Италия"},
					new Country { Id = 9, Name = "Польша"},
					new Country { Id = 10, Name = "Китай"},
					new Country { Id = 11, Name = "Япония"}
				});

			modelBuilder.Entity<Genre>().HasData(
				new Genre[]
				{
					new Genre { Id = 1, Name = "Роман", TypeId = 1},
					new Genre { Id = 2, Name = "Детектив", TypeId = 1},
					new Genre { Id = 3, Name = "Комедия", TypeId = 1},
					new Genre { Id = 4, Name = "Фентези", TypeId = 1},
					new Genre { Id = 5, Name = "Научная фантастика", TypeId = 1},
					new Genre { Id = 6, Name = "Триллер", TypeId = 1},
					new Genre { Id = 7, Name = "Приключения", TypeId = 1},
					new Genre { Id = 8, Name = "Обзор", TypeId = 2},
					new Genre { Id = 9, Name = "Статья", TypeId = 2},
					new Genre { Id = 10, Name = "Доклад", TypeId = 2},
					new Genre { Id = 11, Name = "Реферат", TypeId = 2},
					new Genre { Id = 12, Name = "Диссертация", TypeId = 2},
					new Genre { Id = 13, Name = "Учебник", TypeId = 3},
					new Genre { Id = 14, Name = "Методическое пособие", TypeId = 3},
					new Genre { Id = 15, Name = "Повесть", TypeId = 1},
					new Genre { Id = 16, Name = "Ужасы", TypeId = 1},
					new Genre { Id = 17, Name = "Поэма", TypeId = 1}
				});

			modelBuilder.Entity<Author>().HasData(
				new Author[]
				{
					new Author { Id = 1, FullName = "Фёдор Михайлович Достоевский", CountryId = 1},
					new Author { Id = 2, FullName = "Лев Николаевич Толстой", CountryId = 1},
					new Author { Id = 3, FullName = "Антон Павлович Чехов", CountryId = 1},
					new Author { Id = 4, FullName = "Агата Кристи", CountryId = 2},
					new Author { Id = 5, FullName = "Чарльз Диккенс", CountryId = 2},
					new Author { Id = 6, FullName = "Рэй Брэдбери", CountryId = 3},
					new Author { Id = 7, FullName = "Марк Твен", CountryId = 3},
					new Author { Id = 8, FullName = "Говард Филлипс Лавкрафт", CountryId = 3},
					new Author { Id = 9, FullName = "Жюль Верн", CountryId = 4},
					new Author { Id = 10, FullName = "Эрих Мария Ремарк", CountryId = 5},
					new Author { Id = 11, FullName = "Данте Алигьери", CountryId = 8},
					new Author { Id = 12, FullName = "Артур Конан Дойл", CountryId = 2},
					new Author { Id = 13, FullName = "Дэвид Роджерс", CountryId = 3},
					new Author { Id = 14, FullName = "Валентин Сергеевич Гутников", CountryId = 1}
				});

			var books = new Book[]
				{
					new Book { Id = 1, Title = "Преступление и наказание", NumberOfPages = 500, AuthorId = 1, OriginalLanguageId = 1, GenreId = 1, Price = 25},
					new Book { Id = 2, Title = "Идиот", NumberOfPages = 200, AuthorId = 1, OriginalLanguageId = 1, GenreId = 1, Price = 15},
					new Book { Id = 3, Title = "Война и мир", NumberOfPages = 10000, AuthorId = 2, OriginalLanguageId = 1, GenreId = 1, Price = 45},
					new Book { Id = 4, Title = "Детство, Отрочество, Юность", NumberOfPages = 600, AuthorId = 2, OriginalLanguageId = 1, GenreId = 15, Price = 15},
					new Book { Id = 5, Title = "Десять нергитят", NumberOfPages = 100, AuthorId = 4, OriginalLanguageId = 2, GenreId = 1, Price = 10},
					new Book { Id = 6, Title = "Пять поросят", NumberOfPages = 400, AuthorId = 4, OriginalLanguageId = 2, GenreId = 1, Price = 10},
					new Book { Id = 7, Title = "Приключения Оливера Твиста", NumberOfPages = 800, AuthorId = 5, OriginalLanguageId = 2, GenreId = 1, Price = 25},
					new Book { Id = 8, Title = "Дэвид Копперфилд", NumberOfPages = 1000, AuthorId = 5, OriginalLanguageId = 2, GenreId = 1, Price = 30},
					new Book { Id = 9, Title = "451 Градус по Фаренгейту", NumberOfPages = 1500, AuthorId = 6, OriginalLanguageId = 2, GenreId = 1, Price = 30},
					new Book { Id = 10, Title = "Приключения Тома Сойера", NumberOfPages = 700, AuthorId = 7, OriginalLanguageId = 2, GenreId = 15, Price = 20},
					new Book { Id = 11, Title = "Зов Ктулху", NumberOfPages = 1000, AuthorId = 8, OriginalLanguageId = 2, GenreId = 16, Price = 40},
					new Book { Id = 12, Title = "Таинственный остров", NumberOfPages = 400, AuthorId = 9, OriginalLanguageId = 3, GenreId = 1, Price = 30},
					new Book { Id = 13, Title = "Двадцать тысяч лье под водой", NumberOfPages = 600, AuthorId = 9, OriginalLanguageId = 3, GenreId = 5, Price = 35},
					new Book { Id = 14, Title = "Три товарища", NumberOfPages = 300, AuthorId = 10, OriginalLanguageId = 4, GenreId = 1, Price = 15},
					new Book { Id = 15, Title = "Божественная комедия", NumberOfPages = 1000, AuthorId = 11, OriginalLanguageId = 6, GenreId = 17, Price = 45},
					new Book { Id = 16, Title = "Алгоритмические основы машинной графики", NumberOfPages = 500, AuthorId = 13, OriginalLanguageId = 2, GenreId = 14, Price = 10},
					new Book { Id = 17, Title = "Интегральная электроника в измерительных устройствах", NumberOfPages = 300, AuthorId = 14, OriginalLanguageId = 1, GenreId = 13, Price = 15}
				};

			modelBuilder.Entity<Book>().HasData(books);

			List<BookCopy> copies = new();
			int i = 1; Random rand = new();
			foreach (var b in books)
            {
				for(int j = 0; j < 3; j++)
                {
					copies.Add(new BookCopy { Id = i++, Available = true, BookId = b.Id, LanguageId = 1 });
					copies.Add(new BookCopy { Id = i++, Available = true, BookId = b.Id, LanguageId = b.OriginalLanguageId });
					copies.Add(new BookCopy { Id = i++, Available = true, BookId = b.Id, LanguageId = rand.Next(1, 10) });
				}
            }

			modelBuilder.Entity<BookCopy>().HasData(copies);
		}
	}
}
