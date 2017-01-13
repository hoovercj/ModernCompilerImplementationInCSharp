namespace ModernCompilerImplementation.Chapter1.SLP.Lib
{
    using System;
    using System.Text;

    public class Interpreter
    {
        private StringBuilder output;

        /// <summary>
        /// 2. Write a Java function void interp(Stm s) that “interprets” a program
        /// in this language. To write in a “functional programming” style – in which
        /// you never use an assignment statement – initialize each local variable as you
        /// declare it.
        /// </summary>
        /// <param name="s">The root <see cref="Stm" /> node of the program to analyze</param>
        public string Interpret(Stm s)
        {
            output = new StringBuilder();
            var emptyTable = new Table(null, 0, null);
            interpStm(s, emptyTable);
            return output.ToString().Trim();
        }

        private Table interpStm(Stm s, Table t)
        {
            if (s is AssignStm)
            {
                var statement = s as AssignStm;
                var id = statement.id;
                var expressionResultAndTable = interpExp(statement.exp, t);
                var newValue = expressionResultAndTable.i;
                return update(expressionResultAndTable.t, id, newValue);
            }
            else if (s is CompoundStm)
            {
                var statement = s as CompoundStm;
                var subStatement1 = statement.stm1;
                var subStatement2 = statement.stm2;
                var intermediaryTable = interpStm(subStatement1, t);
                var finalTable = interpStm(subStatement2, intermediaryTable);
                return finalTable;
            }
            else if (s is PrintStm)
            {
                var statement = s as PrintStm;
                return interpPrint(statement.exps, t);
            }
            else
            {
                throw new ArgumentException("Encountered unknown statement type.");
            }
        }

        private IntAndTable interpExp(Exp e, Table t)
        {
            if (e is IdExp)
            {
                var expression = e as IdExp;
                return new IntAndTable(lookup(t, expression.id), t);
            }
            else if (e is NumExp)
            {
                var expression = e as NumExp;
                return new IntAndTable(expression.num, t);
            }
            else if (e is OpExp)
            {
                var expression = e as OpExp;
                var leftExpressionResultAndTable = interpExp(expression.left, t);
                var rightExpressionResultAndTable = interpExp(expression.right, leftExpressionResultAndTable.t);
                var operationResult = PerformOperation(leftExpressionResultAndTable.i, rightExpressionResultAndTable.i, expression.oper);
                return new IntAndTable(operationResult, rightExpressionResultAndTable.t);
            }
            else if (e is EseqExp)
            {
                var expression = e as EseqExp;
                var statementResult = interpStm(expression.stm, t);
                var expressionResultAndTable = interpExp(expression.exp, statementResult);
                return expressionResultAndTable;
            }
            else
            {
                throw new ArgumentException("Encountered unknown expression type.");
            }
        }

        private Table interpPrint(ExpList e, Table t)
        {
            if (e is PairExpList)
            {
                var expList = e as PairExpList;
                var expressionResultAndTable = interpExp(expList.head, t);
                Print(expressionResultAndTable.i + " ");
                return interpPrint(expList.tail, expressionResultAndTable.t);
            }
            else if (e is LastExpList)
            {
                var expList = e as LastExpList;
                var expressionResultAndTable = interpExp(expList.head, t);
                Print(expressionResultAndTable.i + "\n");
                return expressionResultAndTable.t;
            }
            else
            {
                throw new ArgumentException("Encountered unknown statement type.");
            }
        }

        /// <summary>
        /// Updates a value in a table by creating a new table
        /// and setting the old table as the tail
        /// </summary>
        /// <param name="table">The <see cref="Table" /> to update</param>
        /// <param name="id">The id to add/update in the table</param>
        /// <param name="value">The value to associate with the id</param>
        /// <returns>A new table with a reference to the old table as the tail</returns>
        private Table update(Table table, String id, int value)
        {
            if (table == null) { throw new ArgumentNullException(nameof(table)); }
            if (id == null) { throw new ArgumentNullException(nameof(id)); }

            return new Table(id, value, table);
        }


        /// <summary>
        /// Searches a table for the first node with the specified id
        /// </summary>
        /// <param name="table">The <see cref="Table" /> to search</param>
        /// <param name="id">The id to search for</param>
        /// <returns>The first value for a node with the specified id, or 0 if no node exists</returns>
        private int lookup(Table table, String id)
        {
            if (table == null) { throw new ArgumentNullException(nameof(table)); }

            if (table.id == id)
            {
                return table.value;
            }
            
            return table.tail == null ? 0 : lookup(table.tail, id);
        }

        private int PerformOperation(int left, int right, int operand)
        {
            switch (operand)
            {
                case OpExp.Plus:
                    return left + right;
                case OpExp.Minus:
                    return left - right;
                case OpExp.Times:
                    return left * right;
                case OpExp.Div:
                    return left / right;
                default:
                    throw new ArgumentException("Encountered unexpected operand");
            }
        }

        private void Print(string n)
        {
            Console.Write(n);
            output.Append(n);
        }
    }

    class Table {
        public String id; public int value; public Table tail;
        public Table(String i, int v, Table t) {id=i; value=v; tail=t;}
    }

    class IntAndTable {
        public int i; public Table t;
        public IntAndTable(int ii, Table tt) {i=ii; t=tt;}
    }
}