module PartyInc.Core.FoodParsers

open Chessie.ErrorHandling

open Parsers
open PrologInterop

let parseCandy candy =
    let variables =
        getVariables candy
        |> List.sortBy (fun var -> var.Name)
        |> List.toArray
    
    if variables.Length <> 2 then
        sprintf "Expected 2 variables, got %i" variables.Length |> fail
    else
        let nameVar = variables.[0]
        let priceVar = variables.[1]
        if not (nameVar.Name = "Name" && priceVar.Name = "Price") then
            sprintf "Expected Name and Price, got %s and %s" nameVar.Name priceVar.Name
            |> fail
        else
            failwith "Not implemented"
            