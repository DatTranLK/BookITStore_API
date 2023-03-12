using AutoMapper;
using Entity.Dtos.Category;
using Entity.Dtos.ComboBookDTO;
using Entity.Models;
using Repository.IRepositories;
using Repository.Repositories;
using Service.IServices;
using Service.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ComboBookService : IComboBookService
    {
        private readonly IComboBookRepository comboBookRepository;
        private readonly IDetailComboBookRepository detailComboBookRepository;
        private readonly IBookRepository _bookRepository;
        MapperConfiguration configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });

        public ComboBookService(IComboBookRepository comboBookRepository, IDetailComboBookRepository detailComboBookRepository, IBookRepository bookRepository)
        {
            this.comboBookRepository = comboBookRepository;
            this.detailComboBookRepository = detailComboBookRepository;
            _bookRepository = bookRepository;
        }
        public async Task<ServiceResponse<int>> CountComboBooks()
        {
            try
            {
                var count = await comboBookRepository.CountAll(null);
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

        public async Task<ServiceResponse<int>> CreateNewComboBook(ComboBook comboBoo, List<int> bookId)
        {
            try
            {
                //Validation in DetailComboBook here
                HashSet<int> uniqueItems = new HashSet<int>();
                foreach (int bookIdItem in bookId)
                {
                    //Check unique item 
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
                    //Check exist item
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
                }
                //Starting insert to Db
                //Create ComboBook
                comboBoo.IsActive = true;
                await comboBookRepository.Insert(comboBoo);

                //Create Detail Combobook
                foreach (int bookIdItem in bookId)
                {
                    DetailComboBook detailComboBook = new DetailComboBook();
                    detailComboBook.ComboBookId = comboBoo.Id;
                    detailComboBook.BookId = bookIdItem;
                    await detailComboBookRepository.Insert(detailComboBook);
                }

                return new ServiceResponse<int>
                {
                    Data = comboBoo.Id,
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

        public async Task<ServiceResponse<int>> CreateNewComboBookVer2(ComboBook comboBoo)
        {
            //Validation in here
            //Starting insert into DB
            comboBoo.IsActive = true;
            comboBoo.IsCombo = true;
            await comboBookRepository.Insert(comboBoo);
            return new ServiceResponse<int>
            {
                Data = comboBoo.Id,
                StatusCode = 201,
                Success = true,
                Message = "Successfully"
            };
        }

        public async Task<ServiceResponse<string>> DisableOrEnableComboBook(int id)
        {
            try
            {
                var checkExist = await comboBookRepository.GetById(id);
                if (checkExist == null)
                {
                    return new ServiceResponse<string>
                    {
                        Message = "No combo has found",
                        Success = true,
                        StatusCode = 200
                    };
                }
                if (checkExist.IsActive == true)
                {
                    checkExist.IsActive = false;
                    await comboBookRepository.Save();
                }
                else if (checkExist.IsActive == false)
                {
                    checkExist.IsActive = true;
                    await comboBookRepository.Save();
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

        public async Task<ServiceResponse<IEnumerable<ComboBookDTO>>> GetComboBookWithPagination(int page, int pageSize)
        {
            try
            {
                if (page <= 1)
                {
                    page = 1;
                }
                var lst = await comboBookRepository.GetAllWithPagination(null, null, x => x.Id, true, page, pageSize);
                var _mapper = configuration.CreateMapper();
                var lstDto = _mapper.Map<IEnumerable<ComboBookDTO>>(lst);
                if (lst.Count() <= 0)
                {
                    return new ServiceResponse<IEnumerable<ComboBookDTO>>
                    {
                        Message = "Has no data exist",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<IEnumerable<ComboBookDTO>>
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

        //Ham` nay phai do lai theo man` hinh`
        public async Task<ServiceResponse<ComboBookDTO>> GetComboBookyById(int id)
        {
            try
            {
                var combo = await comboBookRepository.GetById(id);
                var mapper = configuration.CreateMapper();
                var comboDTO = mapper.Map<ComboBookDTO>(combo);
                if (combo == null)
                {
                    return new ServiceResponse<ComboBookDTO>
                    {
                        Message = "No combo has found",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new ServiceResponse<ComboBookDTO>
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

        public async Task<ServiceResponse<ComboBook>> UpdateComboBook(ComboBook comboBook)
        {
            try
            {
                var checkExist = await comboBookRepository.GetById(comboBook.Id);
                if (checkExist == null)
                {
                    return new ServiceResponse<ComboBook>
                    {
                        Message = "No combo has found",
                        Success = true,
                        StatusCode = 200
                    };
                }
                if (!string.IsNullOrEmpty(comboBook.Name))
                {
                    checkExist.Name = comboBook.Name;
                }
                if (!string.IsNullOrEmpty(comboBook.Description))
                {
                    checkExist.Description = comboBook.Description;
                }
                if (!string.IsNullOrEmpty(comboBook.PriceReduction.ToString()))
                {
                    checkExist.PriceReduction = comboBook.PriceReduction;
                }
              
                await comboBookRepository.Update(checkExist);
                return new ServiceResponse<ComboBook>
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
