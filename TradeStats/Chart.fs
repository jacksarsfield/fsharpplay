module Chart

open FSharp.Charting
open FSharp.Charting.ChartTypes
open Aggregator

let getChart startDate endDate =
    let tradeGroups = getTradeGroups startDate endDate Grouping.Location
    let columnChart =  Chart.Column tradeGroups        
    new ChartControl(columnChart)