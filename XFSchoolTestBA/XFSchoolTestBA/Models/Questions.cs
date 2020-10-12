using System;
using System.Collections.Generic;
using System.Text;

namespace XFSchoolTestBA.Models
{
    public class Questions
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Answer { get; set; }
        public int Test { get; set; }
    }
}
