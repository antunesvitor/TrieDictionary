using System;
using System.Collections.Generic;
using System.Linq;

namespace DicionarioTrie.Trie
{
    public class TrieNode
    {

        public char letra { get; set; }
        public long count { get; set; }
        public List<TrieNode> filhos { get; set; }
        public bool ehPalavra { get; set; }
        
        public TrieNode(char letra)
        {
            this.letra = letra;

            //ou = zero????
            count = 1;

            filhos = new List<TrieNode>();
        }

        //Construtor da raiz
        public TrieNode(){
            count = 0;
            filhos = new List<TrieNode>();
        }

        public void AdicionarPalavra(string palavra){

            //verifica se é o ultimo nó, se for define-se a si msm como palavra
            if(palavra.Length == 0){
                this.ehPalavra = true;
            }
            //caso contrário será um nó parte de uma nova palavra
            else{
                this.count++;

                var palavraArray = palavra.ToCharArray();

                var filhoEleito = obterFilhoComLetra(palavraArray[0]);

                if(filhoEleito == null)
                {
                    TrieNode novoFilho = new TrieNode(palavraArray[0]);
                    this.filhos.Add(novoFilho);
                    novoFilho.AdicionarPalavra(palavra.Substring(1, palavra.Length - 1));
                }
                else
                {
                    filhoEleito.AdicionarPalavra(palavra.Substring(1, palavra.Length - 1));
                }
            }

        }

        private TrieNode obterFilhoComLetra(char letra){
            return filhos.Where(x=>x.letra == letra).FirstOrDefault();
        }

        //verifica se palavra está no dicionário
        public bool procurarPalavra(string palavra){

            if(palavra.Length == 0){
                if(this.ehPalavra)
                    return true;
                else{
                    Console.WriteLine("Não sou palavra");
                    return false;
                }
            }
            
            var palavraArray = palavra.ToCharArray();

            var filhoEleito = obterFilhoComLetra(palavraArray[0]);

            if (filhoEleito == null){
                return false;
            }
            else{
                string resto = palavra.Substring(1, palavra.Length - 1);
                return filhoEleito.procurarPalavra(resto);
            }
        }
    }      
}