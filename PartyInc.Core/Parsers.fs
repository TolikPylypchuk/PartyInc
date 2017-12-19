module PartyInc.Core.Parsers

open System

open FParsec
open Chessie.ErrorHandling

let pStringInQuotes : Parser<string, unit> = pstring "\"" >>. charsTillString "\"" true 1000

let pStringInSpaces str = pstring str |> between spaces spaces

let pDateTime = pStringInQuotes |>> DateTime.Parse

let pPredicate name pInner =
    pstring name >>. pStringInSpaces "(" >>. pInner .>> pStringInSpaces ")"

let pList pItem =
    pStringInSpaces "[" >>. sepBy pItem (pStringInSpaces ",") .>> pStringInSpaces "]"

let pComma : Parser<unit, unit> = pStringInSpaces "," |>> ignore

let parseInput parser input =
    match run parser input with
    | Success (result, _, _) -> result |> ok
    | Failure (error, _, _) -> error |> fail
