module PartyInc.Core.ResponseManager

open System

open PartyInc.Core
open PartyInc.Core.Luis

let manageResponse (response: Response) = 
    let responseChoices =
        ResponseChoices.sweetsOrderConsultant.[response.TopScoringIntent.Intent]
    responseChoices.[Random().Next(0,responseChoices.Length)]