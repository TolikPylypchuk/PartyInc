module PartyInc.Core.Luis

open System
open System.Net.Http
open System.Web
 
let requestAsync (luisAppId : string) (subscriptionKey : string) query = async {
    use client =  new HttpClient()
    let queryString = HttpUtility.ParseQueryString(String.Empty)

    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey)

    queryString.["timezoneOffset"] <- "0"
    queryString.["verbose"] <- "false"
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