namespace Newtonsoft.Json.Converters

open Newtonsoft.Json

open PartyInc.Core

type ResolutionConverter() =
    inherit JsonConverter()

    override __.CanConvert(t) = 
        t = typedefof<Resolution>

    override __.WriteJson(writer, value, serializer) =
        if value <> null then
            let resolution = value :?> Resolution
            match resolution with
            | Value value -> serializer.Serialize(writer, value)
            | Values values -> serializer.Serialize(writer, values)

    override __.ReadJson(reader, _, _, serializer) =
        try
            let value = serializer.Deserialize<ResolutionValue>(reader)
            value |> Value |> box
        with
        | _ ->
            let values = serializer.Deserialize<ResolutionValues>(reader)
            values |> Values |> box
