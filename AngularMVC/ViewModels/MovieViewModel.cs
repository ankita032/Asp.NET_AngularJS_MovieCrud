using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AngularMVC.ViewModels
{
    public class MovieViewModel
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }

        public int ProducerID { get; set; }
        
        public List<ProducersActorsViewModel> Movies { get; set; }
        public List<ProducersActorsViewModel> producersactors { get; set; }
    }
}