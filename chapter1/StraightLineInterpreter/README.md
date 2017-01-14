## Chapter 1: Straight Line Program Interpreter

### .Net Core Project
The exercise implementation is in `/src` and is a .Net Core library project.

The test implentation is in `/test` and is a .Net Core XUnit project.

Use `dotnet restore` and `dotnet build` in each project folder after cloning the repository.

Use `dotnet test` in the `/test` directory to run the tests.

### Instructions

1. Write a Java function int maxargs(Stm s) that tells the maximum number
of arguments of any print statement within any subexpression of a given
statement. For example, maxargs(prog) is 2.

2. Write a Java function void interp(Stm s) that “interprets” a program
in this language. To write in a “functional programming” style – in which
you never use an assignment statement – initialize each local variable as you
declare it.