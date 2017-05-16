using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypt_lab_01 {
    class Alphabet {

        private string text;
        private HashSet<char> alphabet = new HashSet<char>();
        private Dictionary<char, int> dictionary = new Dictionary<char, int>();        

        // преобразование текста
        public void castTo() {
            text = text.Replace(".", "");
            text = text.Replace(",", "");
            text = text.Replace("!", "");
            text = text.Replace("?", "");
            text = text.Replace("-", "");
            text = text.Replace(" ", "");
            text = text.ToLower();
            text = text.Replace(Environment.NewLine, "");
        }
        
        /*
        // записываем все символы алфавита (неповторяющиеся) в массив
        public void toArray() {
            for (int i = 0; i < text.Length; i++) {
                alphabet.Add(text.ElementAt(i));
            }
        }
        */

        // подсчет количества совпадений одного и того же символа алфавита в тексте
        public void toArrayCount() {

            for (int i = 0; i < text.Length; i++) {
                bool yes = dictionary.ContainsKey(text[i]);
                if (!yes) {
                    dictionary.Add(text[i], 0);
                }
                dictionary[text[i]]++;
            }
            dictionary = dictionary.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
        }
        
        // возвращает массив из N наименее встречающихся символов        
        public char[] getLetters(int count) {
            char[] symbols = new char[count];
            
            for (int i = 0; i < count; i++) {
                symbols[i] = dictionary.ElementAt(i).Key;
            }
                        
            return symbols;
        }
        
        public double getEntropy() {
            double result = 0;

            foreach (KeyValuePair<char, int> element in dictionary) {
                double probability = (double)element.Value / (double)text.Length;  // находим вероятность символа element
                result -= probability * Math.Log(probability) / Math.Log(2);
            }

            return result;
        }

        public string getText() {
            return text;
        }

        public Alphabet(string txt) {
            text = txt;
        }

    }
}
