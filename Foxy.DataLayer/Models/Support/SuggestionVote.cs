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
    public class SuggestionVote
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid SuggestionId { get; set; }
        public Guid UserId { get; set; }

       
        public bool Like { get; set; }

        public Suggestion suggestion { get; set; }

        public FoxyUserInfo foxyUser { get; set; }
    }
}
