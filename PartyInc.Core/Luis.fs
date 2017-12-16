namespace PartyInc.Core

open System
open System.Net.Http
open System.Web

type Intent = {
    Intent: string
    Score: float
}

type Resolution = {
    Values: string list
}

type Entity = {
    Entity: string
    Type: string
    StartIndex: int
    EndIndex: int
    Score: float option
    Resolution: Resolution option
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

type Response = {
    Query: string
    TopScoringIntent: Intent
    Intents: Intent list
    Entities: Entity list
    CompositeEntities: CompositeEntity list option
}

module Luis =
    
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
    
    [<CompiledName("ParseResponse")>]
    let parseResponse responseJson = 
        responseJson
        |> Json.deserialize<Response>
