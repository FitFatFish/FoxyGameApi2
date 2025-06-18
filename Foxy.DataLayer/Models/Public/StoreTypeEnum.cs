using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foxy.DataLayer.Models.Public
{
    public enum StoreTypeEnum
    {
      
        //[Display(Name = "قابل خریداری")]

        Purchasable = 0,

        //[Display(Name = "ویژه")]
         
        Special = 1,

        //[Display(Name = "مسابقات")]
         
        Tournment = 2,
    }

}
