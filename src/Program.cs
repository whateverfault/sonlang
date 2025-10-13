using System.Text;
using sonlanglib.interpreter;
using sonlanglib.interpreter.error;

namespace sonlang;

internal static class Program {
    static void Main(string[] args) {
        if (args.Length <= 0) {
            Shell();
        }
    }

    private static void Shell() {
        Console.InputEncoding  = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;
        
        var interpreter = new Interpreter();
        
        while (true) {
            Console.Write(">>> ");
            var line = Console.ReadLine();
            if (string.IsNullOrEmpty(line)) continue;
            
            if (line.Equals("quit") || line.Equals("exit")) break;
            if (line.Equals("clear")) {
                Console.Clear(); continue;
            }
            
            var result = interpreter.Evaluate(line);
            if (!result.Ok) {
                Console.WriteLine(result.Error);
                continue;
            } if (result.Value == null) {
                Console.WriteLine(Errors.GetErrorString(Error.SmthWentWrong));
                continue;
            }
            
            Console.WriteLine(result.Value.Value.Val);
        }
    }
}