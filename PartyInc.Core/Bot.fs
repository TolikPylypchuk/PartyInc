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

    let respondAsync botInfo query = async {
        let! responseJson = requestAsync botInfo.Id botInfo.SubscriptionKey query
        let response = responseJson |> parseResponse
        return! ResponseManager.manageResponse(response)
    }

    [<CompiledName("Respond")>]
    let respond botInfo query =
        respondAsync botInfo query |> Async.StartAsTask
