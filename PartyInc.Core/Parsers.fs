module PartyInc.Core.Parsers

open FParsec

type Parser<'T> = Parser<'T, unit>

let pStringInQuotes : Parser<string> = pstring "\"" >>. charsTillString "\"" true 1000

let pStringInSpaces str = pstring str |> between spaces spaces

let pPredicate name pInner =
    pstring name >>. pStringInSpaces "(" >>. pInner .>> pStringInSpaces ")"

let pList pItem =
    pStringInSpaces "[" >>. sepBy pItem (pStringInSpaces ",") .>> pStringInSpaces "]"
