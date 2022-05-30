using RestSharp;
using System.Threading.Tasks;

namespace Schedulling.Interfaces
{
    public interface IMaillingService
    {
        Task<RestResponse> SendMail(string message, string email);
    }
}
