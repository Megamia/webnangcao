using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XemPhimTT.Models
{
    public class DetailMovie
    {
        public Movie Data1 { get; set; }
        public IEnumerable<CategoryList> Data2 { get; set; }
        public IEnumerable<Comment> Data3 { get; set; }
        public IEnumerable<Video> Data4 { get; set; }
        public Video Data5 { get; set; }
        public string Data6 { get; set; }
    }
}