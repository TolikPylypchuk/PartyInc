module DrinksOrderConsultant

open Chessie.ErrorHandling
open PartyInc.Core

[<CompiledName("HandleResponse")>]
let handleResponse (response, state) =
    let getResponse = ResponseChoices.getResponse ResponseChoices.drinksOrderConsultant

    let intent = response.TopScoringIntent.Intent

    match intent, state with
    | "order", DrinkEmptyOrder ->
        getResponse intent
        |> Trial.lift (fun response -> response, PriceAsked)
        |> async.Return
    | "custom_price", PriceAsked ->
        let price = 50
        getResponse intent
        |> Trial.lift (fun response -> response, DrinkTypeAsked (decimal(price)))
        |> async.Return
    | _ ->
        getResponse "None"
        |> Trial.lift (fun response -> response, state)
        |> async.Return