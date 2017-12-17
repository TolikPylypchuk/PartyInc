namespace PartyInc.Core

open Chessie.ErrorHandling

open PartyInc.Core
open PartyInc.Core.BotStates
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
            return
                responseJson
                >>= (parseResponse >> Trial.lift bot.Respond.Invoke)
        }
