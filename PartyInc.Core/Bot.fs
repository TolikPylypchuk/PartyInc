namespace PartyInc.Core

open System

open PartyInc.Core
open PartyInc.Core.Luis

type BotInfo = {
    Id: string
    SubscriptionKey: string
}

type BotType =
    | SweetsOrderConsultant
    // TODO Add other types
    
module Bot =

    let respond botInfo query botType = async {
        match botType with
        | SweetsOrderConsultant ->

            let! responseJson = requestAsync botInfo.Id botInfo.SubscriptionKey query
            let responce = responseJson |> parseResponse

            let responseChoices =
                ResponseChoices.sweetsOrderConsultant.[responce.TopScoringIntent.Intent]
            return responseChoices.[Random().Next(0,responseChoices.Length)]
        // TODO add other types
    }

    [<CompiledName("RespondTemporary")>]
    let respondTemporary botInfo query =
        respond botInfo query SweetsOrderConsultant
        |> Async.StartAsTask
