module PartyInc.Core.SweetsOrderConsultant

open System

open Chessie.ErrorHandling
open PartyInc.Core

[<CompiledName("HandleResponse")>]
let handleResponse (response, state) =
    let getResponse = ResponseChoices.getResponse ResponseChoices.sweetsOrderConsultant

    let intent = response.TopScoringIntent.Intent

    match intent, state with
    | "order.all.price", StartedCakeStartedPreferences (order, preferences) -> 
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

            //ToDo
            //get all cakes with suitable price and ingredients and if there are some
            //replace {...} in responseText with their data
            //otherwise another responseText create and state doesn't change

            let newState = StartedCakeFinishedPreferencesSpecifiedPrice (order, preferences, price)
            
            let! responseTextBase = getResponse intent

            let responseText = responseTextBase.Replace("{candies|cookies|cakes}", "cakes")

            return responseText, newState 
        }
        |> async.Return
    | "order.all.price", StartedCake order -> 
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
                
            //ToDo
            //get all cakes with suitable price and if there are some
            //replace {...} in responseText with their data
            //otherwise another responseText create and state doesn't change

            let newState = StartedCakeSpecifiedPrice (order, price)
            
            let! responseTextBase = getResponse intent

            let responseText = responseTextBase.Replace("{candies|cookies|cakes}", "cakes")

            return responseText, newState 
        }
        |> async.Return
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

            //ToDo
            //get all candies with suitable price and if there are some
            //replace {...} in responseText with their data
            //otherwise another responseText create and state doesn't change

            let newState = StartedCandySpecifiedPrice (order, price)

            let! responseTextBase = getResponse intent

            let responseText = responseTextBase.Replace("{candies|cookies|cakes}", "candies")

            return responseText, newState 
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

            //ToDo
            //get all cookies with suitable price and if there are some
            //replace {...} in responseText with their data
            //otherwise another responseText create and state doesn't change

            let newState = StartedCookieSpecifiedPrice (order, price)
            
            let! responseTextBase = getResponse intent

            let responseText = responseTextBase.Replace("{candies|cookies|cakes}", "cookies")

            return responseText, newState 
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
    | "order.cake.preferences-no", StartedCake order ->
        getResponse intent
        |> Trial.lift (fun responseText -> responseText, StartedCake order)
        |> async.Return
    | "order.cake.preferences-yes", StartedCake order ->
        getResponse intent
        |> Trial.lift (fun responseText -> 
            responseText, 
            StartedCakeStartedPreferences (order, { Include = []; Exclude = [] }))
        |> async.Return
    | "order.cake.preferences-yes-dislikes", StartedCakeStartedPreferences (order, preferences) ->
        trial {
            let! responseText = getResponse intent
        
            let! ingredients = 
                response.Entities
                |> List.filter (fun entity -> entity.Type = "ingredients")
                |> List.map (Luis.getEntityResolutionStrings >> Trial.lift List.head)
                |> Trial.sequence
                
            let newPreferences = { preferences with Exclude = List.concat[preferences.Exclude; ingredients]}

            let newState = StartedCakeStartedPreferences (order, newPreferences)

            return responseText, newState 
        }
        |> async.Return
    | "order.cake.preferences-yes-likes", StartedCakeStartedPreferences (order, preferences) ->
        trial {
            let! responseText = getResponse intent
        
            let! ingredients = 
                response.Entities
                |> List.filter (fun entity -> entity.Type = "ingredients")
                |> List.map (Luis.getEntityResolutionStrings >> Trial.lift List.head)
                |> Trial.sequence
                
            let newPreferences = { preferences with Include = List.concat[preferences.Include; ingredients]}

            let newState = StartedCakeStartedPreferences (order, newPreferences)

            return responseText, newState 
        }
        |> async.Return
    | "order.cake.preferences-yes-misunderstanding", StartedCakeStartedPreferences (order, preferences) ->
        trial {
            let! responseTextBase = getResponse intent
        
            let! ingredients = 
                response.Entities
                |> List.filter (fun entity -> entity.Type = "ingredients")
                |> List.map (Luis.getEntityResolutionStrings >> Trial.lift List.head)
                |> Trial.sequence
                
            let responseText = responseTextBase.Replace("{ingredients}", ingredients |> List.reduce (sprintf "%s, %s"))

            return responseText, state 
        }
        |> async.Return
    | "order.cake.specify", StartedCakeSpecifiedPrice (order, price) -> 
        trial {
            //ToDo
            //add cake to order

            let newState = SweetsNotEmptyOrder order

            let! responseText = getResponse intent

            return responseText, newState  
        }
        |> async.Return
    | "order.cake.specify", StartedCakeFinishedPreferencesSpecifiedPrice (order, preferences, price) -> 
        trial {
            //ToDo
            //add cake to order

            let newState = SweetsNotEmptyOrder order

            let! responseText = getResponse intent

            return responseText, newState  
        }
        |> async.Return
    | "order.stop", SweetsNotEmptyOrder order ->
        getResponse intent
        |> Trial.lift (fun responseText -> responseText, FinishedOrder order)
        |> async.Return
    | "order.sweets.specify", StartedCandySpecifiedPrice (order, price) ->
        trial {
            //ToDo
            //get candy name

            let newState = StartedCandySpecifiedName (order, "")
            
            let! responseText = getResponse intent

            return responseText, newState 
        }
        |> async.Return
    | "order.sweets.specify", StartedCookieSpecifiedPrice (order, price) ->
        trial {
            //ToDo
            //get cookie name

            let newState = StartedCookieSpecifiedName (order, "")
            
            let! responseText = getResponse intent

            return responseText, newState 
        }
        |> async.Return
    | "order.sweets.weight", StartedCandySpecifiedName (order, name) ->
        trial {
            let! value = 
                response.Entities
                |> List.filter (fun entity -> entity.Type = "builtin.number")
                |> List.head
                |> Luis.getEntityResolutionValue
                
            let (success, weight) = value |> Decimal.TryParse

            let! weight =
                if success
                then weight |> ok
                else "Could not parse the weight" |> fail
            
            //ToDo
            //add candy to order

            let newState = SweetsNotEmptyOrder order

            let! responseText = getResponse intent

            return responseText, newState  
        }
        |> async.Return
    | "order.sweets.weight", StartedCookieSpecifiedName (order, price) -> 
        trial {
            let! value = 
                response.Entities
                |> List.filter (fun entity -> entity.Type = "builtin.number")
                |> List.head
                |> Luis.getEntityResolutionValue
                
            let (success, weight) = value |> Decimal.TryParse

            let! weight =
                if success
                then weight |> ok
                else "Could not parse the weight" |> fail
                
            //ToDo
            //add cookie to order

            let newState = SweetsNotEmptyOrder order

            let! responseText = getResponse intent

            return responseText, newState  
        }
        |> async.Return
    | _ ->
        getResponse "None"
        |> Trial.lift (fun response -> response, state)
        |> async.Return
