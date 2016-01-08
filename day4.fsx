open System
open System.Security.Cryptography

let md5 = MD5.Create()

let secretKey = "bgvyzdsv"

let computeMD5Hash (input: string) =
    let inputBytes = System.Text.Encoding.ASCII.GetBytes input
    md5.ComputeHash inputBytes
    |> BitConverter.ToString
    
computeMD5Hash secretKey

Seq.initInfinite (fun i ->
    (computeMD5Hash(secretKey + string i), secretKey + string i) )
|> Seq.filter (fun (hash, input) -> hash.StartsWith("00-00-0"))
|> Seq.head


"bgvyzdsv254575"

(**
Part 2 - Starting with six zeroes
*)

Seq.initInfinite (fun i ->
    (computeMD5Hash(secretKey + string i), secretKey + string i) )
|> Seq.filter (fun (hash, input) -> hash.StartsWith("00-00-00"))
|> Seq.head