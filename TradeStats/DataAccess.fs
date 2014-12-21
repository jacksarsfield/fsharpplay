module DataAccess
open System
open FSharp.Data


[<Literal>]
let connectionString = @"Server=dave\sqlexpress;Database=PowerPositions;;Integrated Security=True"

[<Literal>]
let tradeQuery = "SELECT Id, Location, Counterparty, StartDateUtc, EndDateUtc FROM powerpos.TradeView T WHERE T.Deleted = 1 AND T.StartDateUtc < @endDate AND T.EndDateUtc > @startDate"

type TradeCommand = SqlCommandProvider<tradeQuery, connectionString>

type Trade = {
    id : int32
    location : string
    counterparty : string
    startDateUtc : DateTime
    endDateUtc : DateTime }

let getTrades startDateUtc endDateUtc = 
    let cmd = new TradeCommand()
    let data = cmd.AsyncExecute(startDateUtc, endDateUtc) |> Async.RunSynchronously
    data |> Seq.map (fun row ->
    {
        id = row.Id
        location = row.Location
        counterparty = row.Counterparty
        startDateUtc = row.StartDateUtc
        endDateUtc = row.EndDateUtc
    }) |> List.ofSeq
