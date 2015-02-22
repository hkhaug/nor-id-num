using System.Collections.Generic;

namespace NinCmd
{
    class Program
    {
        static int Main(string[] args)
        {
            Queue<string> parameters = new Queue<string>(args);
            Parser parser = new Parser(parameters);
            int result = parser.Parse();
            return result;
        }
    }
}
