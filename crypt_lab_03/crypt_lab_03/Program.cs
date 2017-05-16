using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypt_lab_03 {
    class Program {
        static void Main(string[] args) {
            Compression compress = new Compression("lanabanana");
            compress.toComression();
            Console.WriteLine(compress.getCryptMessage());
            compress.toDecompression();
            Console.WriteLine(compress.getStartMessage());
        }
    }
}