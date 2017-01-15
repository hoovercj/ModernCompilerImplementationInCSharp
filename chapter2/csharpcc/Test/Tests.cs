using System;
using Xunit;
using ModernCompilerImplementation.Chapter2.CSharpCC.Lib;
using System.IO;
using System.Text;

namespace ModernCompilerImplementation.Chapter2.CSharpCC.Test
{
    public class Tests
    {
        [Fact]
        public void MiniCSharpLexerTests() 
        {
            var ifInput = "if";
            var idInput1 = "id";
            var idInput2 = ifInput + idInput1;
            var realInput = "123.456";
            var numInput = "123456";

            var program = String.Format("{0} {1} {2} {3} {4}", ifInput, idInput1, idInput2, realInput, numInput);

            using (var test_stream = new MemoryStream(Encoding.UTF8.GetBytes(program)))
            {
                var tokenManager = new MiniCSharpParserTokenManager(new SimpleCharStream(test_stream));
                Token ifToken = tokenManager.GetNextToken();
                Assert.Equal(ifInput, ifToken.Image);
                Assert.Equal(MiniCSharpParserConstants.IF, ifToken.Kind);

                Token idToken1 = tokenManager.GetNextToken();
                Assert.Equal(idInput1, idToken1.Image);
                Assert.Equal(MiniCSharpParserConstants.ID, idToken1.Kind);
                
                Token idToken2 = tokenManager.GetNextToken();
                Assert.Equal(idInput2, idToken2.Image);
                Assert.Equal(MiniCSharpParserConstants.ID, idToken2.Kind);
                
                Token realToken = tokenManager.GetNextToken();
                Assert.Equal(realInput, realToken.Image);
                Assert.Equal(MiniCSharpParserConstants.REAL, realToken.Kind);
                
                Token numToken = tokenManager.GetNextToken();
                Assert.Equal(numInput, numToken.Image);
                Assert.Equal(MiniCSharpParserConstants.NUM, numToken.Kind);
            }
        }
    }
}
