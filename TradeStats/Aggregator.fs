module Aggregator

open DataAccess

type Grouping = Location | Counterparty

let getGroup grouping =
    match grouping with
    | Location -> (fun (t : Trade) -> t.location)
    | Counterparty -> (fun (t : Trade) -> t.counterparty)

let getTradeGroups startDateUtc endDateUtc grouping = 
    let trades = getTrades startDateUtc endDateUtc
    trades |> Seq.groupBy (getGroup grouping) 
           |> Seq.map (fun (key, group) -> key, (group |> Seq.length))
   
