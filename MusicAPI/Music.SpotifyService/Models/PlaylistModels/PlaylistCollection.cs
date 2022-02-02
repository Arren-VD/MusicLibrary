﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Spotify.Models.PlaylistModels
{
    public class PlaylistCollection
    {
        public string href { get; set; }
        public List<PlaylistInfo> items { get; set; }
        public int limit { get; set; }
        public string next { get; set; }
        public int offset { get; set; }
        public object previous { get; set; }
        public int total { get; set; }
    }
}
