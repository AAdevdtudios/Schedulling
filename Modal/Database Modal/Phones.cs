namespace Schedulling.Modal.Database_Modal
{
    public class Phones
    {
        public int Id { get; set; }
        public string Phone { get; set; }

        public int schedulesId { get; set; }
        public virtual Schedules Schedules { get; set; }
    }
}
