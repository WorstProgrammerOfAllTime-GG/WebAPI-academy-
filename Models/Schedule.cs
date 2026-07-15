namespace ProjectAcademy.Models
{
    public class Schedule
    {
        public int ID { get; private set; }
        public int IDGroup { get; set; }
        public DateTime DateTime { get; set; }
        public string ? Auditorium { get; set; }
    }
}
