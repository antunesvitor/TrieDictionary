using System;
using DicionarioTrie.Trie;

namespace DicionarioTrie
{
    class Program
    {
        static void Main(string[] args)
        {
            TrieNode trie = new TrieNode();

            Console.WriteLine("Adicione palavras a vontade, quando quiser parar aperte Enter sem digitar nada");
            string input = Console.ReadLine();

            if(!string.IsNullOrWhiteSpace(input)){
                trie.AdicionarPalavra(input);
            }
            else return;

            while(!string.IsNullOrWhiteSpace(input)){
                input = Console.ReadLine();
                trie.AdicionarPalavra(input);
            }   

            Console.WriteLine("procure por uma palavra");

            input = Console.ReadLine();

            var existeNoDic = trie.procurarPalavra(input);

            if(existeNoDic)
                Console.WriteLine("Existe");
            
            else Console.WriteLine("ñ existe");
        }
    }
}
