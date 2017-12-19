module PartyInc.Core.PartyOrganizer

open System

open Chessie.ErrorHandling

[<CompiledName("HandleResponse")>]
let handleResponse (response, state) =
    let getResponse = ResponseChoices.getResponse ResponseChoices.partyOrganizer

    let intent = response.TopScoringIntent.Intent

    match intent, state with
    | "welcome", NotStarted ->
        getResponse intent
        |> Trial.lift (fun responseText -> responseText, SaidHi)
    | "start", SaidHi ->
        getResponse intent
        |> Trial.lift (fun responseText -> responseText, Started)
    | "input.date-time", Started
    | "input.date-time", IncorrectDateTime ->
        response.Entities
        |> List.tryFind (fun entity ->
            entity.Type = "builtin.datetimeV2.date" ||
            entity.Type = "builtin.datetimeV2.datetime")
        |> Trial.failIfNone "Could not find the date entity"
        |> Trial.lift (fun entity -> entity.Entity |> DateTime.TryParse)
        >>= (fun (success, dateTime) ->
            if success
            then dateTime |> ok
            else "Could not parse the date-time" |> fail)
        >>= (fun dateTime ->
            let newState =
                if dateTime > DateTime.Now
                then SpecifiedDateTime dateTime
                else IncorrectDateTime

            (intent, newState |> PartyOrganizerState.getName)
            |> ResponseChoices.getResponseKey
            |> getResponse
            |> Trial.lift (fun response -> response, newState))
    | _, SpecifiedDateTime dateTime ->
        getResponse "input.address"
        |> Trial.lift (fun responseText ->
            responseText, SpecifiedAddress(dateTime, response.Query))
    | "input.age", SpecifiedAddress (dateTime, address)
    | "input.age", IncorrectMinAge (dateTime, address) ->
        response.Entities
        |> List.tryFind (fun entity -> entity.Type = "builtin.number")
        |> Trial.failIfNone "Could not find the age or number entity"
        |> Trial.lift (fun entity -> entity.Entity |> Int32.TryParse)
        >>= (fun (success, age) ->
            if success
            then age |> ok
            else "Could not parse the age" |> fail)
        >>= (fun age ->
            let newState =
                if age > 0
                then SpecifiedMinAge (dateTime, address, age)
                else IncorrectMinAge (dateTime, address)

            (intent, newState |> PartyOrganizerState.getName)
            |> ResponseChoices.getResponseKey
            |> getResponse
            |> Trial.lift (fun response -> response, newState))
    | "input.age", SpecifiedMinAge (dateTime, address, minAge)
    | "input.age", IncorrectMaxAge (dateTime, address, minAge) ->
        response.Entities
        |> List.tryFind (fun entity -> entity.Type = "builtin.number")
        |> Trial.failIfNone "Could not find the age or number entity"
        |> Trial.lift (fun entity -> entity.Entity |> Int32.TryParse)
        >>= (fun (success, age) ->
            if success
            then age |> ok
            else "Could not parse the age" |> fail)
        >>= (fun maxAge ->
            let newState =
                if maxAge >= minAge
                then SpecifiedMaxAge (dateTime, address, minAge, maxAge)
                else IncorrectMaxAge (dateTime, address, minAge)

            (intent, newState |> PartyOrganizerState.getName)
            |> ResponseChoices.getResponseKey
            |> getResponse
            |> Trial.lift (fun response -> response, newState))
    | _ ->
        getResponse "None"
        |> Trial.lift (fun response -> response, state)
