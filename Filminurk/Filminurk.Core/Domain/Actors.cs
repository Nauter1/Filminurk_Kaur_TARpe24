﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filminurk.Core.Domain
{
    public class Actors
    {
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public List<string> MoviesActedFor { get; set; }
        public string PortraitID { get; set; }
        
        /* Kolm minu mõeldud asju */
        public int? ActorRating { get; set; }
        public Gender? Gender { get; set; }
        public Genre? FavoriteGenre { get; set; }

    }
}
