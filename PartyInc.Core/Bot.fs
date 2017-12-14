namespace PartyInc.Core

open PartyInc.Core
open PartyInc.Core.Luis
open PartyInc.Core.ResponseManager

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
            let response = responseJson |> parseResponse
            return ResponseManager.manageResponse(response)
        // TODO add other types
    }

    [<CompiledName("RespondTemporary")>]
    let respondTemporary botInfo query =
        respond botInfo query SweetsOrderConsultant
        |> Async.StartAsTask
