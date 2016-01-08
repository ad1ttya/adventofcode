let input = "3113322113"

let elvesRead (input: string) =
    input
    |> Seq.fold (fun acc x ->
        match acc with
        | (n, x')::t1 when x = x' -> (n + 1, x')::t1
        | _ -> (1, x)::acc) []
    |> List.rev
    |> Seq.collect (fun (n, x) ->
        sprintf "%d%c" n x)
    |> fun xs -> System.String.Join("", xs)
    
{1 .. 40}
|> Seq.fold (fun last _ -> elvesRead last) input
|> Seq.length

(**
Part 2
*)

{1 .. 50}
|> Seq.fold (fun last _ -> elvesRead last) input
|> Seq.length