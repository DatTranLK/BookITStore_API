using Entity.Dtos.Account;
using Entity.Dtos.Publisher;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IPublisherService
    {
        Task<ServiceResponse<int>> CountPublishers();
        Task<ServiceResponse<PublisherDto>> GetPublisherById(int id);
        Task<ServiceResponse<string>> DisableOrEnablePublisher(int id);
        Task<ServiceResponse<int>> CreatePublisher(Publisher publisher);
        Task<ServiceResponse<Publisher>> UpdatePublisher(int id, Publisher publisher);
        Task<ServiceResponse<IEnumerable<PublisherDto>>> GetAllPublisherWithPagination(int page, int pageSize);

        Task<ServiceResponse<IEnumerable<PublisherDto>>> GetAllPublisherForCusWithPagination(int page, int pageSize);
        Task<ServiceResponse<int>> CountPublishersForCus();
    }
}
