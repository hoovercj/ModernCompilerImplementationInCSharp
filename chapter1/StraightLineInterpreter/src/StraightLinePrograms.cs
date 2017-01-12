

namespace ModernCompilerImplementation.Chapter1.SLP.Lib
{
    public static class Program1
    {
        // a := 7; b := (a := a+2, a)*(a := a-3, a); print(a, b)
        // Should produce the following output:
        //
        // maxargs result: 2
        // interpretation result: 6 54
        public static Stm Program =
            new CompoundStm(new AssignStm("a", new NumExp(7)),
            new CompoundStm(new AssignStm("b",
            new OpExp(new EseqExp(new AssignStm("a",
            new OpExp(new IdExp("a"), OpExp.Plus, new NumExp(2))), new IdExp("a")), OpExp.Times,
            new EseqExp(new AssignStm("a",
            new OpExp(new IdExp("a"), OpExp.Minus, new NumExp(3))), new IdExp("a")))),
            new PrintStm(new PairExpList(new IdExp("a"),
            new LastExpList(new IdExp("b"))))));
    }

    public static class Program2
    {
        // print((a := 3, (print((b := a-1, a)), b*3)), a*3, (c := a*b, c))
        // Should produce the following output:
        //
        // maxargs result: 3
        // interpretation result: 3
        // 6 9 6
        public static Stm Program =
            new PrintStm(
            new PairExpList(
            new EseqExp(
            new AssignStm("a", new NumExp(3)),
            new EseqExp(
            new PrintStm(
            new LastExpList(
            new EseqExp(
            new AssignStm("b", new OpExp(new IdExp("a"),
            OpExp.Minus,
            new NumExp(1))),
            new IdExp("a")))),
            new OpExp(new IdExp("b"), OpExp.Times, new NumExp(3)))),
            new PairExpList(
            new OpExp(new IdExp("a"), OpExp.Times, new NumExp(3)),
            new LastExpList(
            new EseqExp(
            new AssignStm("c", new OpExp(new IdExp("a"),
            OpExp.Times,
            new IdExp("b"))),
            new IdExp("c"))))));
    }

    public static class Program3 {
        // a := 7; print(a, (print(2*a, (a := a+1, a)), a))
        // Should produce the following output:
        //
        // maxargs result: 2
        // interpretation result: 7 14 8
        // 8
        public static Stm Program =
            new CompoundStm(new AssignStm("a", new NumExp(7)),
            new PrintStm(new PairExpList(new IdExp("a"),
            new LastExpList(new EseqExp(new PrintStm(new PairExpList(
            new OpExp(new NumExp(2), OpExp.Times, new IdExp("a")),
            new LastExpList(new EseqExp(new AssignStm("a",
            new OpExp(new IdExp("a"), OpExp.Plus, new NumExp(1))),
            new IdExp("a"))))), new IdExp("a"))))));
        }
}
