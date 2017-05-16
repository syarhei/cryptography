using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypt_lab_02
{
    class Encryption {

        private string message;
        private string crypt_message;
        private char fixcode;

        public void setMessage(string mess) {
            message = mess;
        }

        public string getMessage() {
            return message;
        }

        public string getCryptMessage() {
            return crypt_message;
        }

        private void mathFunc(int number, int previous_code) {
            int curent_code = message.ElementAt(number);
            int key = number + previous_code;
            char element = (char)(curent_code + key);

            while (element > 255) {
                element -= (char)255;
            }

            crypt_message += element;
            
        }

        public void encrypt() {

            mathFunc(0, fixcode);
            for (int i = 1;  i < message.Length; i++) {
                mathFunc(i, message[i - 1]);
            }

            message = null;                        
        }

        private void test(int number, int previous_code) {
            int curent_code = crypt_message.ElementAt(number);
            int key = (number + previous_code);
            int el = (curent_code - key);
                        
            while (el < 0) {
                el += (char)255;
            }

            message += (char) el;

        }

        public void decrypt() {
            test(0, fixcode);
            for (int i = 1; i < crypt_message.Length; i++) {
                test(i, message[i - 1]);
            }
            crypt_message = null;
        }

        public Encryption(char code) {
            fixcode = code;
        }

    }
}
