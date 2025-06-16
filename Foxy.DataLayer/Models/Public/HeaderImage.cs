using Foxy.DataLayer.Models.FoxyUser;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foxy.DataLayer.Models.Public
{
    public class HeaderImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } 

        public string Title { get; set; }

        public int RequiredLevel { get; set; }

        
    }
}
