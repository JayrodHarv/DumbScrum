using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects {
    public class ImageHelper {
        // TODO: Make this work with at least jpeg too
        // Maybe creating an Image table is nessessary to store the extension
        private static readonly string IMAGE_TYPE = "image/png";
        public static string ToBase64(byte[] data) {
            return "data:" + IMAGE_TYPE + ";base64," + Convert.ToBase64String(data);
        }
    }
}
