using System.Text;
using sonlang.tests;
using sonlanglib.interpreter;
using sonlanglib.interpreter.error;

namespace sonlang;

internal static class Program {
    static void Main(string[] args) {
        Console.InputEncoding  = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;
        
        if (args.Length <= 0) {
            Shell();
        } else if (args.Contains("--test")) {
            Test();
        }
    }

    private static void Shell() {
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
            
            Console.WriteLine(result.Value);
        }
    }

    private static void Test() {
        var tests = new TestsSet(
                               [
                                   new Test(
                                            new Dictionary<string, string> {
                                                                               { "2+2", "4" },
                                                                           }
                                           ),
                                   new Test(
                                            new Dictionary<string, string> {
                                                                               { "2-2", "0" },
                                                                           }
                                           ),
                                   new Test(
                                            new Dictionary<string, string> {
                                                                               { "2-3", "-1" },
                                                                           }
                                           ),
                                   new Test(
                                            new Dictionary<string, string> {
                                                                               { "2*6", "12" },
                                                                           }
                                           ),
                                   new Test(
                                            new Dictionary<string, string> {
                                                                               { "2^6", "64" },
                                                                           }
                                           ),
                                   new Test(
                                            new Dictionary<string, string> {
                                                                               { "2*2^6", "128" },
                                                                           }
                                           ),
                                   new Test(
                                            new Dictionary<string, string> {
                                                                               { "4^(1/2)", "2" },
                                                                           }
                                           ),
                                   new Test(
                                            new Dictionary<string, string> {
                                                                               { "2+2*2", "6" },
                                                                           }
                                           ),
                                   new Test(
                                            new Dictionary<string, string> {
                                                                               { "-69", "-69" },
                                                                           }
                                           ),
                                   new Test(
                                            new Dictionary<string, string> {
                                                                               { "(2+2)*2", "8" },
                                                                           }
                                           ),
                                   new Test(
                                            new Dictionary<string, string> {
                                                                               { "-(420)", "-420" },
                                                                           }
                                           ),
                                   new Test(
                                            new Dictionary<string, string> {
                                                                               { "[]", "[]" },
                                                                           }
                                           ),
                                   new Test(
                                            new Dictionary<string, string> {
                                                                               { "[1]", "[1]" },
                                                                           }
                                           ),
                                   new Test(
                                            new Dictionary<string, string> {
                                                                               { "[69, 420]", "[69, 420]" },
                                                                           }
                                           ),
                                   new Test(
                                            new Dictionary<string, string> {
                                                                               {
                                                                                   "[69, [420, \"aboba\"]]",
                                                                                   "[69, [420, \"aboba\"]]"
                                                                               },
                                                                           }
                                           ),
                                   new Test(
                                            new Dictionary<string, string> {
                                                                               { "a=2", "2" },
                                                                               { "a", "2" },
                                                                               { "a+67", "69" },
                                                                           }
                                           ),
                                   new Test(
                                            new Dictionary<string, string> {
                                                                               {
                                                                                   "a=[69, 420, 123, 321]",
                                                                                   "[69, 420, 123, 321]"
                                                                               },
                                                                               { "a[0]", "69" },
                                                                               { "a[1]", "420" },
                                                                           }
                                           ),
                                   new Test(
                                            new Dictionary<string, string> {
                                                                               { "a=\"aboba\"", "\"aboba\"" },
                                                                               { "a[3]", "\"b\"" },
                                                                               { "a[4]", "\"a\"" },
                                                                           }
                                           ),
                                   new Test(
                                            new Dictionary<string, string> {
                                                                               {
                                                                                   "[123",
                                                                                   Errors.GetErrorString(Error
                                                                                               .InvalidSyntax)
                                                                               },
                                                                           }
                                           ),
                                   new Test(
                                            new Dictionary<string, string> {
                                                                               {
                                                                                   "123]",
                                                                                   Errors.GetErrorString(Error
                                                                                               .InvalidSyntax)
                                                                               },
                                                                           }
                                           ),
                                   new Test(
                                            new Dictionary<string, string> {
                                                                               {
                                                                                   "(123",
                                                                                   Errors.GetErrorString(Error
                                                                                               .InvalidSyntax)
                                                                               },
                                                                           }
                                           ),
                                   new Test(
                                            new Dictionary<string, string> {
                                                                               {
                                                                                   "123)",
                                                                                   Errors.GetErrorString(Error
                                                                                               .InvalidSyntax)
                                                                               },
                                                                           }
                                           ),
                               ]
                              );

        tests.Run();
    }
}