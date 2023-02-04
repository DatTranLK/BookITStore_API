using AutoMapper;
using Entity.Dtos.EBook;
using Entity.Models;
using Repository.IRepositories;
using Service.IServices;
using Service.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class EBookService : IEBookService
    {
        private readonly IEBookRepository _eBookRepository;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });
        public EBookService(IEBookRepository eBookRepository)
        {
            _eBookRepository = eBookRepository;
        }

        public async Task<ServiceResponse<string>> ChangeInformationOfEBook(int id, EBookDtoForUpdate eBookDtoForUpdate)
        {
            try
            {
                var checkExist = await _eBookRepository.GetById(id);
                if (checkExist == null)
                {
                    return new ServiceResponse<string>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                if (!string.IsNullOrEmpty(eBookDtoForUpdate.Price.ToString()))
                {
                    checkExist.Price = eBookDtoForUpdate.Price;
                    await _eBookRepository.Save();
                }
                if (!string.IsNullOrEmpty(eBookDtoForUpdate.PdfUrl))
                {
                    checkExist.PdfUrl = eBookDtoForUpdate.PdfUrl;
                    await _eBookRepository.Save();
                }
                if (!string.IsNullOrEmpty(eBookDtoForUpdate.HasPhysicalBook.ToString()))
                {
                    checkExist.HasPhysicalBook = eBookDtoForUpdate.HasPhysicalBook;
                    await _eBookRepository.Save();
                }
                
                return new ServiceResponse<string>
                {
                    Message = "Successfully",
                    Success = true,
                    StatusCode = 204
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<int>> CountAll()
        {
            try
            {
                var count = await _eBookRepository.CountAll(null);
                if (count <= 0)
                {
                    return new ServiceResponse<int>
                    {
                        Data = 0,
                        Message = "Successfully",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<int>
                {
                    Data = count,
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

        public async Task<ServiceResponse<int>> CreateNewEBook(EBookDto eBookDto)
        {
            try
            {
                //Validation in here 
                //Starting insert into Db
                var _mapper = config.CreateMapper();
                var eBook = _mapper.Map<Ebook>(eBookDto);
                eBook.Book.IsActive = true;
                eBook.Book.SetBookId = null;
                eBook.Book.IsSetBook = false;
                eBook.Book.AmountSold = 0;
                await _eBookRepository.Insert(eBook);
                return new ServiceResponse<int>
                {
                    Data = eBook.EbookId,
                    Message = "Successfully",
                    Success = true,
                    StatusCode = 201
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        public async Task<ServiceResponse<EBookDto>> GetEBookByBookId(int bookId)
        {
            try
            {
                List<Expression<Func<Ebook, object>>> includes = new List<Expression<Func<Ebook, object>>>
                {
                    x => x.Book
                };
                var ebook = await _eBookRepository.GetByWithCondition(x => x.BookId == bookId, includes, true);
                var _mapper = config.CreateMapper();
                var ebookDto = _mapper.Map<EBookDto>(ebook);
                if (ebook == null)
                {

                    return new ServiceResponse<EBookDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<EBookDto>
                {
                    Data = ebookDto,
                    Message = "Successfully",
                    StatusCode = 200,
                    Success = true
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<EBookDto>> GetEBookById(int id)
        {
            try
            {
                List<Expression<Func<Ebook, object>>> includes = new List<Expression<Func<Ebook, object>>>
                {
                    x => x.Book
                };
                var ebook = await _eBookRepository.GetByWithCondition(x => x.EbookId == id, includes, true);
                var _mapper = config.CreateMapper();
                var ebookDto = _mapper.Map<EBookDto>(ebook);
                if (ebook == null)
                {

                    return new ServiceResponse<EBookDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<EBookDto>
                {
                    Data = ebookDto,
                    Message = "Successfully",
                    StatusCode = 200,
                    Success = true
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<IEnumerable<EBookDto>>> GetEBooksWithPagination(int page, int pageSize)
        {
            try
            {
                if (page <= 0)
                {
                    page = 1;
                }
                List<Expression<Func<Ebook, object>>> includes = new List<Expression<Func<Ebook, object>>>
                { 
                    x => x.Book
                };
                var lst = await _eBookRepository.GetAllWithPagination(null, includes, x => x.EbookId, true, page, pageSize);
                var _mapper = config.CreateMapper();
                var lstDto = _mapper.Map<IEnumerable<EBookDto>>(lst);
                if (lst.Count() <= 0)
                {
                    return new ServiceResponse<IEnumerable<EBookDto>>
                    { 
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<IEnumerable<EBookDto>>
                {
                    Data = lstDto,
                    Message = "Successfully",
                    StatusCode = 200,
                    Success = true
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /*public async Task<ServiceResponse<EBookDtoForUpdate>> UpdateEBook(EBookDtoForUpdate eBookDtoForUpdate, int bookId)
        {
            try
            {
                *//*List<Expression<Func<Ebook, object>>> includes = new List<Expression<Func<Ebook, object>>>
                {
                    x => x.Book
                };*//*
                var checkExist = await _eBookRepository.GetByWithCondition(x => x.BookId == bookId, null, true);
                var _mapper = config.CreateMapper();
                var ebook = _mapper.Map<Ebook>(eBookDtoForUpdate);
                if (checkExist == null)
                {

                    return new ServiceResponse<EBookDtoForUpdate>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                if (!string.IsNullOrEmpty(eBookDtoForUpdate.Price.ToString()))
                {
                    checkExist.Price = eBookDtoForUpdate.Price;
                    await _eBookRepository.Save();
                }
                if (!string.IsNullOrEmpty(eBookDtoForUpdate.PdfUrl))
                {
                    checkExist.PdfUrl = eBookDtoForUpdate.PdfUrl;
                    await _eBookRepository.Save();
                }
                if (!string.IsNullOrEmpty(eBookDtoForUpdate.HasPhysicalBook.ToString()))
                {
                    checkExist.HasPhysicalBook = eBookDtoForUpdate.HasPhysicalBook;
                    await _eBookRepository.Save();
                }
                *//*if (!string.IsNullOrEmpty(ebook.Book.Name))
                {
                    checkExist.Book.Name = ebook.Book.Name;
                }
                if (!string.IsNullOrEmpty(ebook.Book.Isbn))
                {
                    checkExist.Book.Isbn = ebook.Book.Isbn;
                }
                if (!string.IsNullOrEmpty(ebook.Book.Author))
                {
                    checkExist.Book.Author = ebook.Book.Author;
                }
                if (!string.IsNullOrEmpty(ebook.Book.ReleaseYear))
                {
                    checkExist.Book.ReleaseYear = ebook.Book.ReleaseYear;
                }
                if (!string.IsNullOrEmpty(ebook.Book.Version.ToString()))
                {
                    checkExist.Book.Version = ebook.Book.Version;
                }
                if (!string.IsNullOrEmpty(ebook.Book.Description))
                {
                    checkExist.Book.Description = ebook.Book.Description;
                }
                if (!string.IsNullOrEmpty(ebook.Book.CategoryId.ToString()))
                {
                    checkExist.Book.CategoryId = ebook.Book.CategoryId;
                }
                if (!string.IsNullOrEmpty(ebook.Book.PublisherId.ToString()))
                {
                    checkExist.Book.PublisherId = ebook.Book.PublisherId;
                }
                if (!string.IsNullOrEmpty(ebook.Book.SetBookId.ToString()))
                {
                    checkExist.Book.SetBookId = ebook.Book.SetBookId;
                }
                if (!string.IsNullOrEmpty(ebook.Book.IsSetBook.ToString()))
                {
                    checkExist.Book.IsSetBook = ebook.Book.IsSetBook;
                }*/
                /*await _eBookRepository.Update(checkExist);*//*
                return new ServiceResponse<EBookDtoForUpdate>
                { 
                    Data = eBookDtoForUpdate,
                    Message = "Successfully",
                    Success = true,
                    StatusCode = 204
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }*/
    }
}
