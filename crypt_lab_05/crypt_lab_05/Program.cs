using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypt_lab_05 {
    class Program {
        static void Main(string[] args) {
            string message = "sergeiregister";
            ShannonFano SF = new ShannonFano(message);

            SF.toEncrypt();
            Console.WriteLine("crypt message: " + SF.getCryptMessage());
            Console.WriteLine("[compression] = " + SF.countCompression());  // степень сжатия

            SF.toDecrypt();
            Console.WriteLine("start message: " + SF.getStartMessage());

        }
    }
}
