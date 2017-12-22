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
    | SpecifiedCandy                               of Food * string
    | StartedCandySpecifiedName                    of Food * string
    | StartedCookie                                of Food
    | StartedCookieSpecifiedPrice                  of Food * decimal
    | SpecifiedCookie                              of Food * string
    | StartedCookieSpecifiedName                   of Food * string
    | FinishedOrder                                of Food

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
        | StartedCandySpecifiedPrice _ -> "candy-price"     
        | SpecifiedCandy _ -> "specified-candy"
        | StartedCandySpecifiedName _ -> "candy-name"                          
        | StartedCookie _ -> "started-cookie"                            
        | StartedCookieSpecifiedPrice _ -> "cookie-price"            
        | SpecifiedCookie _ -> "specified-cookie" 
        | StartedCookieSpecifiedName _ -> "cookie-name" 
        | FinishedOrder _ -> "finished_order"

