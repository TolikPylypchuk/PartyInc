module PartyInc.Core.PrologFood

open Chessie.ErrorHandling

open FoodParsers
open PrologInterop

[<CompiledName("GetCandy")>]
let getCandy prologSolution =
    let variables =
        prologSolution
        |> getVariables
        |> List.sortBy (fun var -> var.Name)
    
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
        prologSolution
        |> getVariables
        |> List.sortBy (fun var -> var.Name)
    
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

[<CompiledName("GetCake")>]
let getCake prologSolution =
    let variables =
        prologSolution
        |> getVariables
        |> List.sortBy (fun var -> var.Name)
    
    if variables.Length <> 3 then
        sprintf "Expected 3 variables, got %i" variables.Length |> fail
    else
        let ingredientsVar = variables.[0]
        let nameVar = variables.[1]
        let priceVar = variables.[2]

        if not (ingredientsVar.Name = "Ingredients" && nameVar.Name = "Name" &&
                priceVar.Name = "Price") then
            sprintf "Expected Ingredients, Name and Price, got %s, %s, and %s"
                    ingredientsVar.Name
                    nameVar.Name
                    priceVar.Name
            |> fail
        else
            sprintf "cake(%s, %s, %s)" nameVar.Value ingredientsVar.Value priceVar.Value
            |> parseCake
