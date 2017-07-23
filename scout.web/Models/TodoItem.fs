namespace scout.web.Models

[<AllowNullLiteral>]
type TodoItem() = 
    member val Id : int64 = 0L with get, set
    member val Name : string = "" with get, set
    member val IsComplete : bool = false with get, set
