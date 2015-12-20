open System
open System.IO

let file = File.ReadAllText @"C:\Workspace\adventofcode\adv3.txt"

let mutable positions = ""
let mutable x = 0
let mutable y = 0

(* ^ -> 0,1 > -> 1,0 v -> 0,-1 < -> -1,0 *)

file
    |> Seq.iter(fun a ->
        match a with
        | '^' -> y <- y + 1; positions <- positions + string(x) + "," + string(y) + ";"
        | 'v' -> y <- y - 1; positions <- positions + string(x) + "," + string(y) + ";"
        | '>' -> x <- x + 1; positions <- positions + string(x) + "," + string(y) + ";"
        | '<' -> x <- x - 1; positions <- positions + string(x) + "," + string(y) + ";"
        | _ -> failwith "exception") 

let res =
    let len = positions.Split ';'
                |> Seq.distinct
                |> Seq.length
    len