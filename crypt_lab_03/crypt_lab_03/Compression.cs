using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypt_lab_03 {
    class Compression {
        private List<string> matrix = new List<string>(); // циклическая матрица перестановок
        private List<string> list = new List<string>();
        private Dictionary<int, string> table  = new Dictionary<int, string>();
        private string start_message;
        private string crypt_message;
        private int key;
        
        public string getCryptMessage() {
            return crypt_message;
        }

        public string getStartMessage() {
            return start_message;
        }

        // сжатие
        public void toComression() {
            string line = start_message;
            matrix.Add(line);
            char last_char;

            for (int i = 0; i < line.Length-1; i++) {
                last_char = line.Last();
                line = last_char + line.Substring(0, line.Length - 1);
                matrix.Add(line);
            }

            matrix = matrix.OrderBy(value => value).ToList();
            key = matrix.IndexOf(start_message);

            for (int i = 0; i < matrix.Count; i++) {
                crypt_message += matrix.ElementAt(i).Last();
            }

            start_message = null;
            matrix.Clear();

            using (FileStream fstream = new FileStream("test.txt", FileMode.OpenOrCreate)) {
                byte[] array = System.Text.Encoding.Default.GetBytes(crypt_message);
                fstream.Write(array, 0, array.Length);
            }

            return;
        }

        // операция обратная сжатию
        public void toDecompression() {

            using (FileStream fstream = File.OpenRead("test.txt")) {
                byte[] mass = new byte[fstream.Length];
                fstream.Read(mass, 0, mass.Length);
                string crypt_message = System.Text.Encoding.Default.GetString(mass);
            }

            char[] array = crypt_message.ToCharArray();
            array = array.OrderBy(value => value).ToArray();
            
            for (int i = 0; i < crypt_message.Length; i++) {
                table.Add(i, crypt_message.ElementAt(i).ToString() + array[i].ToString());
                list.Add(table[i]);
            }

            list = list.OrderBy(value => value).ToList();

            for (int el = 0; el < table.Count - 2; el++) {
                for (int i = 0; i < table.Count; i++) {
                    table[i] += list[i].Last();
                    list[i] = table[i];
                }
                list = list.OrderBy(value => value).ToList();
            }

            start_message = list.ElementAt(key);

            crypt_message = null;
            table.Clear();
            list.Clear();

            return;
        }

        public Compression(string str) {
            start_message = str;
        }
    }
}
