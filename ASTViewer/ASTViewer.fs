module ASTViewer

open System.IO
open Microsoft.FSharp.Compiler.SourceCodeServices

let parse (checker: FSharpChecker) projFile =
    let options =
        match Path.GetExtension(projFile) with
        | ".fsx" ->
            let projCode = File.ReadAllText projFile
            checker.GetProjectOptionsFromScript(projFile, projCode)
            |> Async.RunSynchronously
            |> fst
        | ".fsproj" ->
            let opts, _, _ = Fable.CLI.ProjectCoreCracker.GetProjectOptionsFromProjectFile(projFile)
            opts
        | ext -> failwithf "Unexpected extension: %s" ext
    options
    |> checker.ParseAndCheckProject
    |> Async.RunSynchronously

let rec printDecls prefix decls =
    decls |> Seq.iteri (fun i decl ->
        match decl with
        | FSharpImplementationFileDeclaration.Entity (e, sub) ->
            printfn "%s%i) ENTITY: %s" prefix i e.DisplayName
            printDecls (prefix + "\t") sub
        | FSharpImplementationFileDeclaration.MemberOrFunctionOrValue (meth, _args, body) ->
            if meth.IsCompilerGenerated |> not then
                printfn "%s%i) METHOD: %s" prefix i meth.FullName
                printfn "%A" body
        | FSharpImplementationFileDeclaration.InitAction (expr) ->
            printfn "%s%i) ACTION" prefix i
            printfn "%A" expr
        )

[<EntryPoint>]
let main argv =
    let checker = FSharpChecker.Create(keepAssemblyContents=true)
    let proj = parse checker argv.[0]
    for file in proj.AssemblyContents.ImplementationFiles do
        printfn "FILE: %s" file.FileName
        printDecls "" file.Declarations
    0
