module SaturnPg.Router

open System.Text.Json
open Saturn
open Giraffe.Core
open SaturnPg.Controllers.EntityController

let apiRouter = router {
    not_found_handler (setStatusCode 404 >=> json (JsonSerializer.Serialize({|status = 404; message = "Page not found"|})))
    forward "/entity" entityController
}

