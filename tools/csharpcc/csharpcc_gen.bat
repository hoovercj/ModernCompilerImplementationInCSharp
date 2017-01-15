DEL %1\*.cs
%~dp0\csharpcc.exe -GENERATE_GENERICS=true -OUTPUT_DIRECTORY=%1 %2