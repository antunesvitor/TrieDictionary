using System;
using DicionarioTrie.Trie;
using MatthiWare.CommandLine;
using DicionarioTrie.Utils;

namespace DicionarioTrie
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new CommandLineParser<Options>();

            var result = parser.Parse(args);

            if (result.HasErrors)
            {
                Console.WriteLine("Argumentos invalidos");
                return;
            }

            var optionArgs = result.Result;

            TrieNode trie = new TrieNode();

            string dicio = "./dicionarios/pt";

            if (optionArgs.idioma != null)
            {
                dicio = $"./dicionarios/{optionArgs.idioma}";
            }


            if (optionArgs.completar != null)
            {
                char primeiraLetra = optionArgs.completar[0];
                string[] lines = System.IO.File.ReadAllLines($"{dicio}/{primeiraLetra}.txt");
                trie.AdicionarArrayPalavras(lines);

                trie.autoCompletar(optionArgs.completar, optionArgs.limite);
            }
            else if (optionArgs.sugerir != null)
            {
                char primeiraLetra = optionArgs.sugerir[0];
                string[] lines = System.IO.File.ReadAllLines($"{dicio}/{primeiraLetra}.txt");
                trie.AdicionarArrayPalavras(lines);

                trie.sugerirPalavras(optionArgs.sugerir, optionArgs.limite);
            }
            else if (optionArgs.corrigir != null)
            {
                char primeiraLetra = optionArgs.corrigir[0];
                string[] lines = System.IO.File.ReadAllLines($"{dicio}/{primeiraLetra}.txt");
                trie.AdicionarArrayPalavras(lines);

                trie.corrigirPalavra(optionArgs.corrigir);
            }
            else
            {
                Console.WriteLine("Não foi dado nenhum comando valido");
            }
        }
    }
}
