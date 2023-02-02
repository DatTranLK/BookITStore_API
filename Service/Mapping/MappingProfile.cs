using AutoMapper;
using Entity.Dtos.Account;
using Entity.Dtos.Category;
using Entity.Dtos.OrderDetail;
using Entity.Dtos.Publisher;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                .ForMember(dto => dto.ComboBookName, act => act.MapFrom(obj => obj.ComboBook.Name));
        }
    }
}
