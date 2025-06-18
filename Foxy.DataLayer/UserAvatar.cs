//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace Foxy.DataLayer.Models.Users
//{
//    public class UserAvatar
//    {
//        //[Key]
//        public Guid Id { get; set; } = Guid.NewGuid();

//        public Guid UserId { get; set; }
//        public Guid AvatarId { get; set; }
        
//        #region Relations

//        //[ForeignKey("FoxyUserId")]
//        public UserProfile foxyuser { get; set; }

//        //[ForeignKey("AvatarId")]
//        //public Avatar  avatar { get; set; }
//        #endregion
//    }
//}
