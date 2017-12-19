namespace PartyInc.Core

open System

type PartyOrganizerState =
    | NotStarted
    | SaidHi
    | Started
    | SpecifiedDateTime of DateTime
    | IncorrectDateTime
    | SpecifiedMinAge of DateTime * int
    | IncorrectMinAge of DateTime
    | SpecifiedMaxAge of DateTime * int * int
    | IncorrectMaxAge of DateTime * int

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
        | SpecifiedMinAge _ -> "min-age"
        | IncorrectMinAge _ -> "incorrect-min-age"
        | SpecifiedMaxAge _ -> "max-age"
        | IncorrectMaxAge _ -> "incorrect-max-age"
