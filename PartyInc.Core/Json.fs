[<RequireQualifiedAccess>]
module PartyInc.Core.Json

open Chessie.ErrorHandling
open Newtonsoft.Json
open Newtonsoft.Json.Converters

let serialize<'T> =
    Trial.catch <| fun value -> JsonConvert.SerializeObject(value, OptionConverter())

let deserialize<'T> =
    Trial.catch <| fun json -> JsonConvert.DeserializeObject<'T>(json, OptionConverter())
