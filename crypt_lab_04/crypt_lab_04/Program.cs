using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypt_lab_04 {
    class Program {
        static void Main(string[] args) {

            Recipient Sergei = new Recipient();
            Sergei.toGenerate();

            Sender Ivan = new Sender();
            Ivan.sendKeys(Sergei.e, Sergei.n);
            Ivan.sendMessage("crypt_lab_04 is done");
            Ivan.toEncrypt();

            Sergei.toDecrypt(Ivan.crypt_message);

            Console.WriteLine(Sergei.start_message);

        }
    }
}
