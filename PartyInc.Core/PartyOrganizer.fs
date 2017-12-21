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
        trial {
            let! entities =
                response.Entities
                |> List.tryFind (fun entity ->
                    entity.Type = "builtin.datetimeV2.date" ||
                    entity.Type = "builtin.datetimeV2.datetime")
                |> Trial.failIfNone "Could not find the date entity"

            let! values = entities |> Luis.getEntityResolutionValues

            let (success, dateTime) =
                values
                |> List.head
                |> (fun resValue -> resValue.Value)
                |> DateTime.TryParse

            let! dateTime =
                if success
                then dateTime |> ok
                else "Could not parse the date-time" |> fail

            let newState =
                if dateTime > DateTime.Now
                then SpecifiedDateTime (name, dateTime)
                else IncorrectDateTime name

            let! response =
                (intent, newState |> PartyOrganizerState.getName)
                |> ResponseChoices.getResponseKey
                |> getResponse

            return response, newState
        }
    | _, SpecifiedDateTime (name, dateTime) ->
        getResponse "input.address"
        |> Trial.lift (fun responseText ->
            responseText, SpecifiedAddress (name, dateTime, response.Query))
    | "input.age", SpecifiedAddress (name, dateTime, address)
    | "input.age", IncorrectMinAge (name, dateTime, address) ->
        trial {
            let! entity =
                response.Entities
                |> List.tryFind (fun entity -> entity.Type = "builtin.number")
                |> Trial.failIfNone "Could not find the age or number entity"

            let! value = entity |> Luis.getEntityResolutionValue

            let (success, age) = value |> Int32.TryParse

            let! age =
                if success
                then age |> ok
                else "Could not parse the age" |> fail

            let newState =
                if age > 0
                then SpecifiedMinAge (name, dateTime, address, age)
                else IncorrectMinAge (name, dateTime, address)

            let! response =
                (intent, newState |> PartyOrganizerState.getName)
                |> ResponseChoices.getResponseKey
                |> getResponse

            return response, newState
        }
    | "input.age", SpecifiedMinAge (name, dateTime, address, minAge)
    | "input.age", IncorrectMaxAge (name, dateTime, address, minAge) ->
        trial {
            let! entity =
                response.Entities
                |> List.tryFind (fun entity -> entity.Type = "builtin.number")
                |> Trial.failIfNone "Could not find the age or number entity"

            let! value = entity |> Luis.getEntityResolutionValue

            let (success, age) = value |> Int32.TryParse

            let! maxAge =
                if success
                then age |> ok
                else "Could not parse the age" |> fail

            let newState =
                if maxAge >= minAge
                then SpecifiedMaxAge (name, dateTime, address, minAge, maxAge)
                else IncorrectMaxAge (name, dateTime, address, minAge)

            let! response =
                (intent, newState |> PartyOrganizerState.getName)
                |> ResponseChoices.getResponseKey
                |> getResponse
            
            return response, newState
        }
    | _ ->
        getResponse "None"
        |> Trial.lift (fun response -> response, state)
