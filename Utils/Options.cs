using   MatthiWare.CommandLine.Core.Attributes;

namespace DicionarioTrie.Utils
{
    public class Options
    {
        [Name("c", "completar"), Description("Completa o sufixo inserido")]
        public string completar { get; set; }

        [Name("s", "sugerir"), Description("Tras palavras semelhantes com a inserida")]
        public string sugerir { get; set; }

        [Name("C", "corrigir"), Description("Corrige a palavra inserida")]
        public string corrigir { get; set; }

        [Name("l", "limite"), Description("limite de palavras inseridas")]
        public int limite { get; set; }
    }
}