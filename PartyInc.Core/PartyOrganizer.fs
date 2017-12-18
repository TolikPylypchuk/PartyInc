module PartyInc.Core.PartyOrganizer

open System

open Chessie.ErrorHandling

[<CompiledName("HandleResponse")>]
let handleResponse (response : Response) =
    let intent = response.TopScoringIntent.Intent
    match intent with
    | "welcome" 
    | "start" ->
        ResponseChoices.partyOrganizer
        |> Trial.lift (fun choices ->
            choices
            |> Map.find intent
            |> List.map (fun list -> list.[Random().Next(list.Length)])
            |> List.reduce (sprintf "%s %s"))
    | _ ->
        "Sorry. I didn't get you. Can you repeat, please?" |> ok
