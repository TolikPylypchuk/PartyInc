namespace Newtonsoft.Json.Converters

open Newtonsoft.Json

open PartyInc.Core

type ResolutionValueUnionConverter() =
    inherit JsonConverter()

    override __.CanConvert(t) = 
        t = typedefof<ResolutionValueUnion>

    override __.WriteJson(writer, value, serializer) =
        if value <> null then
            let values = value :?> ResolutionValueUnion
            match values with
            | StringValue string -> serializer.Serialize(writer, string)
            | ResolutionValue object -> serializer.Serialize(writer, object)

    override __.ReadJson(reader, _, _, serializer) =
        if reader.TokenType = JsonToken.String then
            let string = serializer.Deserialize<string>(reader)
            string |> StringValue |> box
        else
            let value = serializer.Deserialize<ResolutionValue>(reader)
            value |> ResolutionValue |> box
