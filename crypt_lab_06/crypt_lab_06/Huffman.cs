using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypt_lab_06 {
    class Huffman {
        private string start_message;
        private string crypt_message;
        private string test;

        private Dictionary<char, int> list_probability = new Dictionary<char, int>();  // таблица вероятности (int - количество поторений символа)
        private Dictionary<object, int> tree = new Dictionary<object, int>();  // дерево
        private Dictionary<char, string> leaves = new Dictionary<char, string>();  // листья
        private Dictionary<string, char> tree_reverse = new Dictionary<string, char>();  // словарь с обратными ключами и значениями, для расшифровки

        public void toEncrypt() {  // шифрование
            countProbability();
            buildTree();
            encryptMessage();
            return;
        }

        private void countProbability() {  // подсчет количества выпадения каждой буквы в строке
            foreach (char symbol in start_message) {
                if (!list_probability.ContainsKey(symbol)) {
                    list_probability.Add(symbol, 1);
                } else {
                    list_probability[symbol] = list_probability[symbol] + 1;
                }
            }
            list_probability = list_probability.OrderBy(s => s.Value).ToDictionary(s => s.Key, p => p.Value);
            foreach (KeyValuePair<char, int> pair in list_probability) {
                tree.Add(pair.Key, pair.Value);
                leaves.Add(pair.Key, "");
            }
        }

        private void buildTree() {  // построение дерева: удаляем 2 элемента дерева с наименьшей вероятностью и добавляем новый объект (их сумму))
            object a = tree.ElementAt(0).Key;
            object b = tree.ElementAt(1).Key;

            object c = new[] { a, b };
            int count = tree[a] + tree[b];

            buildCode(a, '0');
            buildCode(b, '1');

            tree.Add(c, count);
            tree.Remove(a);
            tree.Remove(b);

            tree = tree.OrderBy(s => s.Value).ToDictionary(s => s.Key, p => p.Value);

            if (tree.Count > 1)
                buildTree();

            return;
        }

        private void buildCode(object element, char code) {  // создание таблицы для шифрования (для каждой буквы с каждым шагом добавляется 1|0)
            if (element.GetType().IsClass) {
                object[] obj = (object[])element;
                buildCode(obj[0], code);
                buildCode(obj[1], code);
            }
            else {
                leaves[(char)element] = code + leaves[(char)element];
            }
        }

        private void encryptMessage() {
            crypt_message = "";
            foreach (char symbol in start_message) {
                crypt_message += leaves[symbol];
            }
        }

        public void toDecrypt() {  // расшифровка
            start_message = "";
            string code = "";
            tree_reverse = leaves.ToDictionary(s => s.Value, p => p.Key);
            for (int i = 0; i < crypt_message.Length; i++) {
                code += crypt_message[i];
                if (tree_reverse.ContainsKey(code)) {
                    start_message += tree_reverse[code];
                    code = "";
                }
            }
        }

        public Huffman(string message) {
            start_message = message;
        }

        public string getCryptMessage() {
            return crypt_message;
        }

        public string getStartMessage() {
            return start_message;
        }

        public double countCompression() {
            return (double)(crypt_message.Length) / (double)(start_message.Length * 8);
        }
    }
}