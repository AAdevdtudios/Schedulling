using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedulling.Interfaces
{
    public interface IMaillingService
    {
        void SendMail(string message, string email);
    }
}
