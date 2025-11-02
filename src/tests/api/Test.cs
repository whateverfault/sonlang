using sonlanglib.interpreter;

namespace sonlang.tests;

public class Test {
    public readonly Dictionary<string, string> Cmds;
    
    
    public Test(Dictionary<string, string> cmds) {
        Cmds = cmds;
    }

    public TestResult Run() {
        try { 
            var interpreter = new Interpreter();
            var testResult = new TestResult();
            
            for (var i = 0; i < Cmds.Count; i++) {
                var (test, res) = Cmds.ElementAt(i);
                var result = interpreter.Evaluate(test);

                if (!result.Ok) testResult.Passed &= res.Equals(result.Error);
                else if (result.Value != null) testResult.Passed &= res.Equals(result.Value);
                else testResult.Passed = false;

                if (!testResult.Passed) { 
                    testResult.Error = result.Value == null? 
                                           $"Expected: {res}. Given: {result.Error}" : 
                                           $"Expected: {res}. Given: {result.Value}";
                    return testResult;
                }
            }
            
            return testResult;
        }catch (Exception e) { 
            return new TestResult(false, $"Exception: {e.Message}");
        }
    }
}