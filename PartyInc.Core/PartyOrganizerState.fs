namespace PartyInc.Core

open System

type PartyOrganizerState =
    | NotStarted
    | SaidHi
    | Started
    | SpecifiedDateTime of DateTime
    | IncorrectDateTime
    | SpecifiedAddress of DateTime * string
    | SpecifiedMinAge  of DateTime * string * int
    | IncorrectMinAge  of DateTime * string
    | SpecifiedMaxAge  of DateTime * string * int * int
    | IncorrectMaxAge  of DateTime * string * int

[<RequireQualifiedAccess>]
module PartyOrganizerState =

    [<CompiledName("Initial")>]
    let initial = NotStarted

    let getName =
        function
        | NotStarted -> "not-started"
        | SaidHi -> "said-hi"
        | Started -> "started"
        | SpecifiedDateTime _ -> "date-time"
        | IncorrectDateTime -> "incorrect-date-time"
        | SpecifiedAddress _ -> "address"
        | SpecifiedMinAge _ -> "min-age"
        | IncorrectMinAge _ -> "incorrect-min-age"
        | SpecifiedMaxAge _ -> "max-age"
        | IncorrectMaxAge _ -> "incorrect-max-age"
