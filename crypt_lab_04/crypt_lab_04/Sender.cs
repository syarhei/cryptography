using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace crypt_lab_04 {
    class Sender {

        private long e;
        private long n;
        private string start_message;
        public List<string> crypt_message = new List<string>();

        public void sendKeys(long E, long N) {
            e = E;
            n = N;
        }

        public void sendMessage(string sms) {
            start_message = sms;
        }
        
        public void toEncrypt() {
            
            foreach (char symbol in start_message) {
                BigInteger digit = new BigInteger(symbol);
                digit = BigInteger.Pow(digit, (int)e);
                BigInteger N = new BigInteger(n);
                digit = digit % N;
                crypt_message.Add(digit.ToString());
            }
            
            return;
        }

    }
}
