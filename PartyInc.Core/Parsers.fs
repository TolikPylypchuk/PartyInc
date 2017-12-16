module PartyInc.Core.Parsers

open FParsec

type Parser<'T> = Parser<'T, unit>

let pStringInQuotes: Parser<_> = pstring "\"" >>. charsTillString "\"" false 1000

let parseString = run pStringInQuotes
