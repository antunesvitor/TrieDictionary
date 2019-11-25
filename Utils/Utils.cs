
using System.IO;

namespace DicionarioTrie.Utils{
    public class ReadFromStream{
        public static string[] readAllLines(StreamReader reader){

            string line = string.Empty;

            while(reader.ReadLine() != null){
                line += reader.ReadLine() + "\n";
            }

            return line.Split("\n");
        }
    }
}