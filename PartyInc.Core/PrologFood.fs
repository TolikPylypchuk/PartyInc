module PartyInc.Core.PrologFood

open Chessie.ErrorHandling

open FoodParsers
open PrologInterop

[<CompiledName("GetCandy")>]
let getCandy prologSolution =
    let variables =
        getVariables prologSolution
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
            sprintf "candy(%s, %s)" nameVar.Value priceVar.Value |> parseCandy
            
[<CompiledName("GetCookie")>]
let getCookie prologSolution =
    let variables =
        getVariables prologSolution
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
            sprintf "cookie(%s, %s)" nameVar.Value priceVar.Value |> parseCookie
