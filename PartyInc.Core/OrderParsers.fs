module PartyInc.Core.OrderParsers

open FParsec

open Parsers
open FoodParsers

let pOrder =
    let pOrderInner = parse {
        let! name = pStringInQuotes
        do! pComma
        let! dateTime = pDateTime
        do! pComma
        let! address = pStringInQuotes
        do! pComma
        let! minAge = pint32
        do! pComma
        let! maxAge = pint32
        do! pComma
        let! cake = pCake
        do! pComma
        let! cookies = pList pCookie
        do! pComma
        let! candies = pList pCandy

        return {
            Name = name
            DateTime = dateTime
            Address = address
            MinAge = minAge
            MaxAge = maxAge
            Food = { Cake = cake; Cookies = cookies; Candies = candies }
            Drinks = []
        }
    }

    pPredicate "order" pOrderInner

let parseOrder = parseInput pOrder
