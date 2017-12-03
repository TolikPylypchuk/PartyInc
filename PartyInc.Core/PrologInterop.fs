module PartyInc.Core.PrologInterop

open Chessie.ErrorHandling

open Prolog

let getSolutions (prolog : PrologEngine) file query =
    let solutions = prolog.GetAllSolutions(file, query)

    if solutions.Success
    then solutions.NextSolution |> Seq.toList |> ok
    else solutions.ErrMsg |> fail

let getVariables (solution : Solution) =
    solution.NextVariable
    |> Seq.filter (fun var -> var.Name <> var.Value)
    |> Seq.toList
