module PartyInc.Core.PrologOrder

open System

open Chessie.ErrorHandling
open Prolog

open PrologInterop
open OrderParsers

[<CompiledName("GetOrder")>]
let getOrder prologSolution =
    let variables =
        prologSolution
        |> getVariables
        |> List.sortBy (fun var -> var.Name)
    
    if variables.Length <> 8 then
        sprintf "Expected 8 variables, got %i" variables.Length |> fail
    else
        let addressVar = variables.[0]
        let cakeVar = variables.[1]
        let candiesVar = variables.[2]
        let cookiesVar = variables.[3]
        let dateTimeVar = variables.[4]
        let maxAgeVar = variables.[5]
        let minAgeVar = variables.[6]
        let nameVar = variables.[7]

        if not (addressVar.Name = "Address" &&
                cakeVar.Name = "Cake" &&
                candiesVar.Name = "Candies" &&
                cookiesVar.Name = "Cookies" &&
                dateTimeVar.Name = "DateTime" &&
                maxAgeVar.Name = "MaxAge" &&
                minAgeVar.Name = "MinAge" &&
                nameVar.Name = "Name") then
            let list = "Address, Cake, Candies, Cookies, DateTime, MaxAge, MinAge, Name"
            sprintf "Expected %s, got %s, %s, %s, %s, %s, %s, %s, %s"
                    list
                    addressVar.Name
                    cakeVar.Name
                    candiesVar.Name
                    cookiesVar.Name
                    dateTimeVar.Name
                    maxAgeVar.Name
                    minAgeVar.Name
                    nameVar.Name
            |> fail
        else
            sprintf "order(%s, %s, %s, %s, %s, %s, %s, %s)"
                    nameVar.Value
                    dateTimeVar.Value
                    addressVar.Value
                    minAgeVar.Value
                    maxAgeVar.Value
                    cakeVar.Value
                    cookiesVar.Value
                    candiesVar.Value
            |> parseOrder

let getAllOrdersForDateTime (dateTime : DateTime) = async {
    let prolog = PrologEngine()

    let! solutions =
        getSolutions prolog
                     "Data\\orders.pl"
                     "order(Name, DateTime, Address, MinAge, MaxAge, Cake, Cookies, Candies)"

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

