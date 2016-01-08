open System
open System.IO

let input = File.ReadAllText "inputs/day3.txt"

let mutable positions = ""
let mutable x = 0
let mutable y = 0

(* ^ -> 0,1 > -> 1,0 v -> 0,-1 < -> -1,0 *)

input
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
    
(**
Part 2
*)

let move ((x, y)::_ as acc) dir =
    match dir with
    | '^' -> (x, y - 1)::acc
    | '>' -> (x + 1, y)::acc
    | 'v' -> (x, y + 1)::acc
    | '<' -> (x - 1, y)::acc
    
input
    |> Seq.mapi (fun i x -> i % 2, x)
    |> Seq.groupBy fst
    |> Seq.map (fun (_, gr) -> gr |> Seq.map snd)
    |> Seq.collect (Seq.fold move [(0, 0)])
    |> Seq.distinct
    |> Seq.length