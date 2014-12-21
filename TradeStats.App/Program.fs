open System
open System.Windows
open System.Windows.Controls
open FSharpx

open ViewModels
open System.Windows.Forms.Integration
 
type MainWindow = XAML<"ChartHost.xaml">
 
let loadWindow() =
    let viewModel = new ChartHostViewModel()
    let window = MainWindow()
    viewModel.ChartHost <- window.Root.FindName("ChartHost") :?> ContentControl
    window.Root.DataContext <- viewModel
    window.Root
 
[<STAThread>]
(new Application()).Run(loadWindow()) |> ignore