module SaturnPg.Controllers.EntityController

open SaturnPg.Models.Entity
open Saturn
open Controller
open Response

let mutable entityDb = [{Id = 1; Name = "Abc"}; {Id = 2; Name = "Bca"}
                        {Id = 3; Name = "MyEntity"}; {Id = 4; Name = "Entity"}]

let getEntity ctx id =
    entityDb
    |> List.tryFind (fun e -> e.Id = id)
    |> function
    | Some entity -> json ctx entity
    | None -> notFound ctx id

let getAllEntities ctx = json ctx entityDb

let createEntity ctx = task {
    let! entity = getJson ctx
    
    let entityDto =  {
        Id = entityDb.Length + 1
        Name = entity.Name
    }
    
    entityDb <- entityDb @ [entityDto]
    
    return json ctx entityDto
}

let deleteEntity ctx id =
    entityDb
    |> List.tryFind (fun e -> e.Id = id)
    |> function
    | Some _ ->
        entityDb <- entityDb |> List.filter (fun e -> e.Id <> id)
        ok ctx id
    | None -> notFound ctx id

let updateEntity ctx id = task {
    let! entity = getJson ctx
    return entityDb
    |> List.tryFindIndex (fun e -> e.Id = id)
    |> function
    | Some foundIndex ->
        entityDb <- (entityDb[..foundIndex - 1] @ [{ entity with Id = id }] @ entityDb[foundIndex + 1..])
        ok ctx entityDb[foundIndex]
    | None -> notFound ctx id
}

let entityController = controller {
    show getEntity
    index getAllEntities
    create createEntity
    delete deleteEntity
    update updateEntity
  }


