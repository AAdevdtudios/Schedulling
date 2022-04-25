using Microsoft.EntityFrameworkCore;
using Schedulling.Modal.Database_Modal;
using Schedulling.MyData;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        IMaillingService _mailService;
        public JobTestServices(DatabaseContexts ctx, IMaillingService maillingService)
        {
            contexts = ctx;
            _mailService = maillingService;
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
        public void ReccuringJob()
        {
            TwilioClient.Init("AC6dc45e4e803ed9318e363c0d175191d4", "7e0335e7cd6a428484bb37791beab4c1");
            /*var item = contexts.Schedules.Include(i => i.Phones).Where(i => i.Id == id).FirstOrDefault();
            item.Date = DateTime.Now;

            foreach (var phone in item.Phones)
            {
                MessageResource.Create(
                    from: new Twilio.Types.PhoneNumber("+17622404373"),
                    to: new Twilio.Types.PhoneNumber(phone.Phone),
                    body: ""+ item.Message);
            }
            contexts.Schedules.Update(item);
            contexts.SaveChanges();*/

            List<Members> item = contexts.Members.ToList();
            for (int i = 0; i < item.Count; i++)
            {
                DateTime dob = Convert.ToDateTime(item[i].DOB);
                DateTime doa = Convert.ToDateTime(item[i].DOA);
                //DateTime today = DateTime.ParseExact(item[i].DOB, "MM/dd/yy", CultureInfo.InvariantCulture);
                //int result = DateTime.Compare(DateTime.Today, dateTime);
                var updateThis = contexts.Members.Where(option => option.Email == item[i].Email || option.PhoneNo == item[i].PhoneNo).FirstOrDefault();
                if (DateTime.Today.DayOfYear == dob.DayOfYear)
                {
                    if (updateThis.PhoneNo != null)
                    {
                        string phone = item[i].PhoneNo.Remove(0, 1);
                        MessageResource.Create(
                        from: new Twilio.Types.PhoneNumber("+17622404373"),
                        to: new Twilio.Types.PhoneNumber("+234" + phone),
                        body: "This is a reminder Message for DOA");
                    }
                    if (updateThis.Email != null)
                    {
                        string mailbody = $"<h2> Sjx Logistics Limited </h2>" +
                         $"<p><h3>Dear{item[i].Name}</h3></p>" +
                         $"<p><h3>Below are your account details.</h3></p> ";
                        _mailService.SendMail(mailbody, updateThis.Email);
                    }
                }

                if(DateTime.Today.DayOfYear == doa.DayOfYear)
                {
                    if(updateThis.PhoneNo != null)
                    {
                        string phone = item[i].PhoneNo.Remove(0,1);
                        MessageResource.Create(
                        from: new Twilio.Types.PhoneNumber("+17622404373"),
                        to: new Twilio.Types.PhoneNumber("+234"+ phone),
                        body: "This is a reminder Message for DOA");
                    }
                    if(updateThis.Email != null)
                    {
                        string mailbody = $"<h2> Sjx Logistics Limited </h2>" +
                         $"<p><h3>Dear{item[i].Name}</h3></p>" +
                         $"<p><h3>Below are your account details.</h3></p> ";
                        _mailService.SendMail(mailbody, updateThis.Email);
                    }

                }
            }
        }
    }
}
