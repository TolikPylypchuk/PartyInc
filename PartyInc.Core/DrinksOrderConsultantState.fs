namespace PartyInc.Core

type DrinksOrderConsultantState =
    | DrinkEmptyOrder
    | PriceAsked
    | DrinkTypeAsked        of decimal
    | DrinkIngredientsAsked of decimal * string
    | DrinkFinishedOrder    of Drink

[<RequireQualifiedAccess>]
module DrinksOrderConsultantState =

    [<CompiledName("Initial")>]
    let initial = DrinkEmptyOrder

    let getName =
        function
        | DrinkEmptyOrder -> "drink-empty-order"
        | PriceAsked -> "price-asked"                            
        | DrinkFinishedOrder _ -> "finished_order"

