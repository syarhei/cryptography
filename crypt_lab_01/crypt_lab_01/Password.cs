using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypt_lab_01 {
    class Password {

        private int lenght;
        private string password;
        
        public string generate(char[] symbol, int length) {
            lenght = length;
            int symbol_lenght = symbol.Length;

            Random rand = new Random();
            for (int i = 0; i < symbol_lenght; i++) {
                
                int possition = rand.Next(0, symbol_lenght);
                int register = rand.Next(2);

                string s = symbol[possition].ToString();
                if (register == 1)
                    s = s.ToUpper();

                password += s;
            }

            genegateDigit();

            return password;
        }

        private void genegateDigit() {

            int digit_count = lenght - password.Length;

            Random rand = new Random();
            for (int i = 0; i < digit_count; i++) {
                int possition = rand.Next(0, password.Length);
                int digit = rand.Next(0, 10);

                password = password.Insert(possition, digit.ToString());                
            }

        }
        
        public string getPassword() {
            return password;
        }
                           
    }
}
