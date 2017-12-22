module PartyInc.Core.PrologInterop

open Chessie.ErrorHandling
open Prolog

[<CompiledName("GetSolutions")>]
let getSolutions (prolog : PrologEngine) file query = async {
    let solutions = prolog.GetAllSolutions(file, query)

    return
        if not solutions.HasError
        then solutions.NextSolution |> Seq.toList |> ok
        else solutions.ErrMsg |> fail
}

[<CompiledName("GetVariables")>]
let getVariables (solution : Solution) =
    solution.NextVariable
    |> Seq.filter (fun var -> var.Name <> var.Value)
    |> Seq.toList
    
let formatList list =
    sprintf "[ %s ]" (list |> List.reduce (sprintf "%s, %s"))
