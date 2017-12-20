[<RequireQualifiedAccess>]
module PartyInc.Core.Json

open Chessie.ErrorHandling
open Newtonsoft.Json
open Newtonsoft.Json.Converters

let private converters : JsonConverter[] =
    [| OptionConverter(); ResolutionConverter(); ResolutionValuesConverter() |]

let serialize<'T> =
    Trial.catch <| fun value -> JsonConvert.SerializeObject(value, converters)

let deserialize<'T> =
    Trial.catch <| fun json -> JsonConvert.DeserializeObject<'T>(json, converters)
