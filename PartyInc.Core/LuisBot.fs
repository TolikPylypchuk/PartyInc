module PartyInc.Core.Bots.LuisBot

[<AbstractClass>]
type LuisBot(id: string, subscriptionKey: string) = class
    let mutable _responseVariants: Map<string, List<string>> = Map.empty

    member this.Id = id
    member this.SubscriptionKey = subscriptionKey

    member this.ResponseVariants
        with get() = _responseVariants
        and set(value: Map<string, List<string>>) = _responseVariants <- value
    
    abstract member Response: string -> string
end