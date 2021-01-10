using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SuciuBogdan_7946.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Nume Proiect")]
        public String Name { get; set; }

    }
}
