[<RequireQualifiedAccess>]
module PartyInc.Core.ResponseChoices

open System
open System.Collections.Generic
open System.IO

open Chessie.ErrorHandling

let private getFromFile file =
    file
    |> Trial.catch File.ReadAllText
    >>= Json.deserialize<Dictionary<string, ResizeArray<ResizeArray<string>>>>
    |> Trial.lift
        (Seq.map (fun pair -> pair.Key, pair.Value)
        >> Map.ofSeq
        >> Map.map (fun _ value -> value |> Seq.map Seq.toList |> Seq.toList))

let partyOrganizer = getFromFile "Responses\\PartyOrganizer.json"

let sweetsOrderConsultant = getFromFile "Responses\\SweetsOrderConsultant.json"

let drinksOrderConsultant = getFromFile "Responses\\DrinksOrderConsultant.json"

let getResponse map intent =
    map
    |> Trial.lift
        (Map.find intent
        >> List.map (fun (list : string list) -> list.[Random().Next(list.Length)])
        >> List.reduce (sprintf "%s %s"))

let getResponseKey (intent, stateName) = sprintf "%s:%s" intent stateName
