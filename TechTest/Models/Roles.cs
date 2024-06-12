using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace TechTest.Models
{
    public class Roles : TEntity
    {
        public string Description { get; set; }
    }
}