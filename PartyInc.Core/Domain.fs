namespace PartyInc.Core

open System

type Cake = {
    Name: string
    Ingredients: string list
    Price: decimal
}

type Sweet = {
    Name: string
    Weight: float
    Price: decimal
}

type Food = {
    Cake: Cake
    Cookies: Sweet list
    Candies: Sweet list
}

type Drink = {
    Name: string
    Ingredients: string list
    Amount: int
    Price: decimal
    Volume: float
}

type Order = {
    DateTime: DateTime
    Address: string
    Type: string
    MinAge: int
    MaxAge: int
    Food: Food
    Drinks: Drink list
}

//This type is used to store the ingredients which should and shouldn't be in a recipe
type Preferences = {
    Positive: string list
    Negative: string list
}
