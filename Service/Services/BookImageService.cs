using AutoMapper;
using Entity.Dtos.BookImage;
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
    public class BookImageService : IBookImageService
    {
        private readonly IBookImageRepository _bookImageRepository;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });

        public BookImageService(IBookImageRepository bookImageRepository)
        {
            _bookImageRepository = bookImageRepository;
        }

        public async Task<ServiceResponse<int>> CountBookImages()
        {
            try
            {
                var count = await _bookImageRepository.CountAll(null);
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

        public async Task<ServiceResponse<int>> CreateNewBookImage(BookImage bookImage)
        {
            try
            {
                await _bookImageRepository.Insert(bookImage);
                return new ServiceResponse<int>
                {
                    Data = bookImage.Id,
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

        public async Task<ServiceResponse<string>> DeleteBookImage(int id)
        {
            try
            {
                var checkExisted = await _bookImageRepository.GetById(id);
                if (checkExisted == null)
                {
                    return new ServiceResponse<string>
                    {
                        Message = "NO rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                await _bookImageRepository.Delete(checkExisted);
                return new ServiceResponse<string>
                {
                    Message = "Successfully",
                    Success = true,
                    StatusCode = 204,
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<IEnumerable<BookImageDto>>> GetBookImageByBookId(int bookId)
        {
            try
            {
                List<Expression<Func<BookImage, object>>> includes = new List<Expression<Func<BookImage, object>>>
                {
                    b => b.Book
                };
                var bookImages = await _bookImageRepository.GetAllWithCondition(b => b.BookId == bookId, includes, null, true);
                var mapper = config.CreateMapper();
                var bookImageDto = mapper.Map<IEnumerable<BookImageDto>>(bookImages);
                if (bookImages.Count() <= 0)
                {
                    return new ServiceResponse<IEnumerable<BookImageDto>>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<IEnumerable<BookImageDto>>
                {
                    Data = bookImageDto,
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

        public async Task<ServiceResponse<BookImageDto>> GetBookImageById(int id)
        {
            try
            {
                var bookImage = await _bookImageRepository.GetById(id);
                var _mapper = config.CreateMapper();
                var bookImageDto = _mapper.Map<BookImageDto>(bookImage);
                if (bookImage == null )
                {
                    return new ServiceResponse<BookImageDto>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<BookImageDto>
                {
                    Data = bookImageDto,
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

        public async Task<ServiceResponse<IEnumerable<BookImageDto>>> GetBookImageWithPagination(int page, int pageSize)
        {
            try
            {
                if (page <= 1)
                {
                    page = 1;
                }
                List<Expression<Func<BookImage, object>>> includes = new List<Expression<Func<BookImage, object>>>
                {
                    b => b.Book
                };

                var lstBookImage = await _bookImageRepository.GetAllWithPagination(null, includes, b => b.Id, true, page, pageSize);
                var mapper = config.CreateMapper();
                var bookImageDto = mapper.Map<IEnumerable<BookImageDto>>(lstBookImage);
                if (lstBookImage.Count() <= 0)
                {
                    return new ServiceResponse<IEnumerable<BookImageDto>>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200,
                    };
                }
                return new ServiceResponse<IEnumerable<BookImageDto>>
                {
                    Data = bookImageDto,
                    Message = "Successfully",
                    Success = true,
                    StatusCode = 200,
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<BookImage>> UpdateBookImage(int id, BookImage bookImage)
        {
            try
            {
                var checkExist = await _bookImageRepository.GetById(id);
                if (checkExist == null)
                {
                    return new ServiceResponse<BookImage>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200,
                    };
                }
                if (!string.IsNullOrEmpty(bookImage.ImgPath))
                {
                    checkExist.ImgPath = bookImage.ImgPath;
                }
                await _bookImageRepository.Update(checkExist);
                return new ServiceResponse<BookImage>
                {
                    Data = checkExist,
                    Message = "Successfully",
                    Success = true,
                    StatusCode = 204,
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
