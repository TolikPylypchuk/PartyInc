module PartyInc.Core.Bots.LuisBot

[<AbstractClass>]
type LuisBot(id: string, subscriptionKey: string) = class
    member this.Id = id
    member this.SubscriptionKey = subscriptionKey
    
    abstract member Response: string -> string
end