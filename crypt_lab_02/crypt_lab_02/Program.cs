using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypt_lab_02
{
    class Program {

        static void Main(string[] args) {
            
            Encryption crypt = new Encryption('a');
            crypt.setMessage("nikita pavlovich");
            crypt.encrypt();
            
            Console.WriteLine(crypt.getCryptMessage());

            crypt.decrypt();

            Console.WriteLine(crypt.getMessage());

        }
    }
}
