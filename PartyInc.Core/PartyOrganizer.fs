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
        |> Trial.lift (fun responseText -> responseText, NotStarted)
    | "start", NotStarted ->
        getResponse intent
        |> Trial.lift (fun responseText -> responseText, Started)
    | _, Started ->
        getResponse "input.name"
        |> Trial.lift (fun responseText -> responseText, SpecifiedName response.Query)
    | "input.date-time", SpecifiedName name
    | "input.date-time", IncorrectDateTime name ->
        response.Entities
        |> List.tryFind (fun entity ->
            entity.Type = "builtin.datetimeV2.date" ||
            entity.Type = "builtin.datetimeV2.datetime")
        |> Trial.failIfNone "Could not find the date entity"
        >>= Luis.getEntityResolutionValues
        |> Trial.lift List.head
        |> Trial.lift (fun resValue -> resValue.Value)
        |> Trial.lift DateTime.TryParse
        >>= (fun (success, dateTime) ->
            if success
            then dateTime |> ok
            else "Could not parse the date-time" |> fail)
        >>= (fun dateTime ->
            let newState =
                if dateTime > DateTime.Now
                then SpecifiedDateTime (name, dateTime)
                else IncorrectDateTime name

            (intent, newState |> PartyOrganizerState.getName)
            |> ResponseChoices.getResponseKey
            |> getResponse
            |> Trial.lift (fun response -> response, newState))
    | _, SpecifiedDateTime (name, dateTime) ->
        getResponse "input.address"
        |> Trial.lift (fun responseText ->
            responseText, SpecifiedAddress (name, dateTime, response.Query))
    | "input.age", SpecifiedAddress (name, dateTime, address)
    | "input.age", IncorrectMinAge (name, dateTime, address) ->
        response.Entities
        |> List.tryFind (fun entity -> entity.Type = "builtin.number")
        |> Trial.failIfNone "Could not find the age or number entity"
        >>= Luis.getEntityResolutionValue
        |> Trial.lift Int32.TryParse
        >>= (fun (success, age) ->
            if success
            then age |> ok
            else "Could not parse the age" |> fail)
        >>= (fun age ->
            let newState =
                if age > 0
                then SpecifiedMinAge (name, dateTime, address, age)
                else IncorrectMinAge (name, dateTime, address)

            (intent, newState |> PartyOrganizerState.getName)
            |> ResponseChoices.getResponseKey
            |> getResponse
            |> Trial.lift (fun response -> response, newState))
    | "input.age", SpecifiedMinAge (name, dateTime, address, minAge)
    | "input.age", IncorrectMaxAge (name, dateTime, address, minAge) ->
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
                then SpecifiedMaxAge (name, dateTime, address, minAge, maxAge)
                else IncorrectMaxAge (name, dateTime, address, minAge)

            (intent, newState |> PartyOrganizerState.getName)
            |> ResponseChoices.getResponseKey
            |> getResponse
            |> Trial.lift (fun response -> response, newState))
    | _ ->
        getResponse "None"
        |> Trial.lift (fun response -> response, state)
