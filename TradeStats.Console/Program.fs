open System
open DataAccess
open Aggregator

[<EntryPoint>]
let main argv = 
    let startDate = new DateTime(2014, 11, 1)
    let endDate = new DateTime(2014, 12, 1)
    //let trades = getTrades startDate endDate
    //trades |> Seq.iter (fun x -> printf "id: %i counterparty:%s location: %s\r\n" x.id x.counterparty x.location)

    let tradeGroups = getTradeGroups startDate endDate Grouping.Counterparty 
    tradeGroups |> Seq.iter (fun x -> printf "%A\r\n"  x)
    0 // return an integer exit code
