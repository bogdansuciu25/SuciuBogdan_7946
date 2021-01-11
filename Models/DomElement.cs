using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SuciuBogdan_7946.Models
{
    public class DomElement
    {
        [Key]
        public int Id { set; get; }
        [DisplayName("Nume Element")]
        public String Name { set; get; }
        [DisplayName("Identificator")]
        public String IdentifiedBy { set; get; }
        [DisplayName("Valoare Identificator")]
        public String LocatorValue { set; get; }
        [ForeignKey("Pages")]
        public int PageId { set; get; }
        public virtual Pages Page { set; get; }
    }
}
