namespace scout.web.Models

open Microsoft.EntityFrameworkCore

type TodoContext(options: DbContextOptions<TodoContext>) = 
    inherit DbContext(options)

    [<DefaultValue>] val mutable todoItems : DbSet<TodoItem>
    member x.TodoItems with get() = x.todoItems and set v = x.todoItems <- v
    