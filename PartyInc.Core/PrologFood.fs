module PartyInc.Core.PrologFood

open Chessie.ErrorHandling
open Prolog

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

let getByQuery getter query = async {
    let prolog = PrologEngine()

    let! solutions =
        getSolutions prolog "Data\\food.pl" query

    return trial {
        let! solutions = solutions

        return!
            solutions
            |> List.map getter
            |> Trial.sequence
    }
}

let formatList ingredients =
    let rec formatIngredientsInner ingredients =
        match ingredients with
        | [] -> ""
        | [ ingredient ] -> ingredient
        | head :: tail -> sprintf "%s, %s" head (tail |> formatIngredientsInner)

    sprintf "[ %s ]" (ingredients |> formatIngredientsInner)

let getCandiesByQuery = getByQuery getCandy
let getCookiesByQuery = getByQuery getCookie
let getCakesByQuery = getByQuery getCake

let getCandiesByPriceMoreEqualThan price =
    getCandiesByQuery (sprintf "getCandyByPriceMoreEqualThan(%i, candy(Name, Price))" price)
    
let getCandiesByPriceLessThan price =
    getCandiesByQuery (sprintf "getCandyByPriceLessThan(%i, candy(Name, Price))" price)

let getCookiesByPriceMoreEqualThan price =
    getCookiesByQuery (sprintf "getCookieByPriceMoreEqualThan(%i, cookie(Name, Price))" price)

let getCookiesByPriceLessThan price =
    getCookiesByQuery (sprintf "getCookieByPriceLessThan(%i, cookie(Name, Price))" price)
    
let getCakesByPriceMoreEqualThan price =
    getCakesByQuery (sprintf "getCakeByPriceMoreEqualThan(%i, cake(Name, Ingredients, Price))" price)
  
let getCakesByPriceLessThan price =
    getCakesByQuery (sprintf "getCakeByPriceLessThan(%i, cake(Name, Ingredients, Price))" price)

let getCakesByIngredientsInclude ingredients =
    let format = ingredients |> List.map (fun i -> sprintf "\"%s\"" i) |> formatList
    getCakesByQuery (sprintf "getCakeByIngredientsInclude(%s, cake(Name, Ingredients, Price))" format)

let getCakesByIngredientsExclude ingredients =
    let format = ingredients |> List.map (fun i -> sprintf "\"%s\"" i) |> formatList
    getCakesByQuery (sprintf "getCakeByIngredientsExclude(%s, cake(Name, Ingredients, Price))" format)

let formatCake (cake : Cake) =
    sprintf "cake(\"%s\", %s, %s)"
            cake.Name
            (cake.Ingredients |> List.map (fun i -> sprintf "\"%s\"" i) |> formatList)
            (cake.Price.ToString("0.#"))

let formatCandy (Candy candy) =
    sprintf "candy(\"%s\", %s, %s)"
            candy.Name
            (candy.Price.ToString("0.0"))
            (candy.Weight.ToString("0.0"))

let formatCookie (Cookie cookie) =
    sprintf "cookie(\"%s\", %s, %s)"
            cookie.Name
            (cookie.Price.ToString("0.0"))
            (cookie.Weight.ToString("0.0"))
