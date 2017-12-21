namespace PartyInc.Core

open System

type PartyOrganizerState =
    | NotStarted
    | Started
    | SpecifiedName          of string
    | SpecifiedDateTime      of string * DateTime
    | IncorrectDateTime      of string
    | ReservedDateTime       of string
    | SpecifiedAddress       of string * DateTime * string
    | SpecifiedMinAge        of string * DateTime * string * int
    | IncorrectMinAge        of string * DateTime * string
    | SpecifiedMaxAge        of string * DateTime * string * int * int
    | IncorrectMaxAge        of string * DateTime * string * int
    | SpecifiedFood          of string * DateTime * string * int * int * Food
    | SpecifiedDrinks        of string * DateTime * string * int * int * Drink list
    | SpecifiedFoodAndDrinks of string * DateTime * string * int * int * Food * Drink list
    | Finished               of Order

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
        | SpecifiedFood _ -> "food"
        | SpecifiedDrinks _ -> "drinks"
        | SpecifiedFoodAndDrinks _ -> "food-drinks"
        | Finished _ -> "end"

    [<CompiledName("AddFood")>]
    let addFood food state =
        match state with
        | SpecifiedMaxAge (name, dateTime, address, minAge, maxAge) ->
            SpecifiedFood (name, dateTime, address, minAge, maxAge, food)
        | state -> state

    [<CompiledName("AddDrinks")>]
    let addDrinks drinks state =
        match state with
        | SpecifiedMaxAge (name, dateTime, address, minAge, maxAge) ->
            SpecifiedDrinks (name, dateTime, address, minAge, maxAge, drinks)
        | SpecifiedFood (name, dateTime, address, minAge, maxAge, food) ->
            SpecifiedFoodAndDrinks (name, dateTime, address, minAge, maxAge, food, drinks)
        | state -> state
