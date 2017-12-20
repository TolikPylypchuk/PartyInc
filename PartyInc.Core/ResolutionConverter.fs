namespace Newtonsoft.Json.Converters

open Newtonsoft.Json

open PartyInc.Core
open Newtonsoft.Json.Linq

type ResolutionConverter() =
    inherit JsonConverter()

    override __.CanConvert(t) = 
        t = typedefof<Resolution>

    override __.WriteJson(writer, value, serializer) =
        if value <> null then
            let values = value :?> Resolution
            match values with
            | Value value -> serializer.Serialize(writer, value)
            | Values values -> serializer.Serialize(writer, values)

    override __.ReadJson(reader, _, _, serializer) =
        let jObject = JObject.Load(reader)
        if jObject.First.Path = "values" then
            let values = serializer.Deserialize<ResolutionValues>(new JTokenReader(jObject))
            values |> Values |> box
        else
            let value = serializer.Deserialize<ResolutionValue>(new JTokenReader(jObject))
            value |> Value |> box
