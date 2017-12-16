namespace PartyInc.Core

module BotStates = 

    type SweetsOrderConsultantState = {
        OrderState: Food * Preferences
        PreviousIntent: string
    }

    type BotState =
        | SweetsOrderConsultantState
        //other bot states