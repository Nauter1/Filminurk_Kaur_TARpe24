using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filminurk.Core.Dto.AccountsDTOs
{
    public class ApplicationUserDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool ProfileType { get; set; }
        public List<Guid>? FavoriteListIDs { get; set; }
        public List<Guid> CommentIDs { get; set; }
        public string AvatarImageID { get; set; }
        public string AvatarName { get; set; }
        public int AvatarRating { get; set; }
        public string Opinion { get; set; }
    }
}
