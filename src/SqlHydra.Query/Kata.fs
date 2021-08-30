﻿namespace SqlHydra.Query

open SqlKata

module internal KataUtils = 

    let boxValue (value: obj) = 
        if isNull value then 
            box System.DBNull.Value
        else
            match value.GetType() with
            | t when t.IsGenericType && t.Name.StartsWith("FSharpOption") -> 
                t.GetProperty("Value").GetValue(value)
            | _ -> value
            |> function 
                | null -> box System.DBNull.Value 
                | o -> o

    let fromQuerySource (querySource: QuerySource<'T, Query>) = 
        querySource.Query
    
    let fromUpdate (updateQuery: UpdateQuerySpec<'T>) = 
        let kvps = 
            match updateQuery.Entity, updateQuery.SetValues with
            | Some entity, [] -> 
                match updateQuery.Fields with 
                | [] -> 
                    FSharp.Reflection.FSharpType.GetRecordFields(typeof<'T>) 
                    |> Array.map (fun p -> p.Name, p.GetValue(entity))
                        
                | fields -> 
                    let included = fields |> Set.ofList
                    FSharp.Reflection.FSharpType.GetRecordFields(typeof<'T>) 
                    |> Array.filter (fun p -> included.Contains(p.Name)) 
                    |> Array.map (fun p -> p.Name, p.GetValue(entity))

            | Some _, _ -> failwith "Cannot have both `entity` and `set` operations in an `update` expression."
            | None, [] -> failwith "Either an `entity` or `set` operations must be present in an `update` expression."
            | None, setValues -> setValues |> List.toArray
                    
        // Handle option values
        let preparedKvps = 
            kvps 
            |> Seq.map (fun (key,value) -> key, boxValue value)
            |> dict
            |> Seq.map id

        let q = Query(updateQuery.Table).AsUpdate(preparedKvps)

        // Apply `where` clause
        match updateQuery.Where with
        | Some where -> q.Where(fun w -> where)
        | None -> q

    let fromInsert (returnId: bool) (insertQuery: InsertQuerySpec<'T>) =
        let kvps = 
            match insertQuery.Entity with
            | Some entity -> 
                match insertQuery.Fields with 
                | [] -> 
                    FSharp.Reflection.FSharpType.GetRecordFields(typeof<'T>) 
                    |> Array.map (fun p -> p.Name, p.GetValue(entity))
                        
                | fields -> 
                    let included = fields |> Set.ofList
                    FSharp.Reflection.FSharpType.GetRecordFields(typeof<'T>) 
                    |> Array.filter (fun p -> included.Contains(p.Name)) 
                    |> Array.map (fun p -> p.Name, p.GetValue(entity))
            | None -> 
                failwith "Value not set"

        // Handle option values
        let preparedKvps = 
            kvps 
            |> Seq.map (fun (key,value) -> key, boxValue value)
            |> dict
            |> Seq.map id

        Query(insertQuery.Table).AsInsert(preparedKvps, returnId = returnId)

type Kata = 
    static member ToQuery (typedQuery: QuerySource<'T, SqlKata.Query>) = KataUtils.fromQuerySource typedQuery
    static member ToQuery (updateQuery: UpdateQuerySpec<'T>) = KataUtils.fromUpdate updateQuery
    static member ToQuery (insertQuery: InsertQuerySpec<'T>) = KataUtils.fromInsert false insertQuery
    