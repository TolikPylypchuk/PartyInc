module PartyInc.Core.OrderParsers

open FParsec

open Parsers
open FoodParsers
open DrinkParsers

let pOrder =
    let pOrderInner =
        let pOrderWithCake = parse {
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
            do! pComma
            let! drinks = pList pDrink

            return {
                Name = name
                DateTime = dateTime
                Address = address
                MinAge = minAge
                MaxAge = maxAge
                Food = { Cake = Some cake; Cookies = cookies; Candies = candies }
                Drinks = drinks
            }
        }

        let pOrderWithoutCake = parse {
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
            let! cookies = pList pCookie
            do! pComma
            let! candies = pList pCandy
            do! pComma
            let! drinks = pList pDrink

            return {
                Name = name
                DateTime = dateTime
                Address = address
                MinAge = minAge
                MaxAge = maxAge
                Food = { Cake = None; Cookies = cookies; Candies = candies }
                Drinks = drinks
            }
        }

        pOrderWithCake <|> pOrderWithoutCake
    
    pPredicate "order" pOrderInner

let parseOrder = parseInput pOrder
