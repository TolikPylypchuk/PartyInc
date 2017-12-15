module PartyInc.Core.ResponseManager

open System

open PartyInc.Core

let manageResponse response = async {
    match response.TopScoringIntent.Intent with
    | "order.all.price"
    | "order.cake"
    | "order.cake.preferences-no" 
    | "order.cake.preferences-yes"
    | "order.cake.preferences-yes-dislikes"
    | "order.cake.preferences-yes-likes"
    | "order.cake.preferences-yes-misunderstanding"
    | "order.cake.specify"
    | "order.stop"
    | "order.sweets"
    | "order.sweets.specify"
    | "order.sweets.weight"
    | "welcome" ->
        let responseChoices =
            ResponseChoices.sweetsOrderConsultant.[response.TopScoringIntent.Intent]
        return responseChoices.[Random().Next(0,responseChoices.Length)]
    | _ -> 
        return "Sorry. I didn't get you. Can you repeat, please?"
}