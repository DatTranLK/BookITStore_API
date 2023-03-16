using AutoMapper;
using Entity.Dtos.Book;
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
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task<ServiceResponse<int>> CountAll()
        {
            try
            {
                var count = await _bookRepository.CountAll(null);
                if (count <= 0)
                {
                    return new ServiceResponse<int>
                    { 
                        Data = 0,
                        Success = true,
                        Message = "Successfully",
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<int>
                {
                    Data = count,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<int>> CountBooksShow()
        {
            try
            {
                var count = await _bookRepository.CountAll(x => x.IsActive == true);
                if (count <= 0)
                {
                    return new ServiceResponse<int>
                    { 
                        Data = 0,
                        Success = true,
                        Message = "Successfully",
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<int>
                { 
                    Data = count,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<int>> CountBooksShowVer2()
        {
            try
            {
                var count = await _bookRepository.CountAll(x => x.IsActive == true && x.Amount > 0);
                if (count <= 0)
                {
                    return new ServiceResponse<int>
                    {
                        Data = 0,
                        Success = true,
                        Message = "Successfully",
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<int>
                {
                    Data = count,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<int>> CountPhysicalBooks()
        {
            try
            {
                var count = await _bookRepository.CountAll(null);
                if (count <= 0)
                {
                    return new ServiceResponse<int>
                    {
                        Data = 0,
                        Message = "Succesfully",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<int>
                {
                    Data = count,
                    Message = "Succesfully",
                    StatusCode = 200,
                    Success = true
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<int>> CreateNewPhysicalBook(Book book)
        {
            try
            {
                //Validation in here
                //Starting insert to Db
                book.IsActive = true;
                book.AmountSold = 0;
                await _bookRepository.Insert(book);
                return new ServiceResponse<int>
                {
                    Data = book.Id,
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

        public async Task<ServiceResponse<int>> CreateNewPhysicalBookAndEBook(BookDtoForPhysicalAndEBook bookDtoForPhysicalAndEBook)
        {
            try
            {
                //Validation in here
                //Starting insert into Db
                var _mapper = config.CreateMapper();
                var bookAndEbook = _mapper.Map<Book>(bookDtoForPhysicalAndEBook);
                bookAndEbook.IsActive = true;
                bookAndEbook.AmountSold = 0;
                await _bookRepository.Insert(bookAndEbook);
                return new ServiceResponse<int>
                {
                    Data = bookAndEbook.Id,
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

        public async Task<ServiceResponse<string>> DisableOrEnableBook(int id)
        {
            try
            {
                var checkExist = await _bookRepository.GetById(id);
                if (checkExist == null)
                {
                    return new ServiceResponse<string>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                if (checkExist.IsActive == true)
                {
                    checkExist.IsActive = false;
                    await _bookRepository.Save();
                }
                else if (checkExist.IsActive == false)
                {
                    checkExist.IsActive = true;
                    await _bookRepository.Save();
                }
                return new ServiceResponse<string>
                {
                    Message = "Successfully",
                    StatusCode = 204,
                    Success = true
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<IEnumerable<TopSelling>>> Get10BooksOfTopSelling()
        {
            try
            {
                var lst = await _bookRepository.GetAllWithCondition(null, null, x => (int)x.AmountSold, true);
                var _mapper = config.CreateMapper();
                var lstDto = _mapper.Map<IEnumerable<TopSelling>>(lst);
                if(lst.Count() <= 0)
                {
                    return new ServiceResponse<IEnumerable<TopSelling>>
                    { 
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<IEnumerable<TopSelling>>
                {
                    Data = lstDto.Take(10),
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

        public async Task<ServiceResponse<BookDto>> GetBookById(int id)
        {
            try
            {
                List<Expression<Func<Book, object>>> includes = new List<Expression<Func<Book, object>>>
                {
                    x => x.Category,
                    x => x.Publisher
                };
                var book = await _bookRepository.GetByWithCondition(x => x.Id == id, includes, true);
                var _mapper = config.CreateMapper();
                var bookDto = _mapper.Map<BookDto>(book);
                if (book == null)
                {
                    return new ServiceResponse<BookDto>
                    { 
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<BookDto>
                {
                    Data = bookDto,
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

        public async Task<ServiceResponse<IEnumerable<BookShowDtoVer2>>> GetBooksShowVer2WithPagination(int page, int pageSize)
        {
            try
            {
                if (page <= 0)
                {
                    page = 1;
                }
                List<Expression<Func<Book, object>>> includes = new List<Expression<Func<Book, object>>>
                {
                    x => x.BookImages,
                    x => x.Category,
                    x => x.Publisher
                };
                var lst = await _bookRepository.GetAllWithPagination(x => x.IsActive == true && x.Amount > 0, includes, x => x.Id, true, page, pageSize);
                var _mapper = config.CreateMapper();
                var lstDto = _mapper.Map<IEnumerable<BookShowDtoVer2>>(lst);
                if (lst.Count() <= 0)
                {
                    return new ServiceResponse<IEnumerable<BookShowDtoVer2>>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<IEnumerable<BookShowDtoVer2>>
                {
                    Data = lstDto,
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

        public async Task<ServiceResponse<IEnumerable<BookShowDto>>> GetBooksShowWithPagination(int page, int pageSize)
        {
            try
            {
                if (page <= 0)
                {
                    page = 1;
                }
                List<Expression<Func<Book, object>>> includes = new List<Expression<Func<Book, object>>>
                {
                    x => x.BookImages
                };
                var lst = await _bookRepository.GetAllWithPagination(x => x.IsActive == true , includes, x => (int)x.AmountSold, true, page, pageSize);
                var _mapper = config.CreateMapper();
                var lstDto = _mapper.Map<IEnumerable<BookShowDto>>(lst);
                if (lst.Count() <= 0)
                {
                    return new ServiceResponse<IEnumerable<BookShowDto>>
                    { 
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<IEnumerable<BookShowDto>>
                {
                    Data = lstDto,
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

        public async Task<ServiceResponse<IEnumerable<BookDto>>> GetBooksWithPagination(int page, int pageSize)
        {
            try
            {
                if (page <= 0)
                {
                    page = 1;
                }
                List<Expression<Func<Book, object>>> includes = new List<Expression<Func<Book, object>>>
                { 
                    x => x.Category,
                    x => x.Publisher
                };
                var lst = await _bookRepository.GetAllWithPagination(null, includes, x => x.Id, true, page, pageSize);
                var _mapper = config.CreateMapper();
                var lstDto = _mapper.Map<IEnumerable<BookDto>>(lst);
                if (lst.Count() <= 0)
                {
                    return new ServiceResponse<IEnumerable<BookDto>>
                    { 
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<IEnumerable<BookDto>>
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

        /*public async Task<ServiceResponse<IEnumerable<PhysicalBookAndEbookDtoForAdmin>>> GetPhysicalBookAndEbookWithPagination(int page, int pageSize)
        {
            try
            {
                if (page <= 0)
                {
                    page = 1;
                }
                List<Expression<Func<Book, object>>> includes = new List<Expression<Func<Book, object>>>
                {
                    x => x.Category,
                    x => x.Publisher
                };
                var lst = await _bookRepository.GetAllWithPagination(x => x.Amount != null && x.HasEbook == true && x.Price != null || x.Amount != 0 && x.Price != null && x.HasEbook == true, includes, x => x.Id, true, page, pageSize);
                var mapper = config.CreateMapper();
                var lstDto = mapper.Map<IEnumerable<PhysicalBookAndEbookDtoForAdmin>>(lst);
                if (lstDto.Count() <= 0)
                {
                    return new ServiceResponse<IEnumerable<PhysicalBookAndEbookDtoForAdmin>>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<IEnumerable<PhysicalBookAndEbookDtoForAdmin>>
                {
                    Data = lstDto,
                    Message = "Succesfully",
                    StatusCode = 200,
                    Success = true
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }*/

        public async Task<ServiceResponse<IEnumerable<BookDtoForAdmin>>> GetPhysicalBookWithPagination(int page, int pageSize)
        {
            try
            {
                if (page <= 0)
                {
                    page = 1;
                }
                List<Expression<Func<Book, object>>> includes = new List<Expression<Func<Book, object>>>
                {
                    x => x.Category,
                    x => x.Publisher
                };
                var lst = await _bookRepository.GetAllWithPagination(null, includes, x => x.Id, true, page, pageSize);
                var mapper = config.CreateMapper();
                var lstDto = mapper.Map<IEnumerable<BookDtoForAdmin>>(lst);
                if (lstDto.Count() <= 0)
                {
                    return new ServiceResponse<IEnumerable<BookDtoForAdmin>>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<IEnumerable<BookDtoForAdmin>>
                {
                    Data = lstDto,
                    Message = "Succesfully",
                    StatusCode = 200,
                    Success = true
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<Book>> UpdatePhysicalBook(int id, Book book)
        {
            try
            {
                var checkExist = await _bookRepository.GetById(id);
                if (checkExist == null)
                {
                    return new ServiceResponse<Book>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                if (!string.IsNullOrEmpty(book.Name))
                { 
                    checkExist.Name = book.Name;
                }
                if (!string.IsNullOrEmpty(book.Isbn))
                {
                    checkExist.Isbn = book.Isbn;
                }
                if (!string.IsNullOrEmpty(book.Author))
                {
                    checkExist.Author = book.Author;
                }
                if (!string.IsNullOrEmpty(book.ReleaseYear))
                {
                    checkExist.ReleaseYear = book.ReleaseYear;
                }
                if (!string.IsNullOrEmpty(book.Version.ToString()))
                {
                    checkExist.Version = book.Version;
                }
                if (!string.IsNullOrEmpty(book.Price.ToString()))
                {
                    checkExist.Price = book.Price;
                }
                if (!string.IsNullOrEmpty(book.Description))
                {
                    checkExist.Description = book.Description;
                }
                if (!string.IsNullOrEmpty(book.Amount.ToString()))
                {
                    checkExist.Amount = book.Amount;
                }
                if (!string.IsNullOrEmpty(book.CategoryId.ToString()))
                {
                    checkExist.CategoryId = book.CategoryId;
                }
                if (!string.IsNullOrEmpty(book.PublisherId.ToString()))
                {
                    checkExist.PublisherId = book.PublisherId;
                }
                await _bookRepository.Update(checkExist);
                return new ServiceResponse<Book>
                { 
                    Data = checkExist,
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
    }
}
