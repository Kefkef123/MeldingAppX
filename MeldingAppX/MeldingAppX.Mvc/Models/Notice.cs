namespace MeldingAppX.Mvc.Models
{
    public class Notice
    {
        public int Id { get; set; }
        public int Category { get; set; }
        public int Building { get; set; }
        public string AdditionalLocation { get; set; }
        public string PhoneNumber { get; set; }
        public string ReporterName { get; set; }
        public string Comment { get; set; }
    }
}