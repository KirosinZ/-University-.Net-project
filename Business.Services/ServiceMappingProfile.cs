using AutoMapper;

using Business.Entities;
using Business.Interop.Data;

namespace Business.Services
{
    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            CreateMap<Author, AuthorDto>();
            CreateMap<Book, BookDto>();
            CreateMap<BookCopy, BookCopyDto>();
            CreateMap<Country, CountryDto>();
            CreateMap<Genre, GenreDto>();
            CreateMap<Language, LanguageDto>();
            CreateMap<LiteratureType, LiteratureTypeDto>();
            CreateMap<Ownership, OwnershipDto>();
            CreateMap<Reader, ReaderDto>();
            CreateMap<Subscription, SubscriptionDto>();

            CreateMap<AuthorDto, Author>();
            CreateMap<BookDto, Book>();
            CreateMap<BookCopyDto, BookCopy>();
            CreateMap<CountryDto, Country>();
            CreateMap<GenreDto, Genre>();
            CreateMap<LanguageDto, Language>();
            CreateMap<LiteratureTypeDto, LiteratureType>();
            CreateMap<OwnershipDto, Ownership>();
            CreateMap<ReaderDto, Reader>();
            CreateMap<SubscriptionDto, Subscription>();
        }
    }
}
