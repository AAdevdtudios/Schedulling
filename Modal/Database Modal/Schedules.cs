using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Schedulling.Modal.Database_Modal
{
    public class Schedules
    {
        public Schedules()
        {
            Phones = new HashSet<Phones>();
        }
        public int Id { get; set; }
        public string Label { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public byte[] Images { get; set; }
        public int JobId { get; set; }

        public int phonId { get; set; }
        public virtual ICollection<Phones> Phones { get; set; }
    }
}
