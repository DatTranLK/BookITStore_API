using AutoMapper;
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
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;

namespace Service.Services
{
    public class DetailComboBookService : IDetailComboBookService
    {
        private readonly IDetailComboBookRepository detailComboBookRepository;
        private readonly IBookRepository _bookRepository;
        MapperConfiguration configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });
        public DetailComboBookService(IDetailComboBookRepository detailComboBookRepository, IBookRepository bookRepository)
        {
            this.detailComboBookRepository = detailComboBookRepository;
            _bookRepository = bookRepository;
        }

        public async Task<ServiceResponse<int>> CreateNewDetailComboBook(int comboBookId, List<int> bookId)
        {
            try
            {
                //Validation in here
                //Starting insert to Db
                foreach (int bookIdItem in bookId)
                {
                    var checkExist = await _bookRepository.GetById(bookIdItem);
                    if (checkExist == null)
                    {
                        return new ServiceResponse<int>
                        {
                            Message = "No book has found",
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

        public async Task<ServiceResponse<DetailComboBookDTO>> GetDetailComboBookyById(int id)
        {
            try
            {
                var combo = await detailComboBookRepository.GetById(id);
                var mapper = configuration.CreateMapper();
                var comboDTO = mapper.Map<DetailComboBookDTO>(combo);
                if (combo == null)
                {
                    return new ServiceResponse<DetailComboBookDTO>
                    {
                        Message = "No data has found",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<DetailComboBookDTO>
                {
                    Data = comboDTO,
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

        public async Task<ServiceResponse<IEnumerable<DetailComboBookDTO>>> GetListInDetailOfComboBookId(int id)
        {
            var list = detailComboBookRepository.GetByCondition(x => x.ComboBookId == id);
            var mapper = configuration.CreateMapper();
            var convertedList = mapper.Map< IEnumerable < DetailComboBookDTO >> (list);
            if (convertedList == null)
            {
                return new ServiceResponse<IEnumerable<DetailComboBookDTO>>
                {
                    Message = "No combo detail has found",
                    Success = true,
                    StatusCode = 200
                };
            }
            return new ServiceResponse<IEnumerable<DetailComboBookDTO>>
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
