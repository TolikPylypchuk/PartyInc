namespace Newtonsoft.Json.Converters

open Newtonsoft.Json

open PartyInc.Core

type ResolutionValuesConverter() =
    inherit JsonConverter()

    override __.CanConvert(t) = 
        t = typedefof<ResolutionValues>

    override __.WriteJson(writer, value, serializer) =
        if value <> null then
            let values = value :?> ResolutionValues
            match values with
            | Strings strings -> serializer.Serialize(writer, strings)
            | Objects objects -> serializer.Serialize(writer, objects)

    override __.ReadJson(reader, _, _, serializer) =
        try
            let strings = serializer.Deserialize<string list>(reader)
            strings |> Strings |> box
        with
        | _ ->
            let values = serializer.Deserialize<ResolutionValue list>(reader)
            values |> Objects |> box
