namespace ModernCompilerImplementation.Chapter1.SLP.Lib
{
    using System;

    public class Interpreter
    {
        /// <summary>
        /// 1. Write a function int maxargs(Stm s) that tells the maximum number
        /// of arguments of any print statement within any subexpression of a given
        /// statement. For example, maxargs(prog) is 2.
        /// </summary>
        /// <param name="s">The root <see cref="Stm" /> node of the program to analyze</param>
        /// <returns>The maximum number of arguments of any print statement</returns>
        public int Maxargs(Stm s)
        {
            return MaxargsInStatement(s);
        }

        /// <summary>
        /// 2. Write a Java function void interp(Stm s) that “interprets” a program
        /// in this language. To write in a “functional programming” style – in which
        /// you never use an assignment statement – initialize each local variable as you
        /// declare it.
        /// </summary>
        /// <param name="s">The root <see cref="Stm" /> node of the program to analyze</param>
        public void Interpret(Stm s)
        {
            throw new NotImplementedException();
        }

        private int ExpListSize(ExpList e)
        {
            if (e is PairExpList)
            {
                var expList = e as PairExpList;
                return 1 + ExpListSize(expList.tail);
            }
            else if (e is LastExpList)
            {
                return 1;
            }
            else
            {
                throw new Exception("Encountered unknown ExpList type");
            }
        }

        private int MaxargsInExpressionList(ExpList e)
        {
            var expListSize = ExpListSize(e);

            if (e is PairExpList)
            {
                var expList = e as PairExpList;
                return Math.Max(
                    expListSize,
                    Math.Max(
                        MaxargsInExpression(expList.head),
                        MaxargsInExpressionList(expList.tail)
                    )
                );
            }
            else if (e is LastExpList)
            {
                var expList = e as LastExpList;
                return Math.Max(expListSize, MaxargsInExpression(expList.head));
            }
            else
            {
                throw new Exception("Encountered unknown statement type.");
            }
        }

        private int MaxargsInExpression(Exp e)
        {
            if (e is IdExp || e is NumExp)
            {
                return 0;
            }
            else if (e is OpExp)
            {
                var expression = e as OpExp;
                return Math.Max(MaxargsInExpression(expression.left), MaxargsInExpression(expression.right));
            }
            else if (e is EseqExp)
            {
                var expression = e as EseqExp;
                return Math.Max(MaxargsInExpression(expression.exp), MaxargsInStatement(expression.stm));
            }
            else
            {
                throw new Exception("Encountered unknown expression type.");
            }
        }

        private int MaxargsInStatement(Stm s)
        {
            if (s is AssignStm)
            {
                var statement = s as AssignStm;
                return MaxargsInExpression(statement.exp);
            }
            else if (s is CompoundStm)
            {
                var statement = s as CompoundStm;
                return Math.Max(MaxargsInStatement(statement.stm1), MaxargsInStatement(statement.stm2));
            }
            else if (s is PrintStm)
            {
                var statement = s as PrintStm;
                return MaxargsInExpressionList(statement.exps);
            }
            else
            {
                throw new Exception("Encountered unknown statement type.");
            }
        }
    }
}