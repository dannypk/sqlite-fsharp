namespace SQLite

open System.Data.SQLite

module Database =

    type Result = Success | Failure  
    
    let databaseName = "..\hr-cooper.logging.sqlite"
    let connectionString = sprintf "Data Source=%s;Version=3" databaseName

    let tableStructure = @"CREATE TABLE IF NOT EXISTS DEPLOYMENT (
                            _id INTEGER PRIMARY KEY AUTOINCREMENT,
                            IpAddress TEXT,
                            DeploymentTime TEXT,
                            Environment TEXT,
                            Service TEXT,
                            DeploymentResult TEXT)"
                          
    let insertQuery IP deploymentTime environment service deploymentResult = 
        sprintf @"INSERT INTO DEPLOYMENT (IpAddress, DeploymentTime, Environment, Service, DeploymentResult)
                    values ('%s', '%s', '%s', '%s', '%s')" IP deploymentTime environment service deploymentResult

    let private createTable() = 
        try
            let cn = new SQLiteConnection(connectionString)
            cn.Open()

            let createTableScript = tableStructure
            let cmd = new SQLiteCommand(createTableScript, cn)
            let result = cmd.ExecuteNonQuery()
            cn.Close()
            
            Success
        with
            | _ -> Failure


    let insert IP deploymentTime environment service deploymentResult = 
        if createTable() = Success 
            then
                try
                    let cn = new SQLiteConnection(connectionString)
                    cn.Open()

                    let sql = insertQuery IP deploymentTime environment service deploymentResult
                    let cmd = new SQLiteCommand(sql, cn)
                    let result = cmd.ExecuteNonQuery()
        
                    cn.Close()
                    
                    if result = -1 then Failure
                    else Success
                with 
                    | _ -> Failure

            else Failure 
                