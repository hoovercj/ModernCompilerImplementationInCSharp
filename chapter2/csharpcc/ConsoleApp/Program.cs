namespace ModernCompilerImplementation.Chapter2.CSharpCC.App
{
    using System;
    using ModernCompilerImplementation.Chapter2.CSharpCC.Lib;

    public class Program
    {
        public static void Main(string[] args)
        {
            using (var stream = Console.In)
            {
                try
                {
                    var tokenManager = new MiniCSharpParserTokenManager(new SimpleCharStream(stream));

                    Token t = tokenManager.GetNextToken();
                    while (true)
                    {
                        Console.WriteLine(String.Format("{0}:{1}", KindToText(t.Kind), t.ToString()));
                        t = tokenManager.GetNextToken(); 
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public static string KindToText(int kind)
        {
            switch (kind)
            {
                case MiniCSharpParserConstants.DIGIT:
                    return "DIGIT";
                case MiniCSharpParserConstants.ID:
                    return "ID";
                case MiniCSharpParserConstants.IF:
                    return "IF";
                case MiniCSharpParserConstants.NUM:
                    return "NUM";
                case MiniCSharpParserConstants.REAL:
                    return "REAL";
                default:
                    return "UNKNOWN";
            }
        }
    }
}