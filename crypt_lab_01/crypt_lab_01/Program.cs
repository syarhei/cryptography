using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypt_lab_01 {
    class Program {
        const int password_count = 4;
        static void Main(string[] args) {
                    
            string text = System.IO.File.ReadAllText("text.txt");

            Alphabet alpha = new Alphabet(text);
            alpha.castTo();
            alpha.toArrayCount();

            char[] symbol = alpha.getLetters(6);
            
            Password pass = new Password();
            pass.generate(symbol, 8);
                        
            Console.WriteLine("password: " + pass.getPassword());
            Console.WriteLine("entropy = " + alpha.getEntropy());
                        
        }
    }
}
