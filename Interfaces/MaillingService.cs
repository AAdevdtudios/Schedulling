using Microsoft.Extensions.Options;
using RestSharp;
using System.Threading.Tasks;

namespace Schedulling.Interfaces
{
    public class MaillingService : IMaillingService
    {
        private readonly MailSettings _mailSettings;
        public MaillingService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task<RestResponse> SendMail(string body, string email)
        {
            var client = new RestClient("https://llconference.com/mailtest/");
            var request = new RestRequest();

            request.AddQueryParameter("toMail", email);
            request.AddQueryParameter("subject", "From LLC code");
            request.AddQueryParameter("body", body);
            request.Method = Method.Get;


            request.AddHeader("User-Agent", "Thunder Client (https://www.thunderclient.com)");
            request.AddHeader("Accept", "*/*");
            var response = await client.GetAsync(request);
            return response;
        }
    }
}
