using Microsoft.EntityFrameworkCore;
using Schedulling.Modal.Database_Modal;
using Schedulling.MyData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.AspNet.Common;
using Twilio.AspNet.Core;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML;

namespace Schedulling.Interfaces
{
    public class JobTestServices : IJobTestService
    {
        DatabaseContexts contexts;
        public JobTestServices(DatabaseContexts ctx)
        {
            contexts = ctx;
        }
        public void DelayedJob()
        {
            throw new NotImplementedException();
        }

        public void FireAndForgetJob()
        {
            throw new NotImplementedException();
        }
       /* public TwiMLResult Index(SmsRequest incomming)
        {
            var messageResponse = new MessagingResponse();
            messageResponse.Message("This is a test"+incomming.From);
            return TwiML(messageResponse);
        }*/
        public void ReccuringJob(int id)
        {
            TwilioClient.Init("AC6dc45e4e803ed9318e363c0d175191d4", "7e0335e7cd6a428484bb37791beab4c1");
            var item = contexts.Schedules.Include(i => i.Phones).Where(i => i.Id == id).FirstOrDefault();
            item.Date = DateTime.Now;

            foreach (var phone in item.Phones)
            {
                MessageResource.Create(
                    from: new Twilio.Types.PhoneNumber("+17622404373"),
                    to: new Twilio.Types.PhoneNumber(phone.Phone),
                    body: ""+ item.Message);
            }
            contexts.Schedules.Update(item);
            contexts.SaveChanges();
        }
    }
}
