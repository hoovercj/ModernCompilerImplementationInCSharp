options {
  DEBUG_PARSER = true; // Set to false to turn off token printing
  STATIC=false;
}

PARSER_BEGIN(MiniCSharpParser)

	namespace ModernCompilerImplementation.Chapter2.CSharpCC.Lib;
	using System;

	class MiniCSharpParser
	{
	}
PARSER_END(MiniCSharpParser)

TOKEN : {
	  < IF: "if" >
	| < DIGIT: ["0"-"9"] >
	| < ID: ["a"-"z"] ( ["a"-"z"] | <DIGIT>)* >
	| < NUM: (<DIGIT>)+ >
	| < REAL: ((<DIGIT>)+ "." (<DIGIT>)*) | ( (<DIGIT>)* "." (<DIGIT>)+) >
}

SKIP: {
	  < "--" (~["\n", "\r"])* ("\n" | "\r" | "\r\n") >
	| " "
	| "\t"
	| "\n"
	| "\r"
}

void Start() :
{}
{	(<IF> | <ID> | <NUM> | <REAL>)*}