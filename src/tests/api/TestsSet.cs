namespace sonlang.tests;

public class TestsSet {
    private readonly Test[] _tests;
    
    
    public TestsSet(Test[] tests) {
        _tests = tests;
    }

    public void Run() {
        try {
            var passedCount = 0;

            for (var i = 0; i < _tests.Length; i++) {
                var test = _tests[i];
                Console.Write($"test {(i + 1).ToString($"D{_tests.Length.ToString().Length}")} - ");
                var result = _tests[i].Run();
                
                if (result.Passed) {
                    ++passedCount;

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("OK");
                }
                else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(result.Error);
                }
                
                Console.ForegroundColor = ConsoleColor.Gray;
                if (result.Passed) continue;

                foreach (var cmd in test.Cmds) {
                    Console.WriteLine($"{cmd.Key}");
                }
            }

            var allPassed = passedCount == _tests.Length;
            Console.ForegroundColor = allPassed ? ConsoleColor.Green : ConsoleColor.Red;

            Console.WriteLine(allPassed ? "All tests passed." : $"{passedCount} of {_tests.Length} tests passed.");
        }
        catch (Exception e) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Exception: {e.Message}");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}