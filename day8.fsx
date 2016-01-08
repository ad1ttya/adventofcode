open System.IO

let input = File.ReadAllLines "inputs/day8.txt"

let (|IsEscaped|_|) = function
    | '\\' :: '\\' :: rest -> Some ('\\', rest)
    | '\\' :: '\"' :: rest -> Some ('\"', rest)
    | '\\' :: 'x' :: x :: y :: rest ->
        let char = sprintf "0x%c%c" x y |> int |> char
        Some (char, rest)
    | _ -> None

let read (input: string) =
    let chars = input.Trim('\"') |> Seq.toList

    let rec read acc = function
        | IsEscaped (char, rest) -> read (char::acc) rest
        | hd::rest -> read (hd::acc) rest
        | [] -> acc

    read [] chars

let codeSize = input |> Array.sumBy Seq.length
let memSize = input |> Array.sumBy (read >> Seq.length)

let answer = codeSize - memSize

// Part 2

let encode (input: string) =
    let rec encode acc = function
        | '\"' :: rest -> encode ('\\'::'\"'::acc) rest
        | '\\' :: rest -> encode ('\\'::'\\'::acc) rest
        | hd :: rest -> encode (hd::acc) rest
        | [] -> acc
    
    [
        yield '\"'
        yield! input |> Seq.toList |> encode []
        yield '\"'
    ]

let codeSize2 = input |> Array.sumBy Seq.length
let memSize2 = input |> Array.sumBy (encode >> Seq.length)

let answer2 = memSize2 - codeSize