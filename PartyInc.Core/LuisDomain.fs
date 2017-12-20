namespace PartyInc.Core

type Intent = {
    Intent: string
    Score: float
}

type ResolutionValue = {
    Value: string
}

type ResolutionValueUnion =
    | StringValue of string
    | ResolutionValue of ResolutionValue

type ResolutionValues = {
    Values: ResolutionValueUnion list
}

type Resolution =
    | Value of ResolutionValue
    | Values of ResolutionValues

type Entity = {
    Entity: string
    Type: string
    StartIndex: int
    EndIndex: int
    Score: float option
    Resolution: Resolution
}

type Child = {
    Type: string
    Value: string
}

type CompositeEntity = {
    ParentType: string
    Value: string
    Children: Child list
}

type Response = {
    Query: string
    TopScoringIntent: Intent
    Intents: Intent list
    Entities: Entity list
    CompositeEntities: CompositeEntity list option
}
