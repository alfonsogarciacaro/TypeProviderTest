# Testing Type Providers (erased) with netcore/netstandard

Build TypeProviderTest

```shell
dotnet build TypeProviderTest
```

Run Console project with netcore compiler (correctly prints "Hello World").

```shell
cd Console
dotnet run
cd ..
```

Parse AST with FCS 20.0.1 (doesn't show "Hello World").

```shell
cd ASTViewer
dotnet run ../Console/Console.fsproj
```

Update to FCS 21.0.1, correctly shows "Hello World" in the AST :)

```shell
dotnet add package FSharp.Compiler.Service -v 21.0.1
dotnet run ../Console/Console.fsproj
```
