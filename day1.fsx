let all = ["((())()("; "()()()";] 
(* Split list into lines when too many characters *)

let final = String.concat "" all

let mutable count = 0

final
    |> Seq.iter (fun x ->
            match x with
            | '(' -> count <- count + 1
            | ')' -> count <- count - 1
            | _ -> failwith "Exception" )
            
printfn "Count: %A" count