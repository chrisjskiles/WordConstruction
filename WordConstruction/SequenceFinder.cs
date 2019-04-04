using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace WordConstruction {
    class SequenceFinder {
        static void Main(string[] args) {
            string filePath = String.Empty;

            //get path of file with word declarations
            Console.Write("Please enter file path: ");
            filePath = Console.ReadLine();

            //exit application if file does not exist
            if (!File.Exists(filePath)) {
                Console.WriteLine("No such file exists.");
                Console.Read();

                return;
            }

            List<string> words = new List<string>();

            using (StreamReader reader = new StreamReader(filePath)) {
                string line = String.Empty;
                while ((line = reader.ReadLine()) != null) {
                    words.Add(line);
                }
            }

            List<string> wordSequence = new List<string>();

            FindWordSequence(SeparateIntoLengths(words), ref wordSequence);

            foreach (string word in wordSequence) {
                Console.WriteLine(word);
            }

            Console.Read();
        }

        //find the longest sequence of words that can be formed following the rules in the prompt
        public static void FindWordSequence(Dictionary<int, List<string>> words, ref List<string> wordSequence) {
            int currentLength = wordSequence.Count;

            if (!words.ContainsKey(currentLength + 1)) {
                return;
            }

            List<string> longestSequence = new List<string>();

            foreach (string word in words[currentLength + 1]) {
                if (currentLength == 0 || StringsOffByOne(wordSequence[currentLength - 1], word)) {
                    List<string> wordSequenceCopy = new List<string>(wordSequence);
                    wordSequenceCopy.Add(word);

                    FindWordSequence(words, ref wordSequenceCopy);

                    if (wordSequenceCopy.Count > longestSequence.Count) {
                        longestSequence = wordSequenceCopy;
                    }
                }
            }

            if (longestSequence.Count > wordSequence.Count) {
                wordSequence = longestSequence;
            }
        }

        //determine whether 2 strings can be made equal by adding one character
        public static bool StringsOffByOne(string shortString, string longString) {
            //if the long string is not exactly one char longer they are not off by one
            if (shortString.Length + 1 != longString.Length) {
                return false;
            }

            //if the strings are the same save one character at the beginning or end they are off by one
            if (shortString == longString.Substring(0, shortString.Length) || shortString == longString.Substring(1)) {
                return true;
            }

            //we can start the loop at index 1 since we checked the beginning of the string already
            for (int i = 1; i < shortString.Length; ++i) {

            }

            return false;
        }

        //separate the list of words into lists where all lengths are equal
        public static Dictionary<int, List<string>> SeparateIntoLengths(List<string> words) {
            Dictionary<int, List<string>> separatedWords = new Dictionary<int, List<string>>();

            foreach (string word in words) {
                if (!separatedWords.ContainsKey(word.Length)) {
                    separatedWords.Add(word.Length, new List<string>());
                }

                separatedWords[word.Length].Add(word);
            }

            return separatedWords;
        }
    }
}
