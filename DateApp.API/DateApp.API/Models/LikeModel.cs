using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DateApp.API.Models
{
    public class LikeModel
    {
        public int LikerID { get; set; }
        public int LikeeID { get; set; }
        public UserModel Liker { get; set; }
        public UserModel Likee { get; set; }

    }
}
