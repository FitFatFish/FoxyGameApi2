using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foxy.DataLayer.Models.Public;

namespace Foxy.DataLayer.Models.FoxyUser
{
    public class UserAvatar
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [DisplayName("کاربر ثبت کننده")]
        public Guid FoxyUserId { get; set; }

        [DisplayName("تصویر نمایه")]
        public Guid AvatarId { get; set; }



        [DisplayName("کاربر ثبت کننده")]
        public Guid? InsertedUserId { get; set; }
        [DisplayName("کاربر حذف کننده")]
        public Guid? DeletedUserId { get; set; }
        [DisplayName("کاربر ویرایش کننده")]
        public Guid? UpdatedUserId { get; set; }

        [DisplayName("تاریخ ثبت")]
        [Required]
        public DateTime InsertDate { get; set; }
        [DisplayName("تاریخ حذف")]
        public DateTime? DeleteDate { get; set; }
        [DisplayName("تاریخ ویرایش")]
        public DateTime? UpdateDate { get; set; }

        #region Relations

        [ForeignKey("FoxyUserId")]
        public FoxyUserInfo foxyuser { get; set; }

        //[ForeignKey("AvatarId")]
        //public Avatar  avatar { get; set; }
        #endregion
    }
}
