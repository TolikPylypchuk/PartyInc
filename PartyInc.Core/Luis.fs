namespace PartyInc.Core

open Chessie.ErrorHandling

open System
open System.Net.Http
open System.Web

[<RequireQualifiedAccess>]
module Luis =

    let requestAsync luisAppId (subscriptionKey : string) query =
        let computation = async {
            use client =  new HttpClient()
            let queryString = HttpUtility.ParseQueryString(String.Empty)

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey)

            queryString.["timezoneOffset"] <- "0"
            queryString.["verbose"] <- "true"
            queryString.["spellCheck"] <- "false"
            queryString.["staging"] <- "false"
            queryString.["q"] <- query

            let url =
                sprintf "https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/%s?%O"
                        luisAppId
                        queryString

            let! response =
                url
                |> client.GetAsync
                |> Async.AwaitTask

            return! response.Content.ReadAsStringAsync() |> Async.AwaitTask
        }

        computation
        |> Async.Catch
        |> Async.map (Trial.ofChoice >> Trial.exnToMessage)

    [<CompiledName("ParseResponse")>]
    let parseResponse responseJson = 
        responseJson
        |> Json.deserialize<Response>

    let getEntityResolutionValue entity =
        match entity.Resolution with
        | Value value -> value.Value |> ok
        | Values _ -> "Cannot get entity value, because it contains multiple values" |> fail

    let getEntityResolutionStrings entity =
        match entity.Resolution with
        | Value _ -> "Cannot get entity values, because it contains a single value" |> fail
        | Values values ->
            values.Values
            |> List.map (function
                | StringValue value -> value |> ok
                | ResolutionValue _ -> "One of the values is not a string" |> fail)
            |> Trial.sequence

    let getEntityResolutionValues entity =
        match entity.Resolution with
        | Value _ -> "Cannot get entity values, because it contains a single value" |> fail
        | Values values ->
            values.Values
            |> List.map (function
                | ResolutionValue value -> value |> ok
                | StringValue _ -> "One of the values is not a resolution value" |> fail)
            |> Trial.sequence
