using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace crypt_lab_04 {

    class Recipient {

        private long p; // открытый ключ, большое простое число
        private long q; // открытый ключ, большое простое число
        public long n; // модуль, p*q
        private long fn_euler; // ф-ция Эйлера
        public long e; // закрытый ключ, открытая экспонента
        private long d; // закрытый ключ, закрытая экспонента
        
        public string start_message = "";

        public void toGenerate() {
            Random rand = new Random();
            long start_digit = rand.Next(10, 100);
            p = toGenerateSimple(start_digit);
            q = toGenerateSimple(p + 1); // берем за стартовый символ число p+1, для того чтобы не совпали значения p и q
            n = p * q;
            fn_euler = (p - 1) * (q - 1);
            toGenerate_E();
            toGenerate_D();
            return;
        }

        public void toDecrypt(List<string> message) {
                        
            foreach (string symbol in message) {
                BigInteger encode = new BigInteger(Convert.ToDouble(symbol));
                encode = BigInteger.Pow(encode, (int)d);
                BigInteger N = new BigInteger(n);
                encode = encode % N;
                start_message += (char)encode;
            }

            return;

        }

        private long toGenerateSimple(long start_digit) {
            bool isSimple = false; 
            while (!isSimple) {
                start_digit++;
                isSimple = true;
                for (long i = 2; i < start_digit; i++)
                    if (start_digit % i == 0) {
                        isSimple = false; // если это число не простое isSimple = false
                        break;
                    }
            }
            return start_digit;
        }

        private void toGenerate_E() {
            e = 2;
            for (int i = 2; i <= e; i++)
                if ((fn_euler % i == 0) && (e % i == 0)) { // находим число e взимнопростое c fn_euler 
                    e++; i = 2;
                }
        }

        private void toGenerate_D() {
            d = 1;
            while (Convert.ToBoolean(d++)) {
                if ((e * d) % fn_euler == 1) 
                    break;
            }
        }

    }
}
