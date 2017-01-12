using System;
using Xunit;

namespace ModernCompilerImplementation.Chapter1.SLP.Test
{
    using ModernCompilerImplementation.Chapter1.SLP.Lib;

    public class InterpreterTests
    {
        [Fact]
        public void TestMaxArgs() 
        {
            var interpreter = new Interpreter();

            var maxArgsProgram1 = interpreter.Maxargs(Program1.Program);
            Assert.Equal(2, maxArgsProgram1);
            
            var maxArgsProgram2 = interpreter.Maxargs(Program2.Program);
            Assert.Equal(3, maxArgsProgram2);

            var maxArgsProgram3 = interpreter.Maxargs(Program3.Program);
            Assert.Equal(2, maxArgsProgram3);
        }

        [Fact]
        public void TestInterpret()
        {
            Assert.True(false, "Test not yet implemented.");
        }
    }
}
