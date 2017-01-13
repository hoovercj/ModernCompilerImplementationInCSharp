namespace ModernCompilerImplementation.Chapter1.SLP.Lib
{
    public abstract class Stm { }

    public class CompoundStm : Stm
    {
        public readonly Stm stm1, stm2;
        public CompoundStm(Stm s1, Stm s2) { stm1 = s1; stm2 = s2; }
    }

    public class AssignStm : Stm
    {
        public readonly string id; public readonly Exp exp;
        public AssignStm(string i, Exp e) { id = i; exp = e; }
    }

    public class PrintStm : Stm
    {
        public readonly ExpList exps;
        public PrintStm(ExpList e) { exps = e; }
    }

    public abstract class Exp { }

    public class IdExp : Exp
    {
        public readonly string id;
        public IdExp(string i) { id = i; }
    }

    public class NumExp : Exp
    {
        public readonly int num;
        public NumExp(int n) { num = n; }
    }

    public class OpExp : Exp
    {
        public readonly Exp left, right; public readonly int oper;
        public const int Plus = 1, Minus = 2, Times = 3, Div = 4;
        public OpExp(Exp l, int o, Exp r) { left = l; oper = o; right = r; }
    }

    public class EseqExp : Exp
    {
        public readonly Stm stm; public readonly Exp exp;
        public EseqExp(Stm s, Exp e) { stm = s; exp = e; }
    }

    public abstract class ExpList { }

    public class PairExpList : ExpList
    {
        public readonly Exp head; public readonly ExpList tail;
        public PairExpList(Exp h, ExpList t) { head = h; tail = t; }
    }

    public class LastExpList : ExpList
    {
        public readonly Exp head;
        public LastExpList(Exp h) { head = h; }
    }
}