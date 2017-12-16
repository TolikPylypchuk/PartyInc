module PartyInc.Core.API

[<CompiledName("AsyncToTask")>]
let asyncToTask a = Async.StartAsTask a
