namespace sonlang.tests;

public class TestResult {
    public bool Passed;
    public string Error;


    public TestResult() {
        Passed = true;
        Error = string.Empty;
    }
    
    public TestResult(bool passed, string error) {
        Passed = passed;
        Error = error;
    }
}