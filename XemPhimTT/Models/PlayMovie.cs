using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XemPhimTT.Models
{
    public class PlayMovie
    {
        public Video Data1 { get; set; }
        public IEnumerable<Video> Data2 { get; set; }
        public IEnumerable<Comment> Data3 { get; set; }
    }
}