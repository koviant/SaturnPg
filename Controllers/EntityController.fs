module SaturnPg.Controllers.EntityController

open Saturn
open Controller
open Response
open SaturnPg.Data

let getEntity ctx id =
    EntityDb.getById id
    |> function
       | Some entity -> ok ctx entity
       | None -> notFound ctx id

let getAllEntities ctx =
    EntityDb.getAll()
    |> ok ctx

let createEntity ctx = task {
    let! entity = getJson ctx
    
    return entity
    |> EntityDb.create
    |> function
       | Some e -> created ctx e
       | None -> internalError ctx entity
}

let deleteEntity ctx id =
    EntityDb.deleteById id
    |> function
       | Some _ -> ok ctx id
       | None -> notFound ctx id

let updateEntity ctx id = task {
    let! updatedEntity = getJson ctx
    return EntityDb.update id updatedEntity
    |> function
       | Some e -> ok ctx e
       | None -> notFound ctx id
}

let entityController = controller {
    show getEntity
    index getAllEntities
    create createEntity
    delete deleteEntity
    update updateEntity
  }


