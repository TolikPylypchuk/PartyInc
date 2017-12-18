module PartyInc.Core.SweetsOrderConsultant

open System

open Chessie.ErrorHandling

[<CompiledName("HandleResponse")>]
let handleResponse (response : Response) =
    let intent = response.TopScoringIntent.Intent
    match intent with
    | "order.all.price"
    | "order.cake"
    | "order.cake.preferences-no" 
    | "order.cake.preferences-yes"
    | "order.cake.preferences-yes-dislikes"
    | "order.cake.preferences-yes-likes"
    | "order.cake.preferences-yes-misunderstanding"
    | "order.cake.specify"
    | "order.stop"
    | "order.sweets"
    | "order.sweets.specify"
    | "order.sweets.weight"
    | "welcome" ->
        ResponseChoices.sweetsOrderConsultant
        |> Trial.lift (fun choices ->
            choices
            |> Map.find intent
            |> List.map (fun list -> list.[Random().Next(list.Length)])
            |> List.reduce (sprintf "%s %s"))
    | _ ->
        "Sorry. I didn't get you. Can you repeat, please?" |> ok
