using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedulling.Modal
{
    public class RequestModal
    {
        public string Label { get; set; }
        public string Message { get; set; }
        public List<string> Phones { get; set; }
        public byte[] Images { get; set; }

        //Trick part
        //It is for a onces request
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int minutes { get; set; }
    }
}
