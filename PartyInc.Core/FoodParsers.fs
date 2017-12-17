module PartyInc.Core.FoodParsers

open FParsec
open Chessie.ErrorHandling

open Parsers

let private pSweetInner = parse {
    let! name = pStringInQuotes

    do! pStringInSpaces "," |>> ignore

    let! price = pfloat

    return {
        Name = name
        Price = price |> decimal
        Weight = 0.0M
    }
}

let pCandy = pPredicate "candy" pSweetInner |>> Candy

let pCookie = pPredicate "cookie" pSweetInner |>> Cookie

let pCake =
    let pCakeInner = parse {
        let! name = pStringInQuotes

        do! pStringInSpaces "," |>> ignore

        let! ingredients = pList pStringInQuotes

        do! pStringInSpaces "," |>> ignore

        let! price = pfloat

        return {
            Name = name
            Ingredients = ingredients
            Price = price |> decimal
        }
    }

    pPredicate "cake" pCakeInner

let parseCandy input =
    match run pCandy input with
    | Success (candy, _, _) -> candy |> ok
    | Failure (error, _, _) -> error |> fail

let parseCookie input =
    match run pCookie input with
    | Success (cookie, _, _) -> cookie |> ok
    | Failure (error, _, _) -> error |> fail

let parseCake input =
    match run pCake input with
    | Success (cake, _, _) -> cake |> ok
    | Failure (error, _, _) -> error |> fail
