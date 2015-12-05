// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

open SQLite.Database
open System

[<EntryPoint>]
let main argv = 
    //createDatabase
    let result = insert "192.168.0.123" "2015-12-04" "int" "contract" "success"
    //IP deploymentTime environment service deploymentResult
    Console.WriteLine(result)
    
    //Console.ReadKey() |> ignore;
    0 // return an integer exit code
