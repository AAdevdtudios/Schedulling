using Microsoft.EntityFrameworkCore;
using Schedulling.Modal.Database_Modal;
using Schedulling.MyData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

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

        public void ReccuringJob(int id)
        {
            TwilioClient.Init("AC6dc45e4e803ed9318e363c0d175191d4", "7e0335e7cd6a428484bb37791beab4c1");
            var item = contexts.Schedules.Include(i => i.Phones).Where(i => i.Id == id).FirstOrDefault();
            item.Date = DateTime.Now;

            ICollection<Phones> phones = item.Phones;
            foreach (var phone in item.Phones)
            {
                MessageResource.Create(
                    from: "whatsapp:+17622404373",
                    to: new Twilio.Types.PhoneNumber("whatsapp:" + phone.Phone),
                    body: ""+ item.Message);
            }

            /*for (int i = 0; i < item.Phones.Count; i++)
            {
                var phone = contexts.Phones.Where(i => i.Schedules == item).FirstOrDefault();
                *//*MessageResource.Create(
                    from: "",
                    to: new Twilio.Types.PhoneNumber("whatsapp:" + phone.Phone[i]),
                    body: "");*//*
                string value = phone.Phone[i]+"";
                Console.WriteLine(value);
            }*/


            contexts.Schedules.Update(item);
            contexts.SaveChanges();
        }
    }
}
