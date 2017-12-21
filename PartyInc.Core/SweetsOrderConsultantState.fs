namespace PartyInc.Core

type SweetsOrderConsultantState =
    | SweetsEmptyOrder
    | SweetsNotEmptyOrder                          of Food
    | StartedCake                                  of Food
    | StartedCakeStartedPreferences                of Food * Preferences
    | StartedCakeSpecifiedPrice                    of Food * decimal
    | StartedCakeFinishedPreferences               of Food * Preferences
    | StartedCakeFinishedPreferencesSpecifiedPrice of Food * Preferences * decimal
    | SpecifiedCake                                of Food * string
    | StartedCandy                                 of Food
    | StartedCandySpecifiedPrice                   of Food * decimal
    | StartedCandySpecifiedPriceAndWeight          of Food * decimal * decimal
    | SpecifiedCandy                               of Food * string
    | StartedCookie                                of Food
    | StartedCookieSpecifiedPrice                  of Food * decimal
    | StartedCookieSpecifiedPriceAndWeight         of Food * decimal * decimal
    | SpecifiedCookie                              of Food * string

[<RequireQualifiedAccess>]
module SweetsOrderConsultantState =

    [<CompiledName("Initial")>]
    let initial = SweetsEmptyOrder

    let getName =
        function
        | SweetsEmptyOrder -> "sweets-empty-order"
        | SweetsNotEmptyOrder _ -> "sweets-not-empty-order"
        | StartedCake _ -> "started-cake"                            
        | StartedCakeStartedPreferences _ -> "cake-started-preferences"             
        | StartedCakeSpecifiedPrice _ -> "cake-price" 
        | StartedCakeFinishedPreferences _ -> "cake-finished-preferences"             
        | StartedCakeFinishedPreferencesSpecifiedPrice _ -> "cake-finished-preferences-price"
        | SpecifiedCake _ -> "specified-cake"                              
        | StartedCandy _ -> "started-candy"                              
        | StartedCandySpecifiedPrice _ -> "sweets-price"     
        | StartedCandySpecifiedPriceAndWeight _ -> "candy-price-weight"
        | SpecifiedCandy _ -> "specified-candy"                          
        | StartedCookie _ -> "started-cookie"                            
        | StartedCookieSpecifiedPrice _ -> "sweets-price"        
        | StartedCookieSpecifiedPriceAndWeight _ -> "cookie-price-weight"        
        | SpecifiedCookie _ -> "specified-cookie"                       

