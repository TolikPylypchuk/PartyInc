namespace PartyInc.Core

open Chessie.ErrorHandling

open PartyInc.Core

type ResponseHandler<'TState> =
    delegate of Response * 'TState -> Result<string * 'TState, string> 

type BotInfo<'TState> = {
    Id: string
    SubscriptionKey: string
    Respond: ResponseHandler<'TState>
}

module Bot =

    [<CompiledName("RespondAsync")>]
    let respondAsync<'TState> bot (state : 'TState) query =
        async {
            let! responseJson = Luis.requestAsync bot.Id bot.SubscriptionKey query
            return
                responseJson
                >>= Luis.parseResponse
                |> Trial.lift (fun response -> response, state)
                >>= bot.Respond.Invoke
        }
