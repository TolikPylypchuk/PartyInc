module PartyInc.Core.DrinkParsers

open FParsec

open Parsers

let pDrink =
    let pDrinkInner = parse {
        let! name = pStringInQuotes
        do! pComma
        let! ingredients = pList pStringInQuotes
        do! pComma
        let! price = pfloat

        return {
            Name = name
            Ingredients = ingredients
            Price = price |> decimal
        }
    }

    pPredicate "drink" pDrinkInner
    
let parseDrink = parseInput pDrink
