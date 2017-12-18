module PartyInc.Core.PartyOrganizer

open System

open Chessie.ErrorHandling

[<CompiledName("HandleResponse")>]
let handleResponse (response, state) =
    let getResponse = ResponseChoices.getResponse ResponseChoices.partyOrganizer

    match response.TopScoringIntent.Intent, state with
    | "welcome" as intent, NotStarted ->
        getResponse intent
        |> Trial.lift (fun responseText -> responseText, SaidHi)
    | "start" as intent, SaidHi ->
        getResponse intent
        |> Trial.lift (fun responseText -> responseText, Started)
    | "input.date-time" as intent, Started ->
        getResponse intent
        >>= (fun responseText ->
            let dateTimeText =
                response.Entities
                |> List.tryFind (fun entity ->
                    entity.Type = "builtin.datetimeV2.date" ||
                    entity.Type = "builtin.datetimeV2.datetime")
                |> Trial.failIfNone "Could not find the date entity"
                |> Trial.lift (fun entity -> entity.Entity)

            dateTimeText
            |> Trial.lift DateTime.Parse
            |> Trial.lift (fun dateTime -> responseText, SpecifiedDateTime dateTime))
    | _ ->
        getResponse "None"
        |> Trial.lift (fun response -> response, state)
