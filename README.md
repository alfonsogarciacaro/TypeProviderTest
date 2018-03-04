# Does FCS detect implicit constructors from .fsi signatures?

See https://github.com/fable-compiler/Fable/issues/571

```shell
dotnet restore Console
cd ASTViewer
dotnet run ../Console/Console.fsproj
```

The last line says `CALL to M.Test.( .ctor ) (constructor true, implicit true)` so it seems last FCS correctly identifies implicit constructors even if they come from an .fsi signature.