using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public void AdicionarArrayPalavras(string [] palavras)
        {
            foreach (var palavra in palavras)
            {
                AdicionarPalavra(palavra);
            }
        }

        public void AdicionarPalavra(string palavra){

            //verifica se é o ultimo nó, se for define-se a si msm como palavra
            if(palavra.Length == 0){
                this.ehPalavra = true;
            }
            //caso contrário será um nó parte de uma nova palavra
            else{
                this.count++;

                char charAtual = palavra[0];

                var filhoEleito = obterFilhoComLetra(charAtual);

                if(filhoEleito == null)
                {
                    TrieNode novoFilho = new TrieNode(charAtual);
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
            return filhos.Where(x => x.letra == letra).FirstOrDefault();
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
            
            char charAtual = palavra[0];

            var filhoEleito = obterFilhoComLetra(charAtual);

            if (filhoEleito == null){
                return false;
            }
            else{
                string resto = palavra.Substring(1, palavra.Length - 1);
                return filhoEleito.procurarPalavra(resto);
            }
        }

        public void autoCompletar (string prefixo, int limite){

            _autoCompletar(prefixo, limite, "");
        }

        private void _autoCompletar(string prefixoRestante, int limite, string prefixoSomado){
            
            if(prefixoRestante.Length == 0){

                prefixoSomado = prefixoSomado.Remove(prefixoSomado.Length - 1);
                this._imprimeFilhos(prefixoSomado, limite);
                return;
            }

            //Se houver letras no prefixo prossiga percorrendo a trie
            char charAtual = prefixoRestante[0];

            TrieNode filhoEleito = obterFilhoComLetra(charAtual);

            //se não encontrar o caminho do prefixo na arvore, o prefixo não existe 
            if(filhoEleito == null){
                Console.WriteLine("Não encontrado");
                return;
            }

            else 
            {
                string novoPrefixo = prefixoRestante.Substring(1, prefixoRestante.Length - 1);

                filhoEleito._autoCompletar(novoPrefixo, limite, prefixoSomado + filhoEleito.letra);
            }
                
        }

        private int _imprimeFilhos(string prefixo, int limite){

            if(ehPalavra) {
                Console.WriteLine(prefixo + letra);
                limite --;
            }

            if(limite == 0)
                return 0;

            foreach (var filho in filhos){

                limite = filho._imprimeFilhos(prefixo + letra, limite);
                if(limite == 0){
                    return 0;
                }
            }

            return limite;
        }

        public void sugerirPalavras (string palavra, int limite)
        {
            _sugerirPalavras (palavra, "", limite);
        }

        private void _sugerirPalavras(string sufixo, string prefixo, int limite)
        {
            if(sufixo.Length == 0 && ehPalavra){
                Console.WriteLine(prefixo);
                limite --;
                return;
            }
            else if(sufixo.Length == 0){
                return;
            }
            char charAtual = sufixo[0];
            
            TrieNode filhoAtual = obterFilhoComLetra(charAtual);

            //remove primeiro char de sufixo
            string novoSufixo = sufixo.Substring (1, sufixo.Length - 1);

            // Se tiver filho correspondente então prossegue procurando na Trie
            // Caso não encontre filho correspondente, ignora o charAtual, usa o novo sufixo e não acrescenta no prefixo o charAtual
            if(filhoAtual != null){

                string novoPrefixo = prefixo + charAtual;

                filhoAtual._sugerirPalavras (novoSufixo, novoPrefixo, limite);
            }

            else{

                prefixo = prefixo.Remove(prefixo.Length - 1);
                _imprimeFilhos(prefixo, limite);
            }
        }

        public void corrigirPalavra (string palavra)
        {
            _sugerirPalavras(palavra, "", 1);
        }
    }      
}