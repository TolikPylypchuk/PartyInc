namespace PartyInc.Core

open Chessie.ErrorHandling

open PartyInc.Core

type ResponseHandler<'TState> =
    delegate of Response * 'TState -> Async<Result<string * 'TState, string>> 

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
            let result = trial {
                let! responseJson = responseJson
                let! response = responseJson |> Luis.parseResponse
                return bot.Respond.Invoke(response, state)
            }

            match result with
            | Ok (asyncResult, _) -> return! asyncResult
            | Bad errors -> return (errors |> Bad)
        }
