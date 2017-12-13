namespace PartyInc.Core

open System
open System.Net.Http
open System.Web

open Newtonsoft.Json

type Intent = {
    Intent: string
    Score: float
}

[<AllowNullLiteral>]
type private ParsedResolution(values : string list) =
    member __.Values = values

type Resolution = {
    Values: string list
}

type private ParsedEntity = {
    Entity: string
    Type: string
    StartIndex: int
    EndIndex: int
    Score: Nullable<float>
    Resolution: ParsedResolution
}

type Entity = {
    Entity: string
    Type: string
    StartIndex: int
    EndIndex: int
    Score: float option
    Resolution: Resolution option
}

type private ParsedResponse = {
    Query: string
    TopScoringIntent: Intent
    Intents: Intent list
    Entities: ParsedEntity list
}

type Response = {
    Query: string
    TopScoringIntent: Intent
    Intents: Intent list
    Entities: Entity list
}

module Luis =

    let private toEntity (parsedEntity : ParsedEntity) = {
        Entity = parsedEntity.Entity
        Type = parsedEntity.Type
        StartIndex = parsedEntity.StartIndex
        EndIndex = parsedEntity.EndIndex
        Score =
            if parsedEntity.Score.HasValue
            then Some(parsedEntity.Score.Value)
            else None
        Resolution =
            if parsedEntity.Resolution <> null
            then Some({ Values = parsedEntity.Resolution.Values })
            else None
    }

    let private toResponse (parsedResponse: ParsedResponse) = {
        Query = parsedResponse.Query
        TopScoringIntent = parsedResponse.TopScoringIntent
        Intents = parsedResponse.Intents
        Entities = parsedResponse.Entities |> List.map toEntity
    }

    [<CompiledName("RequestAsync")>]
    let requestAsync luisAppId (subscriptionKey : string) query = async {
        use client =  new HttpClient()
        let queryString = HttpUtility.ParseQueryString(String.Empty)

        client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey)

        queryString.["timezoneOffset"] <- "0"
        queryString.["verbose"] <- "true"
        queryString.["spellCheck"] <- "false"
        queryString.["staging"] <- "false"
        queryString.["q"] <- query

        let url =
            sprintf "https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/%s?%s"
                    luisAppId
                    (queryString.ToString())
                    
        let! response =
            url
            |> client.GetAsync
            |> Async.AwaitTask

        return! response.Content.ReadAsStringAsync() |> Async.AwaitTask
    }

    [<CompiledName("Request")>]
    let request luisAppId subscriptionKey query =
        requestAsync luisAppId subscriptionKey query |> Async.StartAsTask

    [<CompiledName("ParseResponse")>]
    let parseResponse responseJson = 
        responseJson
        |> JsonConvert.DeserializeObject<ParsedResponse>
        |> toResponse
