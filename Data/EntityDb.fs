namespace SaturnPg.Data

open SaturnPg.Models

[<RequireQualifiedAccess>]
module EntityDb =
    let mutable private entities = Map [(1, {Id = 1; Name = "Abc"})
                                        (2, {Id = 2; Name = "Bca"});
                                        (3, {Id = 3; Name = "MyEntity"})
                                        (4, {Id = 4; Name = "Entity"})]

    let getById id =
        entities
        |> Map.tryFind id

    let getAll() =
        entities
        |> Map.values
        |> Seq.toList

    let create entity =
        { entity with Id = entities.Count + 1 }
        |> fun e ->
            entities <- entities |> Map.add e.Id e
            Some e

    let deleteById id =
        entities
        |> Map.tryFind id
        |> function
        | Some e ->
            entities <- entities |> Map.remove id
            Some e.Id
        | None -> None

    let update id entity =
        let getNewEntity _ =
            Some { entity with Id = id}

        entities
        |> Map.tryFind id
        |> function
        | Some _ ->
            entities <- entities |> Map.change id getNewEntity
            Some entities[id]
        | None -> None