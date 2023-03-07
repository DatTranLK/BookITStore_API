using Entity.Dtos.Account;
using AutoMapper;
using Entity.Dtos.BookImage;
using Entity.Dtos.Book;
using Entity.Dtos.Category;
using Entity.Dtos.EBook;
using Entity.Dtos.Order;
using Entity.Dtos.OrderDetail;
using Entity.Dtos.Publisher;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Dtos.ComboBookDTO;
using Entity.Dtos.DetailComboBookDTO;

namespace Service.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Account, AccountDto>().ReverseMap();
            CreateMap<Publisher, PublisherDto>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailDto>()
                .ForMember(dto => dto.BookName, act => act.MapFrom(obj => obj.Book.Name))
                .ForMember(dto => dto.PriceBook, act => act.MapFrom(obj => obj.Book.Price))
                .ForMember(dto => dto.EBookName, act => act.MapFrom(obj => obj.Ebook.Book.Name))
                .ForMember(dto => dto.PriceEBook, act => act.MapFrom(obj => obj.Ebook.Price))
                .ForMember(dto => dto.ComboBookName, act => act.MapFrom(obj => obj.ComboBook.Name))
                .ForMember(dto => dto.PriceCombo, act => act.MapFrom(obj => obj.ComboBook.PriceReduction));
            CreateMap<BookImage, BookImageDto>().
                ForMember(dto => dto.BookName, act => act.MapFrom(obj => obj.Book.Name));
            CreateMap<Book, BookDto>()
                .ForMember(dto => dto.CategoryName, act => act.MapFrom(obj => obj.Category.Name))
                .ForMember(dto => dto.PublisherName, act => act.MapFrom(obj => obj.Publisher.Name));
            CreateMap<Book, BookDtoForAdmin>()
                .ForMember(dto => dto.CategoryName, act => act.MapFrom(obj => obj.Category.Name))
                .ForMember(dto => dto.PublisherName, act => act.MapFrom(obj => obj.Publisher.Name));
            CreateMap<Ebook, EBookDto>()
                .ForMember(dto => dto.Name, act => act.MapFrom(obj => obj.Book.Name))
                .ForMember(dto => dto.Isbn, act => act.MapFrom(obj => obj.Book.Isbn))
                .ForMember(dto => dto.Author, act => act.MapFrom(obj => obj.Book.Author))
                .ForMember(dto => dto.ReleaseYear, act => act.MapFrom(obj => obj.Book.ReleaseYear))
                .ForMember(dto => dto.Version, act => act.MapFrom(obj => obj.Book.Version))
                .ForMember(dto => dto.Description, act => act.MapFrom(obj => obj.Book.Description))
                .ForMember(dto => dto.AmountSold, act => act.MapFrom(obj => obj.Book.AmountSold))
                .ForMember(dto => dto.IsActive, act => act.MapFrom(obj => obj.Book.IsActive))
                .ForMember(dto => dto.CategoryId, act => act.MapFrom(obj => obj.Book.CategoryId))
                .ForMember(dto => dto.PublisherId, act => act.MapFrom(obj => obj.Book.PublisherId))
                .ReverseMap();
            CreateMap<Ebook, EBookDtoForUpdate>().ReverseMap();
            CreateMap<Book, BookDtoForPhysicalAndEBook>()
                .ForMember(dto => dto.EBookPrice, act => act.MapFrom(obj => obj.Ebook.Price))
                .ForMember(dto => dto.PdfUrl, act => act.MapFrom(obj => obj.Ebook.PdfUrl))
                .ReverseMap();
            CreateMap<Book, EBookDtoForAdmin>()
                .ForMember(dto => dto.PriceEbook, act => act.MapFrom(obj => obj.Ebook.Price))
                .ForMember(dto => dto.CategoryName, act => act.MapFrom(obj => obj.Category.Name))
                .ForMember(dto => dto.PublisherName, act => act.MapFrom(obj => obj.Publisher.Name));
            CreateMap<Book, BookShowDto>().ForMember(dto => dto.ImgPath, act => act.MapFrom(obj => obj.BookImages.FirstOrDefault().ImgPath));
            CreateMap<Book, BookShowDtoVer2>()
                .ForMember(dto => dto.ImgPath, act => act.MapFrom(obj => obj.BookImages.FirstOrDefault().ImgPath))
                .ForMember(dto => dto.CategoryName, act => act.MapFrom(obj => obj.Category.Name))
                .ForMember(dto => dto.PublisherName, act => act.MapFrom(obj => obj.Publisher.Name));
            CreateMap<Order, OrderDtoForAdmin>()
                .ForMember(dto => dto.CustomerName, act => act.MapFrom(obj => obj.Customer.Name))
                .ReverseMap();
            CreateMap<Order, OrderDtoForCus>().ReverseMap();

            //Map ComboBook
            CreateMap<ComboBook, ComboBookDTO>().ReverseMap();

            //Map Detail ComboBook
            CreateMap<DetailComboBook, ComboBookDTO>().ReverseMap();
            CreateMap<DetailComboBook, ListBookOfCombo>().ForMember(dto => dto.Book, act => act.MapFrom(obj => obj.Book));
            CreateMap<DetailComboBook, DetailComboBookDtoShow>()
                .ForMember(dto => dto.ComboName, act => act.MapFrom(obj => obj.ComboBook.Name))
                .ForMember(dto => dto.BookName, act => act.MapFrom(obj => obj.Book.Name))
                .ForMember(dto => dto.BookIsbn, act => act.MapFrom(obj => obj.Book.Isbn))
                .ForMember(dto => dto.BookAuthor, act => act.MapFrom(obj => obj.Book.Author))
                .ForMember(dto => dto.CategoryName, act => act.MapFrom(obj => obj.Book.Category.Name))
                .ForMember(dto => dto.PublisherName, act => act.MapFrom(obj => obj.Book.Publisher.Name));
        }
    }
}
