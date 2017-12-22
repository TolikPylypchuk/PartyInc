module PartyInc.Core.PrologDrinks

open Chessie.ErrorHandling
open Prolog

open PrologInterop
open DrinkParsers

[<CompiledName("GetDrink")>]
let getDrink prologSolution =
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
            sprintf "drink(%s, %s, %s)" nameVar.Value ingredientsVar.Value priceVar.Value
            |> parseDrink

let getDrinksByQuery query = async {
    let prolog = PrologEngine()

    let! solutions =
        getSolutions prolog "Data\\drinks.pl" query

    return trial {
        let! solutions = solutions

        return!
            solutions
            |> List.map getDrink
            |> Trial.sequence
    }
}

let getDrinksByName name =
    getDrinksByQuery (sprintf "getDrinkByName(\"%s\", drink(Name, Ingredients, Price))" name)

let getDrinksByMaxPrice price =
    getDrinksByQuery (sprintf "getDrinkByMaxPrice(%f, drink(Name, Ingredients, Price))" price)

let getDrinksByRangePrice minPrice maxPrice =
    getDrinksByQuery (sprintf "getDrinkByRangePrice(%f, %f, drink(Name, Ingredients, Price))"
                              minPrice
                              maxPrice)

let getDrinksByIngredients ingredients =
    getDrinksByQuery (sprintf "getDrinkByIngredients(%s, drink(Name, Ingredients, Price))"
                              (ingredients |> formatList))

let getDrinksBySomeIngredients ingredients =
    getDrinksByQuery (sprintf "getDrinkBySomeIngredients(%s, drink(Name, Ingredients, Price))"
                              (ingredients |> formatList))

let getDrinksByNameAndSomeIngredients name ingredients =
    getDrinksByQuery (sprintf "getDrinkByNameAndSomeIngredients(\"%s\", %s, drink(Name, Ingredients, Price))"
                              name
                              (ingredients |> formatList))

let formatDrink (drink : Drink) =
    sprintf "drink(\"%s\", %s, %s)"
            drink.Name
            (drink.Ingredients |> List.map (fun i -> sprintf "\"%s\"" i) |> formatList)
            (drink.Price.ToString("0.0"))
