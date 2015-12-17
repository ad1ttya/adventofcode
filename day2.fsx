open System
open System.IO

let input = File.ReadLines @"C:\Workspace\FSharp\Explore\Explore\adv2.txt"

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