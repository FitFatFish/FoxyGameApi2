using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class MatchResDto
    {
    public Guid Id { get; set; }
    public Guid GameId { get; set; }

    public DateTime BeginDate { get; set; }
    public DateTime? EndDate { get; set; }
}

