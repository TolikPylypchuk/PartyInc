namespace PartyInc.Core

open System

open Chessie.ErrorHandling

type PartyOrganizerState =
    | NotStarted
    | Started
    | SpecifiedName     of string
    | SpecifiedDateTime of string * DateTime
    | IncorrectDateTime of string
    | ReservedDateTime  of string
    | SpecifiedAddress  of string * DateTime * string
    | SpecifiedMinAge   of string * DateTime * string * int
    | IncorrectMinAge   of string * DateTime * string
    | SpecifiedMaxAge   of string * DateTime * string * int * int
    | IncorrectMaxAge   of string * DateTime * string * int
    | GoToFood          of string * DateTime * string * int * int
    | SpecifiedFood     of string * DateTime * string * int * int * Food
    | GoToDrinks        of string * DateTime * string * int * int * Food
    | SpecifiedDrinks   of string * DateTime * string * int * int * Food * Drink list
    | Finished          of Order
    | Canceled

[<RequireQualifiedAccess>]
module PartyOrganizerState =

    [<CompiledName("Initial")>]
    let initial = NotStarted

    let getName =
        function
        | NotStarted -> "not-started"
        | Started -> "started"
        | SpecifiedName _ -> "name"
        | SpecifiedDateTime _ -> "date-time"
        | IncorrectDateTime _ -> "incorrect-date-time"
        | ReservedDateTime _ -> "reserved-date-time"
        | SpecifiedAddress _ -> "address"
        | SpecifiedMinAge _ -> "min-age"
        | IncorrectMinAge _ -> "incorrect-min-age"
        | SpecifiedMaxAge _ -> "max-age"
        | IncorrectMaxAge _ -> "incorrect-max-age"
        | GoToFood _ -> "go-to-food"
        | SpecifiedFood _ -> "food"
        | GoToDrinks _ -> "go-to-drinks"
        | SpecifiedDrinks _ -> "drinks"
        | Finished _ -> "end"
        | Canceled -> "canceled"

    [<CompiledName("IsAwaitingFood")>]
    let isAwaitingFood = function GoToFood _ -> true | _ -> false

    [<CompiledName("IsAwaitingDrinks")>]
    let isAwaitingDrinks = function GoToDrinks _ -> true | _ -> false

    [<CompiledName("IsFinished")>]
    let isFinished = function Finished _ -> true | _ -> false

    [<CompiledName("GetOrder")>]
    let getOrder = function Finished order -> order |> ok | _ -> "Order not finished" |> fail

    
    [<CompiledName("AddFood")>]
    let addFood sweetOrderConsultantState partyOrganizerState =
        match partyOrganizerState with
        | GoToFood (name, dateTime, address, minAge, maxAge) ->
            match sweetOrderConsultantState with
            | FinishedOrder food ->
                SpecifiedFood (name, dateTime, address, minAge, maxAge, food)
            | _ -> partyOrganizerState
        | _ -> partyOrganizerState

    // TODO Implement this when the drink order consultant's states are ready
    // TODO Rename 'LastState' to actual state
    (*
    [<CompiledName("AddDrinks")>]
    let addDrinks drinksOrderConsultantState partyOrganizerState =
        match partyOrganizerState with
        | GoToDrinks (name, dateTime, address, minAge, maxAge, food)
            match drinksOrderConsultantState with
            | LastState drinks ->
                SpecifiedDrinks (name, dateTime, address, minAge, maxAge, food, drinks)
            | _ -> state
        | _ -> state
    *)
