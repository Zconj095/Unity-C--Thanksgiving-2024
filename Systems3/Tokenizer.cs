using System.Collections.Generic;

public class Tokenizer
{
    private Dictionary<string, int> vocabulary = new Dictionary<string, int>();
    private int vocabIndex = 0;

    public int[] Tokenize(string input)
    {
        string[] words = input.ToLower().Split(' ');
        List<int> tokens = new List<int>();

        foreach (var word in words)
        {
            if (!vocabulary.ContainsKey(word))
            {
                vocabulary[word] = vocabIndex++;
            }
            tokens.Add(vocabulary[word]);
        }

        return tokens.ToArray();
    }

    public string Detokenize(int[] tokens)
    {
        Dictionary<int, string> reverseVocab = new Dictionary<int, string>();
        foreach (var pair in vocabulary)
        {
            reverseVocab[pair.Value] = pair.Key;
        }

        string result = "";
        foreach (int token in tokens)
        {
            result += reverseVocab[token] + " ";
        }

        return result.Trim();
    }
}
