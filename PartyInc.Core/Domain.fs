namespace PartyInc.Core

open System

type Cake = {
    Name: string
    Ingredients: string list
    Price: decimal
}

type SweetInfo = {
    Name: string
    Price: decimal
    Weight: decimal
}

type Cookie = Cookie of SweetInfo

type Candy = Candy of SweetInfo

type Food = {
    Cake: Cake
    Cookies: Cookie list
    Candies: Candy list
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

type Preferences = {
    Include: string list
    Exclude: string list
}
