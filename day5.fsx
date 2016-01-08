open System
open System.IO
open System.Text.RegularExpressions

let input = File.ReadAllText "inputs/day5.txt"

let ruleOne input =
    input
    |> String.filter (function
        | 'a' | 'e' | 'i' | 'o' | 'u' -> true
        | _ -> false )
    |> String.length >= 3
    
let ruleTwo input =
    input
    |> Seq.pairwise
    |> Seq.map (function | (x, y) when x = y -> true
                         | _ -> false)
    |> Seq.contains true
    
let ruleThree input =
    input
    |> Seq.pairwise
    |> Seq.map (function
                    | ('a', 'b')
                    | ('c', 'd')
                    | ('p', 'q')
                    | ('x', 'y') -> true
                    | _ -> false)
    |> Seq.contains true |> not
    
let niceTest (input: string) =
    input.Split([|'\n'|])
    |> Seq.filter (fun i -> ruleOne i && ruleTwo i && ruleThree i)
    |> Seq.length
    
niceTest input

(**
Part 2
*)

let newRuleOne input =
    Regex.IsMatch(input, @"(..).*\1")

let newRuleTwo input =
    input
    |> Seq.windowed 3
    |> Seq.exists (fun [|a; b; c|] -> a = c)
    
let niceTest2 (input: string) =
    input.Split([|'\n'|])
    |> Seq.filter (fun i -> newRuleOne i && newRuleTwo i)
    |> Seq.length
    
niceTest2 input