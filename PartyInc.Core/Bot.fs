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

    [<CompiledName("RespondAsync")>]
    let respondAsync botInfo query =
        async {
            let! responseJson = requestAsync botInfo.Id botInfo.SubscriptionKey query
            let response = responseJson |> parseResponse
            return! ResponseManager.manageResponse(response)
        }
        |> Async.StartAsTask
