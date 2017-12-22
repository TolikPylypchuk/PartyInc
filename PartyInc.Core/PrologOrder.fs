module PartyInc.Core.PrologOrder

open System

open Chessie.ErrorHandling
open Prolog

open PrologInterop
open OrderParsers
open PrologFood
open PrologDrinks

[<CompiledName("GetOrder")>]
let getOrder prologSolution =
    let variables =
        prologSolution
        |> getVariables
        |> List.sortBy (fun var -> var.Name)
    
    if variables.Length <> 9 then
        sprintf "Expected 9 variables, got %i" variables.Length |> fail
    else
        let addressVar = variables.[0]
        let cakeVar = variables.[1]
        let candiesVar = variables.[2]
        let cookiesVar = variables.[3]
        let dateTimeVar = variables.[4]
        let drinksVar = variables.[5]
        let maxAgeVar = variables.[6]
        let minAgeVar = variables.[7]
        let nameVar = variables.[8]

        if not (addressVar.Name = "Address" &&
                cakeVar.Name = "Cake" &&
                candiesVar.Name = "Candies" &&
                cookiesVar.Name = "Cookies" &&
                dateTimeVar.Name = "DateTime" &&
                drinksVar.Name = "Drinks" &&
                maxAgeVar.Name = "MaxAge" &&
                minAgeVar.Name = "MinAge" &&
                nameVar.Name = "Name") then
            let list = "Address, Cake, Candies, Cookies, DateTime, Drinks, MaxAge, MinAge, Name"
            sprintf "Expected %s, got %s, %s, %s, %s, %s, %s, %s, %s, %s"
                    list
                    addressVar.Name
                    cakeVar.Name
                    candiesVar.Name
                    cookiesVar.Name
                    dateTimeVar.Name
                    drinksVar.Name
                    maxAgeVar.Name
                    minAgeVar.Name
                    nameVar.Name
            |> fail
        else
            sprintf "order(%s, %s, %s, %s, %s, %s, %s, %s, %s)"
                    nameVar.Value
                    dateTimeVar.Value
                    addressVar.Value
                    minAgeVar.Value
                    maxAgeVar.Value
                    cakeVar.Value
                    cookiesVar.Value
                    candiesVar.Value
                    drinksVar.Value
            |> parseOrder

let getAllOrdersForDate (dateTime : DateTime) = async {
    let prolog = PrologEngine()

    let! solutions =
        getSolutions prolog
                     "Data\\orders.pl"
                     "order(Name, DateTime, Address, MinAge, MaxAge, Cake, Cookies, Candies, Drinks)"

    return trial {
        let! solutions = solutions
        let! orders =
            solutions
            |> List.map getOrder
            |> Trial.sequence

        return
            orders
            |> List.filter (fun order -> order.DateTime.Date = dateTime.Date)
    }
}

[<CompiledName("FormatOrder")>]
let formatOrder order =
    match order.Food.Cake with
    | Some cake ->
        sprintf "order(\"%s\", \"%s\", \"%s\", %i, %i, %s, %s, %s, %s)"
            order.Name
            (order.DateTime.ToString("s"))
            order.Address
            order.MinAge
            order.MaxAge
            (cake |> formatCake)
            (order.Food.Cookies |> List.map formatCookie |> formatList)
            (order.Food.Candies |> List.map formatCandy  |> formatList)
            (order.Drinks       |> List.map formatDrink  |> formatList)
    | None ->
        sprintf "order(\"%s\", \"%s\", \"%s\", %i, %i, %s, %s, %s)"
                order.Name
                (order.DateTime.ToString("s"))
                order.Address
                order.MinAge
                order.MaxAge
                (order.Food.Cookies |> List.map formatCookie |> formatList)
                (order.Food.Candies |> List.map formatCandy  |> formatList)
                (order.Drinks       |> List.map formatDrink  |> formatList)
