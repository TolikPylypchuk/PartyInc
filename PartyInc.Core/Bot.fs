namespace PartyInc.Core

open Chessie.ErrorHandling

open PartyInc.Core
open PartyInc.Core.Luis

type ResponseHandler<'TState> =
    delegate of Response * 'TState -> Result<string * 'TState, string> 

type BotInfo<'TState> = {
    Id: string
    SubscriptionKey: string
    Respond: ResponseHandler<'TState>
}

module Bot =

    [<CompiledName("RespondAsync")>]
    let respondAsync<'TState> bot (initialState : 'TState) query =
        async {
            let! responseJson = requestAsync bot.Id bot.SubscriptionKey query
            return
                responseJson
                >>= parseResponse
                |> Trial.lift (fun response -> response, initialState)
                >>= bot.Respond.Invoke
        }
