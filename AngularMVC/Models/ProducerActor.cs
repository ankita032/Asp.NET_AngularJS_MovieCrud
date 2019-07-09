using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace AngularMVC.Models
{
    public class ProducerActor
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
       
        public virtual Movie Movies { get; set; }
    }
}
