using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Schedulling.Modal;
using Schedulling.Modal.Database_Modal;
using Schedulling.MyData;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
        TwilloConfigModal twilloConfig;
        public JobTestServices(DatabaseContexts ctx, IMaillingService maillingService, IOptions<TwilloConfigModal> twilloconfigs)
        {
            contexts = ctx;
            _mailService = maillingService;
            twilloConfig = twilloconfigs.Value;
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
            TwilioClient.Init(twilloConfig.username, twilloConfig.password);
            List<Members> item = contexts.Members.ToList();
            for (int i = 0; i < item.Count; i++)
            {
                DateTime dob = Convert.ToDateTime(item[i].DOB);
                DateTime doa = Convert.ToDateTime(item[i].DOA);
                var updateThis = contexts.Members.Where(option => option.Email == item[i].Email || option.PhoneNo == item[i].PhoneNo).FirstOrDefault();
                if (DateTime.Today.DayOfYear == dob.DayOfYear)
                {
                    if (updateThis.PhoneNo != null)
                    {
                        string phone = item[i].PhoneNo.Remove(0, 1);
                        MessageResource.Create(
                        from: new Twilio.Types.PhoneNumber(twilloConfig.phone),
                        to: new Twilio.Types.PhoneNumber("+234" + phone),
                        body: "This is a Birth day message");
                    }
                    if (updateThis.Email != null)
                    {
                        string filePath = Path.GetFullPath("Controllers/HtmlCode/birthday.html");
                        StreamReader sr = new StreamReader(filePath);
                        string mailbody= sr.ReadToEnd().Replace("{{name}}", updateThis.Name);
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
                        string filePath = Path.GetFullPath("Controllers/HtmlCode/birthday.html");
                        StreamReader sr = new StreamReader(filePath);
                        string mailbody = sr.ReadToEnd().Replace("{{name}}", updateThis.Name);
                        _mailService.SendMail(mailbody, updateThis.Email);
                    }

                }
            }
        }
    }
}
