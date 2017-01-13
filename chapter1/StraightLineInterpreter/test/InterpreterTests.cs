using System;
using Xunit;

namespace ModernCompilerImplementation.Chapter1.SLP.Test
{
    using ModernCompilerImplementation.Chapter1.SLP.Lib;

    public class InterpreterTests
    {
        [Fact]
        public void TestInterpret()
        {
            var interpreter = new Interpreter();

            var interpretProgram1ActualOutput = interpreter.Interpret(Program1.Program);
            var interpretProgram1ExpectedOutput = "6 54";
            Assert.Equal(interpretProgram1ExpectedOutput, interpretProgram1ActualOutput);
            
            var interpretProgram2ActualOutput = interpreter.Interpret(Program2.Program);
            var interpretProgram2ExpectedOutput = "3\n6 9 6";
            Assert.Equal(interpretProgram2ExpectedOutput, interpretProgram2ActualOutput);

            var interpretProgram3ActualOutput = interpreter.Interpret(Program3.Program);
            var interpretProgram3ExpectedOutput = "7 14 8\n8";
            Assert.Equal(interpretProgram3ExpectedOutput, interpretProgram3ActualOutput);
        }
    }
}
