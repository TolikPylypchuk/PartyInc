module PartyInc.Core.API

open Chessie.ErrorHandling

[<CompiledName("AsyncToTask")>]
let asyncToTask a = Async.StartAsTask a

[<CompiledName("GetResult")>]
let getResult result =
    match result with
    | Ok (result, _) ->
        result
    | Bad errors ->
        failwith (errors |> List.reduce (sprintf "%s; %s"))

[<CompiledName("GetAsyncResult")>]
let getAsyncResult result =
    result
    |> Async.map getResult
    |> Async.StartAsTask
