module PartyInc.Core.Bots.SweetsOrderConsultantBot

open System

open PartyInc.Core.Luis
open PartyInc.Core.Bots.LuisBot

type SweetsOrderConsultantBot = class
    inherit LuisBot
    new (id: string, subscriptionKey: string) = {
        inherit LuisBot(id, subscriptionKey)
    }
    
    override __.Response query =
        let responseObj = (request __.Id __.SubscriptionKey query).Result |> parseResponse
        let responseVariants = __.ResponseVariants.Item responseObj.TopScoringIntent.Intent
        responseVariants.Item((new Random()).Next(0,responseVariants.Length))
end