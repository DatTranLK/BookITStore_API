using AutoMapper;
using Entity.Dtos.Book;
using Entity.Dtos.ComboBookDTO;
using Entity.Dtos.DetailComboBookDTO;
using Entity.Models;
using Repository.IRepositories;
using Repository.Repositories;
using Service.IServices;
using Service.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;

namespace Service.Services
{
    public class DetailComboBookService : IDetailComboBookService
    {
        private readonly IComboBookRepository comboBookRepository;
        private readonly IDetailComboBookRepository detailComboBookRepository;
        private readonly IBookRepository _bookRepository;
        MapperConfiguration configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });
        public DetailComboBookService(IDetailComboBookRepository detailComboBookRepository, IBookRepository bookRepository, IComboBookRepository comboBookRepository)
        {
            this.comboBookRepository = comboBookRepository;
            this.detailComboBookRepository = detailComboBookRepository;
            _bookRepository = bookRepository;
        }

        public async Task<ServiceResponse<int>> CreateNewDetailComboBook(int comboBookId, List<int> bookId)
        {
            try
            {
                //Validation in here
               

                //Check data exsitst 
                ComboBook comboBook = await comboBookRepository.GetById(comboBookId);
                if (comboBook == null)
                {
                    return new ServiceResponse<int>
                    {
                        Message = "No data has found",
                        Success = true,
                        StatusCode = 404
                    };
                }


                HashSet<int> uniqueItems = new HashSet<int>();
                //Starting insert to Db
                foreach (int bookIdItem in bookId)
                {
                    //Check data unique (chua check book da ton tai trong DB)
                    if (!uniqueItems.Contains(bookIdItem))
                    {
                        uniqueItems.Add(bookIdItem);
                    }
                    else
                    {
                        return new ServiceResponse<int>
                        {
                            Message = "Book has to be unique",
                            Success = true,
                            StatusCode = 400
                        };
                    }

                    //Check data exsitst 
                    var checkExist = await _bookRepository.GetById(bookIdItem);
                    if (checkExist == null)
                    {
                        return new ServiceResponse<int>
                        {
                            Message = "No data has found",
                            Success = true,
                            StatusCode = 404
                        };
                    }
                    DetailComboBook detailComboBook = new DetailComboBook();
                    detailComboBook.ComboBookId = comboBookId;
                    detailComboBook.BookId = bookIdItem;
                    await detailComboBookRepository.Insert(detailComboBook);
                }

                return new ServiceResponse<int>
                {
                    Data = comboBookId,
                    Message = "Successfully",
                    StatusCode = 201,
                    Success = true
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<int>> CreateNewDetailComboBookVer2(DetailComboBook detailComboBook)
        {
            try
            {
                //Validation in here
                //Starting insert into Db
                await detailComboBookRepository.Insert(detailComboBook);
                return new ServiceResponse<int>
                { 
                    Data = detailComboBook.Id,
                    Success = true,
                    StatusCode = 201,
                    Message = "Successfully"
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<IEnumerable<DetailComboBookDtoShow>>> GetDetailOfComboBookIdWithPagination(int id, int page, int pageSize)
        {
            try
            {
                if (page <= 0)
                {
                    page = 1;
                }
                List<Expression<Func<DetailComboBook, object>>> includes = new List<Expression<Func<DetailComboBook, object>>>
                {
                    x => x.Book,
                    x => x.Book.Category,
                    x => x.Book.Publisher,
                    x => x.ComboBook
                };
                var list = await detailComboBookRepository.GetAllWithPagination(x => x.ComboBookId == id, includes, null, true, page, pageSize);
                var mapper = configuration.CreateMapper();
                var convertedList = mapper.Map<IEnumerable<DetailComboBookDtoShow>>(list);
                if (convertedList == null)
                {
                    return new ServiceResponse<IEnumerable<DetailComboBookDtoShow>>
                    {
                        Message = "No combo detail has found",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<IEnumerable<DetailComboBookDtoShow>>
                {
                    Data = convertedList,
                    Message = "Successfully",
                    Success = true,
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
        }


        //lay 1 list sach thuoc combo do 
        public async Task<ServiceResponse<IEnumerable<ListBookOfCombo>>> GetListInDetailOfComboBookId(int id)
        {
            List<Expression<Func<DetailComboBook, object>>> includes = new List<Expression<Func<DetailComboBook, object>>>
            { 
                x => x.Book,
                x => x.Book.Category,
                x => x.Book.Publisher,
                x => x.ComboBook
            };
            var list = await detailComboBookRepository.GetAllWithCondition(x => x.ComboBookId == id, includes, null, true);
            var mapper = configuration.CreateMapper();
            var convertedList = mapper.Map< IEnumerable <ListBookOfCombo>> (list);
            if (convertedList == null)
            {
                return new ServiceResponse<IEnumerable<ListBookOfCombo>>
                {
                    Message = "No combo detail has found",
                    Success = true,
                    StatusCode = 200
                };
            }
            return new ServiceResponse<IEnumerable<ListBookOfCombo>>
            {
                Data = convertedList,
                Message = "Successfully",
                Success = true,
                StatusCode = 200
            };
        }

        public async Task<ServiceResponse<string>> RemoveDetailComboBook(int detailComboBookId)
        {
            try
            {
                //Validation in here
                //Starting insert to Db
     
                    var checkExist = await detailComboBookRepository.GetById(detailComboBookId);
                    if (checkExist == null)
                    {
                        return new ServiceResponse<string>
                        {
                            Message = "No data has found",
                            Success = true,
                            StatusCode = 404
                        };
                    }
                    await detailComboBookRepository.Delete(checkExist);
              
                return new ServiceResponse<string>
                {
                    Message = "Successfully",
                    StatusCode = 201,
                    Success = true
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
