using System.Text;
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
        try {
            var tests = new Dictionary<string, string> {
                                                           { "2+2", "4" },
                                                           { "2-2", "0" },
                                                           { "2-3", "-1" },
                                                           { "2*6", "12" },
                                                           { "2^6", "64" },
                                                           { "2*2^6", "128" },
                                                           { "4^(1/2)", "2" },
                                                           { "2+2*2", "6" },
                                                           { "-69", "-69" },
                                                           { "(2+2)*2", "8" },
                                                           { "-(420)", "-420" },
                                                           { "[]", "[]" },
                                                           { "[1]", "[1]" },
                                                           { "[69, 420]", "[69, 420]" },
                                                           { "[69, [420, \"aboba\"]]", "[69, [420, \"aboba\"]]" },
                                                           { "a=2", "2" },
                                                           { "a=-2", "-2" },
                                                           { "[123", Errors.GetErrorString(Error.InvalidSyntax) },
                                                           { "123]", Errors.GetErrorString(Error.InvalidSyntax) },
                                                           { "(123", Errors.GetErrorString(Error.InvalidSyntax) },
                                                           { "123)", Errors.GetErrorString(Error.InvalidSyntax) },
                                                       };

            var interpreter = new Interpreter();
            var passedCount = 0;
            
            for (var i = 0; i < tests.Count; i++) {
                var (test, res) = tests.ElementAt(i);
                bool ok;

                Console.Write($"test {i + 1}: {test} ");
                
                var result = interpreter.Evaluate(test);

                if (!result.Ok) {
                    ok = res.Equals(result.Error);
                }
                else if (result.Value != null) {
                    ok = res.Equals(result.Value);
                }
                else {
                    ok = false;
                }

                if (ok) {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("- OK");
                    ++passedCount;
                }
                else {
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine(result.Value == null
                                          ? $"\nError:\nExpected: {res}. Given: {result.Error}"
                                          : $"\nError:\nExpected: {res}. Given: {result.Value}");
                }

                Console.ForegroundColor = ConsoleColor.Gray;
            }

            var allPassed = passedCount == tests.Count;
            Console.ForegroundColor = allPassed? 
                                          ConsoleColor.Green : 
                                          ConsoleColor.Red;

            Console.WriteLine(allPassed? 
                                  "All tests passed." : 
                                  $"{passedCount} of {tests.Count} tests passed.");
        }catch (Exception e) { 
            Console.WriteLine($"\nException: {e.Message}");
        }
    }
}