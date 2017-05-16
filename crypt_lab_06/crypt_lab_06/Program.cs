using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypt_lab_06 {
    class Program {
        static void Main(string[] args) {
            string message = "sergeiregister";
            Huffman huffman = new Huffman(message);

            huffman.toEncrypt();
            Console.WriteLine("crypt message: " + huffman.getCryptMessage());
            Console.WriteLine("[compression] = " + huffman.countCompression());  // степень сжатия

            huffman.toDecrypt();
            Console.WriteLine("start message: " + huffman.getStartMessage());
        }
    }
}
