module Chessie.ErrorHandling.Trial

open System

let exnToMessage<'T> (result : Result<'T, exn>) =

    let rec exnToMessageInner (exn : Exception) =
        match exn with
        | :? AggregateException as exn ->
            exn.InnerExceptions
            |> Seq.map exnToMessageInner
            |> Seq.reduce (sprintf "%s; %s")
        | exn -> exn.Message

    result |> Trial.mapFailure (List.map exnToMessageInner)

let catch f = Trial.Catch f >> exnToMessage

let ofChoice choice =
    match choice with
    | Choice1Of2 result -> result |> ok
    | Choice2Of2 error -> error |> fail
 
let traverse f list =
    
    let folder head tail =
        f head >>= (fun h ->
            tail >>= (fun t ->
                List.Cons(h, t) |> ok))

    List.foldBack folder list ([] |> ok)

let sequence list = traverse id list

