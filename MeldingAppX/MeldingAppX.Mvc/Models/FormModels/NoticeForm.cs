using System.Collections.Generic;
using System.Web.Mvc;

namespace MeldingAppX.Mvc.Models
{
    public class NoticeForm
    {
        public int Id { get; set; }
        public string Building { get; set; }
        public IEnumerable<SelectListItem> Buildings { get; set; }
        public string Category { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public string AdditionalLocation { get; set; }
        public string ReporterName { get; set; }
        public string PhoneNumber { get; set; }
        public string Comment { get; set; }
    }
}