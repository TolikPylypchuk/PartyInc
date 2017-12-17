module PartyInc.Core.PartyOrganizer

[<CompiledName("ManageResponse")>]
let manageResponse (response : Response) =
    match response.TopScoringIntent.Intent with
    | "welcome" ->
        "Hi there!"
    | "start" ->
        "Awesome! I don't know what to do next, though."
    | _ ->
        "Sorry. I didn't get you. Can you repeat, please?"
