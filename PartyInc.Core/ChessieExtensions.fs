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
