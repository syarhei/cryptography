using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypt_lab_05 {
    class ShannonFano {
        private string start_message;
        private string crypt_message;
        private Dictionary<char, int> list_probability = new Dictionary<char, int>();  // таблица вероятности (int - количество поторений символа)
        private Dictionary<char, string> tree = new Dictionary<char, string>();
        private Dictionary<string, char> tree_reverse = new Dictionary<string, char>();  // словарь с обратными ключами и значениями, для расшифровки

        public void toEncrypt() {
            countProbability();
            buildTree(tree, start_message.Length);
            encryptMessage();
            return;
        }

        public void toDecrypt() {
            start_message = "";
            string code = "";
            tree_reverse = tree.ToDictionary(s => s.Value, p => p.Key);
            for (int i = 0; i < crypt_message.Length; i++) {
                code += crypt_message[i];
                if (tree_reverse.ContainsKey(code)) {
                    start_message += tree_reverse[code];
                    code = "";
                }
            }
        }

        public double countCompression() {
            return (double) (crypt_message.Length) / (double) (start_message.Length * 8);
        }

        public string getCryptMessage() {
            return crypt_message;
        }

        public string getStartMessage() {
            return start_message;
        }

        private void countProbability() {
            foreach (char symbol in start_message) {
                if (!list_probability.ContainsKey(symbol)) {
                    list_probability.Add(symbol, 1);
                }
                else {
                    list_probability[symbol] = list_probability[symbol] + 1;
                }
            }
            list_probability = list_probability.OrderByDescending(s => s.Value).ToDictionary(s => s.Key, p => p.Value);
            foreach (KeyValuePair<char, int> pair in list_probability) {
                tree.Add(pair.Key, "");
            }
        }

        private void buildTree(Dictionary<char, string> block, int count_all) {
            Dictionary<char, string> left = new Dictionary<char, string>();
            Dictionary<char, string> right = new Dictionary<char, string>();
            Dictionary<char, string> test = new Dictionary<char, string>();
            int count_left = 0;
            for (int i = 0; i < block.Count; i++) {
                int count_left_prev = count_left;
                count_left += list_probability[block.ElementAt(i).Key];
                if (count_left > count_all - count_left) {
                    int difference_1 = Math.Abs(2 * count_left - count_all);
                    int difference_2 = Math.Abs(2 * count_left_prev - count_all);
                    if (difference_2 < difference_1) {
                        tree[block.ElementAt(i).Key] += "0";
                        addElement(ref right, block.ElementAt(i), "0");
                        count_left = count_left_prev;
                    } else {
                        tree[block.ElementAt(i).Key] += "1";
                        addElement(ref left, block.ElementAt(i), "1");
                    }
                    for (int p = i + 1; p < block.Count; p++) {
                        tree[block.ElementAt(p).Key] += "0";
                        addElement(ref right, block.ElementAt(p), "0");
                    }
                    break;
                } else {
                    tree[block.ElementAt(i).Key] += "1";
                    addElement(ref left, block.ElementAt(i), "1");
                }
            }
            if (left.Count > 1) buildTree(left, count_left);
            if (right.Count > 1) buildTree(right, count_all - count_left);
            return;
        }

        private void addElement(ref Dictionary<char, string> block, KeyValuePair<char, string> element, string digit) {
            if (block.ContainsKey(element.Key))
                block[element.Key] += digit;
            else
                block.Add(element.Key, element.Value);
        }

        private void encryptMessage() {
            crypt_message = "";
            foreach (char symbol in start_message) {
                crypt_message += tree[symbol];
            }
        }

        public ShannonFano(string message) {
            start_message = message;
        }
    }
}

/*      полная параша
        int count_left = 0, count_left_prev;
        int part = count_all / 2;
        int i;
        for (i = 0; count_left < part; i++) {
            count_left += list_probability.ElementAt(i).Value;
        }
        count_left_prev = count_left - list_probability.ElementAt(i-1).Value;
        int difference_current = Math.Abs(part - count_left);
        int difference_prev = Math.Abs(part - count_left_prev);
        if (difference_current > difference_prev) {
            i--;
        }
        for (int p = 0; p < block.Count; p++) {
            char symbol = (p < i) ? '1' : '0';
            tree[block.ElementAt(p).Key] += symbol;
            if (p < i)
                addElement(ref left, block.ElementAt(p), symbol.ToString());
            else
                addElement(ref right, block.ElementAt(p), symbol.ToString());

        }
        if (i > 1)
*/