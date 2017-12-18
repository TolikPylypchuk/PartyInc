namespace PartyInc.Core

open System

type PartyOrganizerState =
    | NotStarted
    | SaidHi
    | Started
    | SpecifiedDateTime of DateTime
with
    static member Initial = NotStarted

type SweetsOrderConsultantState = {
    OrderState: Food * Preferences
    PreviousIntent: string
}
