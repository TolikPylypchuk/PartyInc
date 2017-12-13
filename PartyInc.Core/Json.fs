[<RequireQualifiedAccess>]
module PartyInc.Core.Json

open Newtonsoft.Json
open Newtonsoft.Json.Converters

let serialize value = JsonConvert.SerializeObject(value, OptionConverter())

let deserialize<'T> json = JsonConvert.DeserializeObject<'T>(json, OptionConverter())
