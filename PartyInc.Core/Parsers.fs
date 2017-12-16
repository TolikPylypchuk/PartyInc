module PartyInc.Core.Parsers

open FParsec

type Parser<'T> = Parser<'T, unit>

let pStringInQuotes : Parser<string> = pstring "\"" >>. charsTillString "\"" true 1000

let pStringInSpaces str = pstring str |> between spaces spaces

let pInParens pInner = pStringInSpaces "(" >>. pInner .>> pStringInSpaces ")"

let pInBrackets pInner = pStringInSpaces "[" >>. pInner .>> pStringInSpaces "]"

let pPredicate name pInner = pstring name >>. pInParens pInner
