module PartyInc.Core.SweetsOrderConsultant

open System

open Chessie.ErrorHandling

[<CompiledName("HandleResponse")>]
let handleResponse (response, state) =
    let getResponse = ResponseChoices.getResponse ResponseChoices.sweetsOrderConsultant

    let intent = response.TopScoringIntent.Intent

    match intent, state with
    | "order.all.price", StartedCakeSpecifiedPrice (order, price) -> fail "" |> async.Return
    | "order.all.price", StartedCakeFinishedPreferencesSpecifiedPrice (order, preferences, price) -> fail "" |> async.Return
    | "order.all.price", StartedCandy order ->
        trial {
            let! value = 
                response.Entities
                |> List.filter (fun entity -> entity.Type = "builtin.number")
                |> List.head
                |> Luis.getEntityResolutionValue
                
            let (success, price) = value |> Decimal.TryParse

            let! price =
                if success
                then price |> ok
                else "Could not parse the price" |> fail

            let newState = StartedCandySpecifiedPrice (order, price)

            let! response = 
                (intent, newState |> SweetsOrderConsultantState.getName)
                |> ResponseChoices.getResponseKey
                |> getResponse

            return response, newState 
        }
        |> async.Return
    | "order.all.price", StartedCookie order ->
        trial {
            let! value = 
                response.Entities
                |> List.filter (fun entity -> entity.Type = "builtin.number")
                |> List.head
                |> Luis.getEntityResolutionValue
                
            let (success, price) = value |> Decimal.TryParse

            let! price =
                if success
                then price |> ok
                else "Could not parse the price" |> fail

            let newState = StartedCookieSpecifiedPrice (order, price)

            let! response = 
                (intent, newState |> SweetsOrderConsultantState.getName)
                |> ResponseChoices.getResponseKey
                |> getResponse

            return response, newState 
        }
        |> async.Return
    | "order.cake", SweetsEmptyOrder
    | "order.sweets", SweetsEmptyOrder -> 
        trial {
            let! strings = 
                response.Entities
                |> List.filter (fun entity -> entity.Type = "cake" || entity.Type = "sweets")
                |> List.head
                |> Luis.getEntityResolutionStrings

            let name = strings |> List.head

            let newState = 
                if name = "cake" then 
                    StartedCake { Cake = None; Cookies = []; Candies = [] }
                elif name = "candy" then 
                    StartedCandy { Cake = None; Cookies = []; Candies = [] }
                else
                    StartedCookie { Cake = None; Cookies = []; Candies = [] }

            let! responseText = getResponse intent

            return responseText, newState  
        }
        |> async.Return
    | "order.cake", SweetsNotEmptyOrder order
    | "order.sweets", SweetsNotEmptyOrder order -> 
        trial {
            let! strings = 
                response.Entities
                |> List.filter (fun entity -> entity.Type = "cake" || entity.Type = "sweets")
                |> List.head
                |> Luis.getEntityResolutionStrings

            let name = strings |> List.head

            let newState = 
                if name = "cake" then 
                    StartedCake order
                elif name = "candy" then 
                    StartedCandy order
                else
                    StartedCookie order

            let! responseText = getResponse intent

            return responseText, newState  
        }
        |> async.Return
    | "order.cake.preferences-no", StartedCake order -> fail "" |> async.Return
    | "order.cake.preferences-yes", StartedCake order -> fail "" |> async.Return
    | "order.cake.preferences-yes-dislikes", StartedCakeStartedPreferences (order, preferences)
    | "order.cake.preferences-yes-likes", StartedCakeStartedPreferences (order, preferences) -> fail "" |> async.Return
    | "order.cake.preferences-yes-misunderstanding", StartedCakeStartedPreferences (order, preferences) -> fail "" |> async.Return
    | "order.cake.specify", StartedCakeSpecifiedPrice (order, price) -> fail "" |> async.Return
    | "order.cake.specify", StartedCakeFinishedPreferencesSpecifiedPrice (order, preferences, price) -> fail "" |> async.Return
    | "order.stop", SpecifiedCake (order, name)
    | "order.stop", SpecifiedCandy (order, name)
    | "order.stop", SpecifiedCookie (order, name) -> fail "" |> async.Return
    | "order.sweets.specify", StartedCandySpecifiedPriceAndWeight (order, price, weight)
    | "order.sweets.specify", StartedCookieSpecifiedPriceAndWeight (order, price, weight) -> fail "" |> async.Return
    | "order.sweets.weight", StartedCandySpecifiedPrice (order, price) ->
        trial {
            let! strings = 
                response.Entities
                |> List.filter (fun entity -> entity.Type = "builtin.number")
                |> List.head
                |> Luis.getEntityResolutionStrings

            let (success, weight) =
                strings
                |> List.head
                |> Decimal.TryParse

            let! weight =
                if success
                then weight |> ok
                else "Could not parse the weight" |> fail

            let newState = StartedCandySpecifiedPriceAndWeight (order, price, weight)

            let! responseText = getResponse intent

            return responseText, newState  
        }
        |> async.Return
    | "order.sweets.weight", StartedCookieSpecifiedPrice (order, price) -> 
        trial {
            let! strings = 
                response.Entities
                |> List.filter (fun entity -> entity.Type = "builtin.number")
                |> List.head
                |> Luis.getEntityResolutionStrings

            let (success, weight) =
                strings
                |> List.head
                |> Decimal.TryParse

            let! weight =
                if success
                then weight |> ok
                else "Could not parse the weight" |> fail

            let newState = StartedCookieSpecifiedPriceAndWeight (order, price, weight)

            let! responseText = getResponse intent

            return responseText, newState  
        }
        |> async.Return
    | _ ->
        getResponse "None"
        |> Trial.lift (fun response -> response, state)
        |> async.Return
