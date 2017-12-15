namespace PartyInc.Core

open PartyInc.Core
open PartyInc.Core.Luis

type ResponseHandler = delegate of Response -> string

type BotInfo = {
    Id: string
    SubscriptionKey: string
    Respond: ResponseHandler
}

module Bot =

    [<CompiledName("RespondAsync")>]
    let respondAsync bot query =
        async {
            let! responseJson = requestAsync bot.Id bot.SubscriptionKey query
            let response = responseJson |> parseResponse
            return bot.Respond.Invoke(response)
        }
        |> Async.StartAsTask
