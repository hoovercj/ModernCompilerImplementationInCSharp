using Xunit;

namespace ModernCompilerImplementation.Chapter1.SLP.Test
{
    using ModernCompilerImplementation.Chapter1.SLP.Lib;

    public class MaxargsCalculatorTests
    {
        [Fact]
        public void TestMaxArgs() 
        {
            var maxargsCalculator = new MaxargsCalculator();

            var maxArgsProgram1 = maxargsCalculator.Maxargs(Program1.Program);
            Assert.Equal(2, maxArgsProgram1);
            
            var maxArgsProgram2 = maxargsCalculator.Maxargs(Program2.Program);
            Assert.Equal(3, maxArgsProgram2);

            var maxArgsProgram3 = maxargsCalculator.Maxargs(Program3.Program);
            Assert.Equal(2, maxArgsProgram3);
        }
    }
}
