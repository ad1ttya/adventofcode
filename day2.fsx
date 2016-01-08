open System
open System.IO

let input = File.ReadLines "inputs/day2.txt"

let mutable total = 0

input
    |> Seq.iter (fun x ->
        let dim = x.Split 'x'
        let length = int dim.[0]
        let width = int dim.[1]
        let height = int dim.[2]
        let surface = (2 * length * width) + (2 * width * height) + (2 * height * length)
        let extra = List.min [length * width; width * height; height * length]
        total <- surface + total + extra )
        
total

(**
Part 2
*)

let input2 = File.ReadAllText "inputs/day2.txt"

let split (by: char) (x: string) =
    x.Split(
        [| by |],
        StringSplitOptions.RemoveEmptyEntries
    )

input2.Split '\n'
    |> Array.map (split 'x' >> Array.map int)
    |> Array.sumBy (fun ([| l; w; h |] as dim) ->
        let [| l'; w' |] = (Array.sort dim).[0..1]
        let wrap = l' + l' + w' + w'
        let bow = l * w * h
        wrap + bow)