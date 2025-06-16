using Foxy.DataLayer.Models.FoxyUser;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foxy.DataLayer.Models.Support
{
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } 

        public string Title { get; set; }

        public string Description { get; set; }

        //CU
        //
        public Guid? CreatedBy { get; set; } // CreatedBy , CreatedAt
        public Guid? UpdatedBy { get; set; } // UpdatedBy
        

        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }


        public FoxyUserInfo foxyUser { get; set; }
    }
}
