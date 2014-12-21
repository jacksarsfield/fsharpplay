namespace ViewModels

open System
open System.Windows

open System.Windows.Input
open System.ComponentModel
open System.Windows.Controls
open System.Windows.Forms.Integration
open Chart

type ViewModelBase() =
    let propertyChangedEvent = new DelegateEvent<PropertyChangedEventHandler>()
    interface INotifyPropertyChanged with
        [<CLIEvent>]
        member x.PropertyChanged = propertyChangedEvent.Publish
    member x.OnPropertyChanged propertyName = 
        propertyChangedEvent.Trigger([| x; new PropertyChangedEventArgs(propertyName) |])
 
type RelayCommand (canExecute:(obj -> bool), action:(obj -> unit)) =
    let event = new DelegateEvent<EventHandler>()
    interface ICommand with
        [<CLIEvent>]
        member x.CanExecuteChanged = event.Publish
        member x.CanExecute arg = canExecute(arg)
        member x.Execute arg = action(arg)


type ChartHostViewModel () = 
    inherit ViewModelBase()

    let processChartRequest startDate endDate = 
        let windowsFormsHost = new WindowsFormsHost()
        windowsFormsHost.Child <- getChart startDate endDate
        windowsFormsHost

    let mutable startDate = new DateTime(2014, 11, 1)
    let mutable endDate = new DateTime(2014, 12, 1)
    let mutable chartHost = new ContentControl()

    member x.StartDate
        with get () = startDate
        and set value = startDate <- value
                        x.OnPropertyChanged "StartDate"                      
    member x.EndDate
        with get () = endDate
        and set value = endDate <- value
                        x.OnPropertyChanged "EndDate"
    member x.ChartHost
        with get () = chartHost
        and set value = chartHost <- value
                       
    member x.OkCommand = 
        new RelayCommand ((fun canExecute -> true), 
            (fun action -> x.ChartHost.Content <- processChartRequest x.StartDate x.EndDate)) 
            