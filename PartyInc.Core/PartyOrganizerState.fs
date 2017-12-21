namespace PartyInc.Core

open System

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
