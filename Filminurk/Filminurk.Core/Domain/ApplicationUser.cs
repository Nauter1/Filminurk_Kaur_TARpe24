using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Filminurk.Core.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public List<Guid>? FavoriteListIDs { get; set; }
        public List<Guid>? CommentIDs { get; set; }
        public string AvatarImageID { get; set; }
        public string AvatarName { get; set; }
        public bool ProfileType { get; set; }
        /* Ise mõeldud välja */
        public int? AvatarRating { get; set; } = 0;
        public string? Opinion {  get; set; }
    }
}
