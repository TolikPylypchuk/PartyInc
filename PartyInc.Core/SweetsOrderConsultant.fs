module PartyInc.Core.SweetsOrderConsultant

open Chessie.ErrorHandling

[<CompiledName("HandleResponse")>]
let handleResponse (response, state) =
    let getResponse = ResponseChoices.getResponse ResponseChoices.sweetsOrderConsultant

    let intent = response.TopScoringIntent.Intent

    match intent, state with
    | "order.all.price", StartedCakeSpecifiedPrice (order, price) -> fail ""
    | "order.all.price", StartedCakeFinishedPreferencesSpecifiedPrice (order, preferences, price) -> fail ""
    | "order.all.price", StartedCandySpecifiedPrice (order, decimal)
    | "order.all.price", StartedCookieSpecifiedPrice (order, decimal) -> fail ""
    | "order.cake", SweetsEmptyOrder -> fail ""
    | "order.cake.preferences-no", StartedCake order -> fail ""
    | "order.cake.preferences-yes", StartedCake order -> fail ""
    | "order.cake.preferences-yes-dislikes", StartedCakeStartedPreferences (order, preferences)
    | "order.cake.preferences-yes-likes", StartedCakeStartedPreferences (order, preferences) -> fail ""
    | "order.cake.preferences-yes-misunderstanding", StartedCakeStartedPreferences (order, preferences) -> fail ""
    | "order.cake.specify", StartedCakeSpecifiedPrice (order, price) -> fail ""
    | "order.cake.specify", StartedCakeFinishedPreferencesSpecifiedPrice (order, preferences, price) -> fail ""
    | "order.stop", SpecifiedCake (order, name)
    | "order.stop", SpecifiedCandy (order, name)
    | "order.stop", SpecifiedCookie (order, name) -> fail ""
    | "order.sweets", SweetsEmptyOrder -> fail ""
    | "order.sweets.specify", StartedCandySpecifiedPriceAndWeight (order, price, weight)
    | "order.sweets.specify", StartedCookieSpecifiedPriceAndWeight (order, price, weight) -> fail ""
    | "order.sweets.weight", StartedCandySpecifiedPrice (order, price)
    | "order.sweets.weight", StartedCookieSpecifiedPrice (order, price) -> fail ""
    | _ ->
        getResponse "None"
        |> Trial.lift (fun response -> response, state)
