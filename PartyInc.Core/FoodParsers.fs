module PartyInc.Core.FoodParsers

open FParsec

open Parsers

let private pSweetInner =
    let pSweetWithoutWeight = parse {
        let! name = pStringInQuotes
        do! pComma
        let! price = pfloat

        return {
            Name = name
            Price = price |> decimal
            Weight = 0.0M
        }
    }

    let pSweetWithWeight = parse {
        let! name = pStringInQuotes
        do! pComma
        let! price = pfloat
        do! pComma
        let! weight = pfloat

        return {
            Name = name
            Price = price |> decimal
            Weight = weight |> decimal
        }
    }

    pSweetWithWeight <|> pSweetWithoutWeight

let pCandy = pPredicate "candy" pSweetInner |>> Candy

let pCookie = pPredicate "cookie" pSweetInner |>> Cookie

let pCake =
    let pCakeInner = parse {
        let! name = pStringInQuotes
        do! pComma
        let! ingredients = pList pStringInQuotes
        do! pComma
        let! price = pfloat

        return {
            Cake.Name = name
            Ingredients = ingredients
            Price = price |> decimal
        }
    }

    pPredicate "cake" pCakeInner

let parseCandy = parseInput pCandy

let parseCookie = parseInput pCookie

let parseCake = parseInput pCake
