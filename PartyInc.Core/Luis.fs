module PartyInc.Core.Luis

open System
open System.Net.Http
open System.Web

open Newtonsoft.Json
 
type Intent = {
    Intent: string
    Score: float
}

[<AllowNullLiteral>]
type ParsedResolution(values : string list) =
    member __.Values = values

type Resolution = {
    Values: string list
}

type ParsedEntity = {
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
} with
    static member FromParsedEntity(parsedEntity : ParsedEntity) = 
    {
        Entity = parsedEntity.Entity
        Type = parsedEntity.Type
        StartIndex = parsedEntity.StartIndex
        EndIndex = parsedEntity.EndIndex
        Score = if parsedEntity.Score.HasValue then Some(parsedEntity.Score.Value) else None
        Resolution = if parsedEntity.Resolution <> null then Some({ Values = parsedEntity.Resolution.Values }) else None
    }

type Child = {
    Type: string
    Value: string
}

type CompositeEntity = {
    ParentType: string
    Value: string
    Children: Child list
}

[<AllowNullLiteral>]
type ParsedCompositeEntities(values : CompositeEntity list) =
    member __.Values = values

type CompositeEntities = {
    Values: CompositeEntity list
}

type ParsedResponse = {
    Query: string
    TopScoringIntent: Intent
    Intents: Intent list
    Entities: ParsedEntity list
    CompositeEntities: ParsedCompositeEntities
}

type Response = {
    Query: string
    TopScoringIntent: Intent
    Intents: Intent list
    Entities: Entity list
    CompositeEntities: CompositeEntities option
} with
    static member FromParsedResponse(parsedResponse : ParsedResponse) =
        {
            Query = parsedResponse.Query
            TopScoringIntent = parsedResponse.TopScoringIntent
            Intents = parsedResponse.Intents
            Entities = parsedResponse.Entities |> List.map Entity.FromParsedEntity
            CompositeEntities = 
                if parsedResponse.CompositeEntities <> null then 
                    Some({ Values = parsedResponse.CompositeEntities.Values }) 
                else None
        }

let requestAsync (luisAppId : string) (subscriptionKey : string) query = async {
    use client =  new HttpClient()
    let queryString = HttpUtility.ParseQueryString(String.Empty)

    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey)

    queryString.["timezoneOffset"] <- "0"
    queryString.["verbose"] <- "true"
    queryString.["spellCheck"] <- "false"
    queryString.["staging"] <- "false"
    queryString.["q"] <- query
    
    let! response =
        client.GetAsync("https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/" + luisAppId +
                        "?" + queryString.ToString())
        |> Async.AwaitTask

    return! response.Content.ReadAsStringAsync() |> Async.AwaitTask
}

let request luisAppId subscriptionKey query =
    requestAsync luisAppId subscriptionKey query |> Async.StartAsTask

let parseResponse responceJson = 
    JsonConvert.DeserializeObject<ParsedResponse>(responceJson)
    |> Response.FromParsedResponse
