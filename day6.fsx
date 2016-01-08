open System
open System.IO
open System.Text.RegularExpressions

let input = File.ReadAllText "inputs/day6.txt"

type State = On | Off

type CoOrd = {x:int; y:int}

let lights = Array2D.create 1000 1000 false

let through (a:CoOrd) (b:CoOrd) =
    let top = List.max [a.y; b.y]
    let bottom = List.min [a.y; b.y]
    let left = List.min [a.x; b.x]
    let right = List.max [a.x; b.x]
    [for h in left..right do
        for v in bottom..top do
            yield {x= h; y=v}]

let turnOn (targets:List<CoOrd>) =
    List.map (fun (target:CoOrd) -> lights.[target.x, target.y] <- true) targets

let turnOff (targets:List<CoOrd>) =
    List.map (fun (target:CoOrd) -> lights.[target.x, target.y] <- false) targets

let toggle (targets:List<CoOrd>) =
    List.map (fun (target:CoOrd) -> lights.[target.x, target.y] <- not lights.[target.x, target.y]) targets


let parse (x:string) =
    let parseCoOrd (c:string) =
        let splitCoOrd = c.Split[|','|]
        let xAxis = System.Int32.Parse splitCoOrd.[0]
        let yAxis = System.Int32.Parse splitCoOrd.[1]
        {x = xAxis; y = yAxis}

    let command = x.Split[|' '|]
    let throughIndex = command |> Array.findIndex (function | "through" -> true | _ -> false)
    let leftCoOrd = command.[throughIndex - 1] |> parseCoOrd
    let rightCoOrd = command.[throughIndex + 1] |> parseCoOrd
    let lights = through leftCoOrd rightCoOrd

    match command.[0] with
    | "turn" -> match command.[1] with
                | "on" -> turnOn lights
                | "off" -> turnOff lights
    | "toggle" -> toggle lights |> Seq.toList
    
input.Split[|'\n'|]
|> Array.map parse

lights |> Seq.cast<bool> |> Seq.filter id |> Seq.length

(**
part 2
*)

let lights2 = Array2D.create 1000 1000 0

let turnOn2 (targets:List<CoOrd>) =
    List.map (fun (target:CoOrd) -> lights2.[target.x, target.y] <- lights2.[target.x, target.y] + 1) targets

let turnOff2 (targets:List<CoOrd>) =
    targets |>
    List.map (fun (target:CoOrd) -> match lights2.[target.x, target.y] with
                                    | 0 -> ()
                                    | _ -> lights2.[target.x, target.y] <- lights2.[target.x, target.y] - 1)

let toggle2 (targets:List<CoOrd>) =
    List.map (fun (target:CoOrd) -> lights2.[target.x, target.y] <- lights2.[target.x, target.y] + 2) targets
    
let parse2 (x:string) =
    let parseCoOrd (c:string) =
        let splitCoOrd = c.Split[|','|]
        let xAxis = System.Int32.Parse splitCoOrd.[0]
        let yAxis = System.Int32.Parse splitCoOrd.[1]
        {x = xAxis; y = yAxis}

    let command = x.Split[|' '|]
    let throughIndex = command |> Array.findIndex (function | "through" -> true | _ -> false)
    let leftCoOrd = command.[throughIndex - 1] |> parseCoOrd
    let rightCoOrd = command.[throughIndex + 1] |> parseCoOrd
    let lights = through leftCoOrd rightCoOrd

    match command.[0] with
    | "turn" -> match command.[1] with
                | "on" -> turnOn2 lights
                | "off" -> turnOff2 lights
    | "toggle" -> toggle2 lights |> Seq.toList
    
input.Split[|'\n'|] 
|> Array.map parse2

lights2 |> Seq.cast<int> |> Seq.reduce (+)