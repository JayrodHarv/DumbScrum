using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects {
    public class File {
        public int FileID { get; set; }
        public byte[] Data { get; set; }
        public string Extension { get; set; }
        public int TaskID { get; set; }
        public string FileName { get; set; }
        public string Type { get; set; }
    }
}
