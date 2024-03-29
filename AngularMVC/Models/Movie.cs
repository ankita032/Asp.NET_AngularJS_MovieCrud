﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace AngularMVC.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int YearOfRelease { get; set; }
        
        public int ProducerId { get; set; }

        public virtual ICollection<ProducerActor> ProducersActors { get; set; }
    }
}