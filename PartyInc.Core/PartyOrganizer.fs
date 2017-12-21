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
        |> async.Return
    | "start", NotStarted ->
        getResponse intent
        |> Trial.lift (fun responseText -> responseText, Started)
        |> async.Return
    | _, Started ->
        getResponse "input.name"
        |> Trial.lift (fun responseText -> responseText, SpecifiedName response.Query)
        |> async.Return
    | "input.date-time", SpecifiedName name
    | "input.date-time", ReservedDateTime name
    | "input.date-time", IncorrectDateTime name ->
        async {
            let dateTime = trial {
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

                return!
                    if success
                    then dateTime |> ok
                    else "Could not parse the date-time" |> fail
            }

            match dateTime with
            | Bad errors -> return errors |> Bad
            | Ok (dateTime, _) ->
                let! existingOrders = PrologOrder.getAllOrdersForDateTime dateTime

                return trial {
                    let! existingOrders = existingOrders

                    let newState, partyName =
                        if dateTime > DateTime.Now
                        then
                            match existingOrders with
                            | [] -> SpecifiedDateTime (name, dateTime), None
                            | head :: _ -> ReservedDateTime name, Some head.Name
                        else IncorrectDateTime name, None

                    let! response =
                        (intent, newState |> PartyOrganizerState.getName)
                        |> ResponseChoices.getResponseKey
                        |> getResponse

                    let response =
                        partyName
                        |> Option.fold (fun (res : string) name -> res.Replace("$party", name))
                                       response

                    return response, newState
                }
        }
    | _, SpecifiedDateTime (name, dateTime) ->
        getResponse "input.address"
        |> Trial.lift (fun responseText ->
            responseText, SpecifiedAddress (name, dateTime, response.Query))
        |> async.Return
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
        |> async.Return
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
        |> async.Return
    | _ ->
        getResponse "None"
        |> Trial.lift (fun response -> response, state)
        |> async.Return
