namespace scout.web.Controllers
open Microsoft.AspNetCore.Mvc
open System.Linq
open scout.web.Models

module Impl = 
    let addItemIfEmpty (context: TodoContext) = 
        if context.TodoItems.Count() = 0 then
            context.TodoItems.Add(new TodoItem(Name = "Item1")) |> ignore
            context.SaveChanges() |> ignore

    let getAllItems (context: TodoContext) = 
        context.TodoItems.ToList()

    let getItemById (context: TodoContext) (id: int64) = 
        match context.TodoItems.FirstOrDefault(fun item -> item.Id = id) with 
        | null -> None 
        | item -> Some item

    let toActionResult (opt: 'a Option) =
        match opt with 
        | Some x -> ObjectResult(x) :> ActionResult
        | None -> NotFoundResult() :> ActionResult


[<Route("api/[controller]")>]
type TodoController(context: TodoContext) =
    inherit Controller()
    let context = context
    do match context.TodoItems with 
        | null -> ()
        | items -> Impl.addItemIfEmpty context

    [<HttpGet>]
    member this.GetAll() = Impl.getAllItems context

    [<HttpGet("{id}", Name = "GetTodo")>]
    member this.GetById(id: int64) = 
        id
        |> Impl.getItemById context
        |> Impl.toActionResult
