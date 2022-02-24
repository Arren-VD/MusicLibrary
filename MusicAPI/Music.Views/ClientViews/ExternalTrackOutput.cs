using Music.Views.GlobalViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Views.ClientViews
{
    public class ExternalTrackOutput
    {
        public ExternalTrackOutput()
        {
            Artists = new List<NameDTO<string>>();
            Playlists = new List<NameDTO<string>>();
        }

        public List<NameDTO<string>> Artists { get; set; }
        public string ISRC_Id { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Preview_url { get; set; }
        public string ClientServiceName { get; set; }
        public List<NameDTO<string>> Playlists { get; set; }
    }
}
