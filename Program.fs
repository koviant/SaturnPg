open Microsoft.Extensions.DependencyInjection
open Saturn
open SaturnPg.Router

let app = application {
     use_developer_exceptions
     use_router apiRouter
     use_static "wwwroot"
}

run app
