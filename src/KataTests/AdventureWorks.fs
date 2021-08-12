// This code was generated by SqlHydra.SqlServer.
namespace AdventureWorks

type Column(reader: System.Data.IDataReader, getOrdinal: string -> int, column) =
        member __.Name = column
        member __.IsNull() = getOrdinal column |> reader.IsDBNull
        override __.ToString() = __.Name

type RequiredColumn<'T, 'Reader when 'Reader :> System.Data.IDataReader>(reader: 'Reader, getOrdinal, getter: int -> 'T, column) =
        inherit Column(reader, getOrdinal, column)
        member __.Read(?alias) = alias |> Option.defaultValue __.Name |> getOrdinal |> getter

type OptionalColumn<'T, 'Reader when 'Reader :> System.Data.IDataReader>(reader: 'Reader, getOrdinal, getter: int -> 'T, column) =
        inherit Column(reader, getOrdinal, column)
        member __.Read(?alias) = 
            match alias |> Option.defaultValue __.Name |> getOrdinal with
            | o when reader.IsDBNull o -> None
            | o -> Some (getter o)

type RequiredBinaryColumn<'T, 'Reader when 'Reader :> System.Data.IDataReader>(reader: 'Reader, getOrdinal, getValue: int -> obj, column) =
        inherit Column(reader, getOrdinal, column)
        member __.Read(?alias) = alias |> Option.defaultValue __.Name |> getOrdinal |> getValue :?> byte[]

type OptionalBinaryColumn<'T, 'Reader when 'Reader :> System.Data.IDataReader>(reader: 'Reader, getOrdinal, getValue: int -> obj, column) =
        inherit Column(reader, getOrdinal, column)
        member __.Read(?alias) = 
            match alias |> Option.defaultValue __.Name |> getOrdinal with
            | o when reader.IsDBNull o -> None
            | o -> Some (getValue o :?> byte[])
        
module dbo =
    [<CLIMutable>]
    type ErrorLog =
        { ErrorLogID: int
          ErrorTime: System.DateTime
          UserName: string
          ErrorNumber: int
          ErrorMessage: string
          ErrorSeverity: Option<int>
          ErrorState: Option<int>
          ErrorProcedure: Option<string>
          ErrorLine: Option<int> }

    type ErrorLogReader(reader: System.Data.SqlClient.SqlDataReader, getOrdinal) =
        member __.ErrorLogID = RequiredColumn(reader, getOrdinal, reader.GetInt32, "ErrorLogID")
        member __.ErrorTime = RequiredColumn(reader, getOrdinal, reader.GetDateTime, "ErrorTime")
        member __.UserName = RequiredColumn(reader, getOrdinal, reader.GetString, "UserName")
        member __.ErrorNumber = RequiredColumn(reader, getOrdinal, reader.GetInt32, "ErrorNumber")
        member __.ErrorMessage = RequiredColumn(reader, getOrdinal, reader.GetString, "ErrorMessage")
        member __.ErrorSeverity = OptionalColumn(reader, getOrdinal, reader.GetInt32, "ErrorSeverity")
        member __.ErrorState = OptionalColumn(reader, getOrdinal, reader.GetInt32, "ErrorState")
        member __.ErrorProcedure = OptionalColumn(reader, getOrdinal, reader.GetString, "ErrorProcedure")
        member __.ErrorLine = OptionalColumn(reader, getOrdinal, reader.GetInt32, "ErrorLine")
        member __.Read() =
            { ErrorLogID = __.ErrorLogID.Read()
              ErrorTime = __.ErrorTime.Read()
              UserName = __.UserName.Read()
              ErrorNumber = __.ErrorNumber.Read()
              ErrorMessage = __.ErrorMessage.Read()
              ErrorSeverity = __.ErrorSeverity.Read()
              ErrorState = __.ErrorState.Read()
              ErrorProcedure = __.ErrorProcedure.Read()
              ErrorLine = __.ErrorLine.Read() }

        member __.ReadIfNotNull() =
            if __.ErrorLogID.IsNull() then None else Some(__.Read())

    [<CLIMutable>]
    type BuildVersion =
        { SystemInformationID: byte
          ``Database Version``: string
          VersionDate: System.DateTime
          ModifiedDate: System.DateTime }

    type BuildVersionReader(reader: System.Data.SqlClient.SqlDataReader, getOrdinal) =
        member __.SystemInformationID = RequiredColumn(reader, getOrdinal, reader.GetByte, "SystemInformationID")
        member __.``Database Version`` = RequiredColumn(reader, getOrdinal, reader.GetString, "Database Version")
        member __.VersionDate = RequiredColumn(reader, getOrdinal, reader.GetDateTime, "VersionDate")
        member __.ModifiedDate = RequiredColumn(reader, getOrdinal, reader.GetDateTime, "ModifiedDate")
        member __.Read() =
            { SystemInformationID = __.SystemInformationID.Read()
              ``Database Version`` = __.``Database Version``.Read()
              VersionDate = __.VersionDate.Read()
              ModifiedDate = __.ModifiedDate.Read() }

        member __.ReadIfNotNull(column: Column) =
            if column.IsNull() then None else Some(__.Read())

    type HydraReader(reader: System.Data.SqlClient.SqlDataReader) =
        let entities = System.Collections.Generic.Dictionary<string, string -> int>()
        let buildGetOrdinal entity= 
            if not (entities.ContainsKey(entity)) then 
                let dictionary = 
                    [0..reader.FieldCount-1] 
                    |> List.mapi (fun i fieldIdx -> reader.GetName(fieldIdx), i)
                    |> List.groupBy (fun (nm, i) -> nm) 
                    |> List.map (fun (_, items) -> List.tryItem(entities.Count) items |> Option.defaultWith (fun () -> List.last items))
                    |> dict
                let getOrdinal = fun idx -> dictionary.Item idx
                entities.Add(entity, getOrdinal)
                getOrdinal
            else
                entities.[entity]
        
        member __.ErrorLog = ErrorLogReader(reader, buildGetOrdinal "ErrorLog")
        member __.BuildVersion = BuildVersionReader(reader, buildGetOrdinal "BuildVersion")
        member private __.ReadByName(entity: string, isOption: bool) =
            match entity, isOption with
            | "ErrorLog", false -> __.ErrorLog.Read() :> obj
            | "ErrorLog", true -> __.ErrorLog.ReadIfNotNull() :> obj
            | _ -> failwith $"Invalid entity: {entity}"
            :?> _

        static member Read(reader: System.Data.SqlClient.SqlDataReader) = 
            let hydra = HydraReader(reader)
            
            let getNameIsOption (t: System.Type) = 
                if t.Name.StartsWith "FSharpOption"
                then t.GenericTypeArguments.[0].Name, true
                else t.Name, false
            
            let t = typeof<'T>
            if t.Name.StartsWith "Tuple" then
                let entityInfos = t.GenericTypeArguments |> Array.map getNameIsOption
                fun () ->
                    let values = entityInfos |> Array.map hydra.ReadByName
                    let tuple = Microsoft.FSharp.Reflection.FSharpValue.MakeTuple(values, t)
                    tuple :?> 'T
            else
                fun () -> 
                    t |> getNameIsOption |> hydra.ReadByName |> box :?> 'T
        

module SalesLT =
    [<CLIMutable>]
    type Address =
        { City: string
          StateProvince: string
          CountryRegion: string
          PostalCode: string
          rowguid: System.Guid
          ModifiedDate: System.DateTime
          AddressID: int
          AddressLine1: string
          AddressLine2: Option<string> }

    type AddressReader(reader: System.Data.SqlClient.SqlDataReader, getOrdinal) =
        member __.City = RequiredColumn(reader, getOrdinal, reader.GetString, "City")
        member __.StateProvince = RequiredColumn(reader, getOrdinal, reader.GetString, "StateProvince")
        member __.CountryRegion = RequiredColumn(reader, getOrdinal, reader.GetString, "CountryRegion")
        member __.PostalCode = RequiredColumn(reader, getOrdinal, reader.GetString, "PostalCode")
        member __.rowguid = RequiredColumn(reader, getOrdinal, reader.GetGuid, "rowguid")
        member __.ModifiedDate = RequiredColumn(reader, getOrdinal, reader.GetDateTime, "ModifiedDate")
        member __.AddressID = RequiredColumn(reader, getOrdinal, reader.GetInt32, "AddressID")
        member __.AddressLine1 = RequiredColumn(reader, getOrdinal, reader.GetString, "AddressLine1")
        member __.AddressLine2 = OptionalColumn(reader, getOrdinal, reader.GetString, "AddressLine2")
        member __.Read() =
            { City = __.City.Read()
              StateProvince = __.StateProvince.Read()
              CountryRegion = __.CountryRegion.Read()
              PostalCode = __.PostalCode.Read()
              rowguid = __.rowguid.Read()
              ModifiedDate = __.ModifiedDate.Read()
              AddressID = __.AddressID.Read()
              AddressLine1 = __.AddressLine1.Read()
              AddressLine2 = __.AddressLine2.Read() }

        member __.ReadIfNotNull() =
            if __.AddressID.IsNull() then None else Some(__.Read())

    [<CLIMutable>]
    type Customer =
        { LastName: string
          PasswordHash: string
          PasswordSalt: string
          rowguid: System.Guid
          ModifiedDate: System.DateTime
          CustomerID: int
          NameStyle: bool
          FirstName: string
          MiddleName: Option<string>
          Title: Option<string>
          Suffix: Option<string>
          CompanyName: Option<string>
          SalesPerson: Option<string>
          EmailAddress: Option<string>
          Phone: Option<string> }

    type CustomerReader(reader: System.Data.SqlClient.SqlDataReader, getOrdinal) =
        member __.LastName = RequiredColumn(reader, getOrdinal, reader.GetString, "LastName")
        member __.PasswordHash = RequiredColumn(reader, getOrdinal, reader.GetString, "PasswordHash")
        member __.PasswordSalt = RequiredColumn(reader, getOrdinal, reader.GetString, "PasswordSalt")
        member __.rowguid = RequiredColumn(reader, getOrdinal, reader.GetGuid, "rowguid")
        member __.ModifiedDate = RequiredColumn(reader, getOrdinal, reader.GetDateTime, "ModifiedDate")
        member __.CustomerID = RequiredColumn(reader, getOrdinal, reader.GetInt32, "CustomerID")
        member __.NameStyle = RequiredColumn(reader, getOrdinal, reader.GetBoolean, "NameStyle")
        member __.FirstName = RequiredColumn(reader, getOrdinal, reader.GetString, "FirstName")
        member __.MiddleName = OptionalColumn(reader, getOrdinal, reader.GetString, "MiddleName")
        member __.Title = OptionalColumn(reader, getOrdinal, reader.GetString, "Title")
        member __.Suffix = OptionalColumn(reader, getOrdinal, reader.GetString, "Suffix")
        member __.CompanyName = OptionalColumn(reader, getOrdinal, reader.GetString, "CompanyName")
        member __.SalesPerson = OptionalColumn(reader, getOrdinal, reader.GetString, "SalesPerson")
        member __.EmailAddress = OptionalColumn(reader, getOrdinal, reader.GetString, "EmailAddress")
        member __.Phone = OptionalColumn(reader, getOrdinal, reader.GetString, "Phone")
        member __.Read() =
            { LastName = __.LastName.Read()
              PasswordHash = __.PasswordHash.Read()
              PasswordSalt = __.PasswordSalt.Read()
              rowguid = __.rowguid.Read()
              ModifiedDate = __.ModifiedDate.Read()
              CustomerID = __.CustomerID.Read()
              NameStyle = __.NameStyle.Read()
              FirstName = __.FirstName.Read()
              MiddleName = __.MiddleName.Read()
              Title = __.Title.Read()
              Suffix = __.Suffix.Read()
              CompanyName = __.CompanyName.Read()
              SalesPerson = __.SalesPerson.Read()
              EmailAddress = __.EmailAddress.Read()
              Phone = __.Phone.Read() }

        member __.ReadIfNotNull() =
            if __.CustomerID.IsNull() then None else Some(__.Read())

    [<CLIMutable>]
    type CustomerAddress =
        { CustomerID: int
          AddressID: int
          AddressType: string
          rowguid: System.Guid
          ModifiedDate: System.DateTime }

    type CustomerAddressReader(reader: System.Data.SqlClient.SqlDataReader, getOrdinal) =
        member __.CustomerID = RequiredColumn(reader, getOrdinal, reader.GetInt32, "CustomerID")
        member __.AddressID = RequiredColumn(reader, getOrdinal, reader.GetInt32, "AddressID")
        member __.AddressType = RequiredColumn(reader, getOrdinal, reader.GetString, "AddressType")
        member __.rowguid = RequiredColumn(reader, getOrdinal, reader.GetGuid, "rowguid")
        member __.ModifiedDate = RequiredColumn(reader, getOrdinal, reader.GetDateTime, "ModifiedDate")
        member __.Read() =
            { CustomerID = __.CustomerID.Read()
              AddressID = __.AddressID.Read()
              AddressType = __.AddressType.Read()
              rowguid = __.rowguid.Read()
              ModifiedDate = __.ModifiedDate.Read() }

        member __.ReadIfNotNull() =
            if __.CustomerID.IsNull() then None else Some(__.Read())

    [<CLIMutable>]
    type Product =
        { ProductID: int
          Name: string
          ProductNumber: string
          StandardCost: decimal
          ListPrice: decimal
          SellStartDate: System.DateTime
          rowguid: System.Guid
          ModifiedDate: System.DateTime
          SellEndDate: Option<System.DateTime>
          DiscontinuedDate: Option<System.DateTime>
          ThumbNailPhoto: Option<byte []>
          ThumbnailPhotoFileName: Option<string>
          Size: Option<string>
          Weight: Option<decimal>
          ProductCategoryID: Option<int>
          ProductModelID: Option<int>
          Color: Option<string> }

    type ProductReader(reader: System.Data.SqlClient.SqlDataReader, getOrdinal) =
        member __.ProductID = RequiredColumn(reader, getOrdinal, reader.GetInt32, "ProductID")
        member __.Name = RequiredColumn(reader, getOrdinal, reader.GetString, "Name")
        member __.ProductNumber = RequiredColumn(reader, getOrdinal, reader.GetString, "ProductNumber")
        member __.StandardCost = RequiredColumn(reader, getOrdinal, reader.GetDecimal, "StandardCost")
        member __.ListPrice = RequiredColumn(reader, getOrdinal, reader.GetDecimal, "ListPrice")
        member __.SellStartDate = RequiredColumn(reader, getOrdinal, reader.GetDateTime, "SellStartDate")
        member __.rowguid = RequiredColumn(reader, getOrdinal, reader.GetGuid, "rowguid")
        member __.ModifiedDate = RequiredColumn(reader, getOrdinal, reader.GetDateTime, "ModifiedDate")
        member __.SellEndDate = OptionalColumn(reader, getOrdinal, reader.GetDateTime, "SellEndDate")
        member __.DiscontinuedDate = OptionalColumn(reader, getOrdinal, reader.GetDateTime, "DiscontinuedDate")
        member __.ThumbNailPhoto = OptionalBinaryColumn(reader, getOrdinal, reader.GetValue, "ThumbNailPhoto")
        member __.ThumbnailPhotoFileName = OptionalColumn(reader, getOrdinal, reader.GetString, "ThumbnailPhotoFileName")
        member __.Size = OptionalColumn(reader, getOrdinal, reader.GetString, "Size")
        member __.Weight = OptionalColumn(reader, getOrdinal, reader.GetDecimal, "Weight")
        member __.ProductCategoryID = OptionalColumn(reader, getOrdinal, reader.GetInt32, "ProductCategoryID")
        member __.ProductModelID = OptionalColumn(reader, getOrdinal, reader.GetInt32, "ProductModelID")
        member __.Color = OptionalColumn(reader, getOrdinal, reader.GetString, "Color")
        member __.Read() =
            { ProductID = __.ProductID.Read()
              Name = __.Name.Read()
              ProductNumber = __.ProductNumber.Read()
              StandardCost = __.StandardCost.Read()
              ListPrice = __.ListPrice.Read()
              SellStartDate = __.SellStartDate.Read()
              rowguid = __.rowguid.Read()
              ModifiedDate = __.ModifiedDate.Read()
              SellEndDate = __.SellEndDate.Read()
              DiscontinuedDate = __.DiscontinuedDate.Read()
              ThumbNailPhoto = __.ThumbNailPhoto.Read()
              ThumbnailPhotoFileName = __.ThumbnailPhotoFileName.Read()
              Size = __.Size.Read()
              Weight = __.Weight.Read()
              ProductCategoryID = __.ProductCategoryID.Read()
              ProductModelID = __.ProductModelID.Read()
              Color = __.Color.Read() }

        member __.ReadIfNotNull() =
            if __.ProductID.IsNull() then None else Some(__.Read())

    [<CLIMutable>]
    type ProductCategory =
        { Name: string
          rowguid: System.Guid
          ModifiedDate: System.DateTime
          ProductCategoryID: int
          ParentProductCategoryID: Option<int> }

    type ProductCategoryReader(reader: System.Data.SqlClient.SqlDataReader, getOrdinal) =
        member __.Name = RequiredColumn(reader, getOrdinal, reader.GetString, "Name")
        member __.rowguid = RequiredColumn(reader, getOrdinal, reader.GetGuid, "rowguid")
        member __.ModifiedDate = RequiredColumn(reader, getOrdinal, reader.GetDateTime, "ModifiedDate")
        member __.ProductCategoryID = RequiredColumn(reader, getOrdinal, reader.GetInt32, "ProductCategoryID")
        member __.ParentProductCategoryID = OptionalColumn(reader, getOrdinal, reader.GetInt32, "ParentProductCategoryID")
        member __.Read() =
            { Name = __.Name.Read()
              rowguid = __.rowguid.Read()
              ModifiedDate = __.ModifiedDate.Read()
              ProductCategoryID = __.ProductCategoryID.Read()
              ParentProductCategoryID = __.ParentProductCategoryID.Read() }

        member __.ReadIfNotNull() =
            if __.ProductCategoryID.IsNull() then None else Some(__.Read())

    [<CLIMutable>]
    type ProductDescription =
        { ProductDescriptionID: int
          Description: string
          rowguid: System.Guid
          ModifiedDate: System.DateTime }

    type ProductDescriptionReader(reader: System.Data.SqlClient.SqlDataReader, getOrdinal) =
        member __.ProductDescriptionID = RequiredColumn(reader, getOrdinal, reader.GetInt32, "ProductDescriptionID")
        member __.Description = RequiredColumn(reader, getOrdinal, reader.GetString, "Description")
        member __.rowguid = RequiredColumn(reader, getOrdinal, reader.GetGuid, "rowguid")
        member __.ModifiedDate = RequiredColumn(reader, getOrdinal, reader.GetDateTime, "ModifiedDate")
        member __.Read() =
            { ProductDescriptionID = __.ProductDescriptionID.Read()
              Description = __.Description.Read()
              rowguid = __.rowguid.Read()
              ModifiedDate = __.ModifiedDate.Read() }

        member __.ReadIfNotNull() =
            if __.ProductDescriptionID.IsNull() then None else Some(__.Read())

    [<CLIMutable>]
    type ProductModel =
        { ProductModelID: int
          Name: string
          rowguid: System.Guid
          ModifiedDate: System.DateTime
          CatalogDescription: Option<System.Xml.Linq.XElement> }

    type ProductModelReader(reader: System.Data.SqlClient.SqlDataReader, getOrdinal) =
        member __.ProductModelID = RequiredColumn(reader, getOrdinal, reader.GetInt32, "ProductModelID")
        member __.Name = RequiredColumn(reader, getOrdinal, reader.GetString, "Name")
        member __.rowguid = RequiredColumn(reader, getOrdinal, reader.GetGuid, "rowguid")
        member __.ModifiedDate = RequiredColumn(reader, getOrdinal, reader.GetDateTime, "ModifiedDate")

    [<CLIMutable>]
    type ProductModelProductDescription =
        { ProductModelID: int
          ProductDescriptionID: int
          Culture: string
          rowguid: System.Guid
          ModifiedDate: System.DateTime }

    type ProductModelProductDescriptionReader(reader: System.Data.SqlClient.SqlDataReader, getOrdinal) =
        member __.ProductModelID = RequiredColumn(reader, getOrdinal, reader.GetInt32, "ProductModelID")
        member __.ProductDescriptionID = RequiredColumn(reader, getOrdinal, reader.GetInt32, "ProductDescriptionID")
        member __.Culture = RequiredColumn(reader, getOrdinal, reader.GetString, "Culture")
        member __.rowguid = RequiredColumn(reader, getOrdinal, reader.GetGuid, "rowguid")
        member __.ModifiedDate = RequiredColumn(reader, getOrdinal, reader.GetDateTime, "ModifiedDate")
        member __.Read() =
            { ProductModelID = __.ProductModelID.Read()
              ProductDescriptionID = __.ProductDescriptionID.Read()
              Culture = __.Culture.Read()
              rowguid = __.rowguid.Read()
              ModifiedDate = __.ModifiedDate.Read() }

        member __.ReadIfNotNull() =
            if __.ProductModelID.IsNull() then None else Some(__.Read())

    [<CLIMutable>]
    type SalesOrderDetail =
        { SalesOrderID: int
          SalesOrderDetailID: int
          OrderQty: int16
          ProductID: int
          UnitPrice: decimal
          UnitPriceDiscount: decimal
          LineTotal: decimal
          rowguid: System.Guid
          ModifiedDate: System.DateTime }

    type SalesOrderDetailReader(reader: System.Data.SqlClient.SqlDataReader, getOrdinal) =
        member __.SalesOrderID = RequiredColumn(reader, getOrdinal, reader.GetInt32, "SalesOrderID")
        member __.SalesOrderDetailID = RequiredColumn(reader, getOrdinal, reader.GetInt32, "SalesOrderDetailID")
        member __.OrderQty = RequiredColumn(reader, getOrdinal, reader.GetInt16, "OrderQty")
        member __.ProductID = RequiredColumn(reader, getOrdinal, reader.GetInt32, "ProductID")
        member __.UnitPrice = RequiredColumn(reader, getOrdinal, reader.GetDecimal, "UnitPrice")
        member __.UnitPriceDiscount = RequiredColumn(reader, getOrdinal, reader.GetDecimal, "UnitPriceDiscount")
        member __.LineTotal = RequiredColumn(reader, getOrdinal, reader.GetDecimal, "LineTotal")
        member __.rowguid = RequiredColumn(reader, getOrdinal, reader.GetGuid, "rowguid")
        member __.ModifiedDate = RequiredColumn(reader, getOrdinal, reader.GetDateTime, "ModifiedDate")
        member __.Read() =
            { SalesOrderID = __.SalesOrderID.Read()
              SalesOrderDetailID = __.SalesOrderDetailID.Read()
              OrderQty = __.OrderQty.Read()
              ProductID = __.ProductID.Read()
              UnitPrice = __.UnitPrice.Read()
              UnitPriceDiscount = __.UnitPriceDiscount.Read()
              LineTotal = __.LineTotal.Read()
              rowguid = __.rowguid.Read()
              ModifiedDate = __.ModifiedDate.Read() }

        member __.ReadIfNotNull() =
            if __.SalesOrderID.IsNull() then None else Some(__.Read())

    [<CLIMutable>]
    type SalesOrderHeader =
        { SalesOrderID: int
          RevisionNumber: byte
          OrderDate: System.DateTime
          DueDate: System.DateTime
          Status: byte
          OnlineOrderFlag: bool
          SalesOrderNumber: string
          CustomerID: int
          SubTotal: decimal
          TaxAmt: decimal
          Freight: decimal
          TotalDue: decimal
          rowguid: System.Guid
          ModifiedDate: System.DateTime
          ShipMethod: string
          CreditCardApprovalCode: Option<string>
          Comment: Option<string>
          ShipToAddressID: Option<int>
          BillToAddressID: Option<int>
          PurchaseOrderNumber: Option<string>
          AccountNumber: Option<string>
          ShipDate: Option<System.DateTime> }

    type SalesOrderHeaderReader(reader: System.Data.SqlClient.SqlDataReader, getOrdinal) =
        member __.SalesOrderID = RequiredColumn(reader, getOrdinal, reader.GetInt32, "SalesOrderID")
        member __.RevisionNumber = RequiredColumn(reader, getOrdinal, reader.GetByte, "RevisionNumber")
        member __.OrderDate = RequiredColumn(reader, getOrdinal, reader.GetDateTime, "OrderDate")
        member __.DueDate = RequiredColumn(reader, getOrdinal, reader.GetDateTime, "DueDate")
        member __.Status = RequiredColumn(reader, getOrdinal, reader.GetByte, "Status")
        member __.OnlineOrderFlag = RequiredColumn(reader, getOrdinal, reader.GetBoolean, "OnlineOrderFlag")
        member __.SalesOrderNumber = RequiredColumn(reader, getOrdinal, reader.GetString, "SalesOrderNumber")
        member __.CustomerID = RequiredColumn(reader, getOrdinal, reader.GetInt32, "CustomerID")
        member __.SubTotal = RequiredColumn(reader, getOrdinal, reader.GetDecimal, "SubTotal")
        member __.TaxAmt = RequiredColumn(reader, getOrdinal, reader.GetDecimal, "TaxAmt")
        member __.Freight = RequiredColumn(reader, getOrdinal, reader.GetDecimal, "Freight")
        member __.TotalDue = RequiredColumn(reader, getOrdinal, reader.GetDecimal, "TotalDue")
        member __.rowguid = RequiredColumn(reader, getOrdinal, reader.GetGuid, "rowguid")
        member __.ModifiedDate = RequiredColumn(reader, getOrdinal, reader.GetDateTime, "ModifiedDate")
        member __.ShipMethod = RequiredColumn(reader, getOrdinal, reader.GetString, "ShipMethod")
        member __.CreditCardApprovalCode = OptionalColumn(reader, getOrdinal, reader.GetString, "CreditCardApprovalCode")
        member __.Comment = OptionalColumn(reader, getOrdinal, reader.GetString, "Comment")
        member __.ShipToAddressID = OptionalColumn(reader, getOrdinal, reader.GetInt32, "ShipToAddressID")
        member __.BillToAddressID = OptionalColumn(reader, getOrdinal, reader.GetInt32, "BillToAddressID")
        member __.PurchaseOrderNumber = OptionalColumn(reader, getOrdinal, reader.GetString, "PurchaseOrderNumber")
        member __.AccountNumber = OptionalColumn(reader, getOrdinal, reader.GetString, "AccountNumber")
        member __.ShipDate = OptionalColumn(reader, getOrdinal, reader.GetDateTime, "ShipDate")
        member __.Read() =
            { SalesOrderID = __.SalesOrderID.Read()
              RevisionNumber = __.RevisionNumber.Read()
              OrderDate = __.OrderDate.Read()
              DueDate = __.DueDate.Read()
              Status = __.Status.Read()
              OnlineOrderFlag = __.OnlineOrderFlag.Read()
              SalesOrderNumber = __.SalesOrderNumber.Read()
              CustomerID = __.CustomerID.Read()
              SubTotal = __.SubTotal.Read()
              TaxAmt = __.TaxAmt.Read()
              Freight = __.Freight.Read()
              TotalDue = __.TotalDue.Read()
              rowguid = __.rowguid.Read()
              ModifiedDate = __.ModifiedDate.Read()
              ShipMethod = __.ShipMethod.Read()
              CreditCardApprovalCode = __.CreditCardApprovalCode.Read()
              Comment = __.Comment.Read()
              ShipToAddressID = __.ShipToAddressID.Read()
              BillToAddressID = __.BillToAddressID.Read()
              PurchaseOrderNumber = __.PurchaseOrderNumber.Read()
              AccountNumber = __.AccountNumber.Read()
              ShipDate = __.ShipDate.Read() }

        member __.ReadIfNotNull() =
            if __.SalesOrderID.IsNull() then None else Some(__.Read())

    [<CLIMutable>]
    type vProductAndDescription =
        { ProductID: int
          Name: string
          ProductModel: string
          Culture: string
          Description: string }

    type vProductAndDescriptionReader(reader: System.Data.SqlClient.SqlDataReader, getOrdinal) =
        member __.ProductID = RequiredColumn(reader, getOrdinal, reader.GetInt32, "ProductID")
        member __.Name = RequiredColumn(reader, getOrdinal, reader.GetString, "Name")
        member __.ProductModel = RequiredColumn(reader, getOrdinal, reader.GetString, "ProductModel")
        member __.Culture = RequiredColumn(reader, getOrdinal, reader.GetString, "Culture")
        member __.Description = RequiredColumn(reader, getOrdinal, reader.GetString, "Description")
        member __.Read() =
            { ProductID = __.ProductID.Read()
              Name = __.Name.Read()
              ProductModel = __.ProductModel.Read()
              Culture = __.Culture.Read()
              Description = __.Description.Read() }

        member __.ReadIfNotNull(column: Column) =
            if column.IsNull() then None else Some(__.Read())

    [<CLIMutable>]
    type vProductModelCatalogDescription =
        { ProductModelID: int
          Name: string
          rowguid: System.Guid
          ModifiedDate: System.DateTime
          Summary: Option<string>
          Manufacturer: Option<string>
          Copyright: Option<string>
          ProductURL: Option<string>
          WarrantyPeriod: Option<string>
          WarrantyDescription: Option<string>
          NoOfYears: Option<string>
          MaintenanceDescription: Option<string>
          Wheel: Option<string>
          Saddle: Option<string>
          Pedal: Option<string>
          BikeFrame: Option<string>
          Crankset: Option<string>
          PictureAngle: Option<string>
          PictureSize: Option<string>
          ProductPhotoID: Option<string>
          Material: Option<string>
          Color: Option<string>
          ProductLine: Option<string>
          Style: Option<string>
          RiderExperience: Option<string> }

    type vProductModelCatalogDescriptionReader(reader: System.Data.SqlClient.SqlDataReader, getOrdinal) =
        member __.ProductModelID = RequiredColumn(reader, getOrdinal, reader.GetInt32, "ProductModelID")
        member __.Name = RequiredColumn(reader, getOrdinal, reader.GetString, "Name")
        member __.rowguid = RequiredColumn(reader, getOrdinal, reader.GetGuid, "rowguid")
        member __.ModifiedDate = RequiredColumn(reader, getOrdinal, reader.GetDateTime, "ModifiedDate")
        member __.Summary = OptionalColumn(reader, getOrdinal, reader.GetString, "Summary")
        member __.Manufacturer = OptionalColumn(reader, getOrdinal, reader.GetString, "Manufacturer")
        member __.Copyright = OptionalColumn(reader, getOrdinal, reader.GetString, "Copyright")
        member __.ProductURL = OptionalColumn(reader, getOrdinal, reader.GetString, "ProductURL")
        member __.WarrantyPeriod = OptionalColumn(reader, getOrdinal, reader.GetString, "WarrantyPeriod")
        member __.WarrantyDescription = OptionalColumn(reader, getOrdinal, reader.GetString, "WarrantyDescription")
        member __.NoOfYears = OptionalColumn(reader, getOrdinal, reader.GetString, "NoOfYears")
        member __.MaintenanceDescription = OptionalColumn(reader, getOrdinal, reader.GetString, "MaintenanceDescription")
        member __.Wheel = OptionalColumn(reader, getOrdinal, reader.GetString, "Wheel")
        member __.Saddle = OptionalColumn(reader, getOrdinal, reader.GetString, "Saddle")
        member __.Pedal = OptionalColumn(reader, getOrdinal, reader.GetString, "Pedal")
        member __.BikeFrame = OptionalColumn(reader, getOrdinal, reader.GetString, "BikeFrame")
        member __.Crankset = OptionalColumn(reader, getOrdinal, reader.GetString, "Crankset")
        member __.PictureAngle = OptionalColumn(reader, getOrdinal, reader.GetString, "PictureAngle")
        member __.PictureSize = OptionalColumn(reader, getOrdinal, reader.GetString, "PictureSize")
        member __.ProductPhotoID = OptionalColumn(reader, getOrdinal, reader.GetString, "ProductPhotoID")
        member __.Material = OptionalColumn(reader, getOrdinal, reader.GetString, "Material")
        member __.Color = OptionalColumn(reader, getOrdinal, reader.GetString, "Color")
        member __.ProductLine = OptionalColumn(reader, getOrdinal, reader.GetString, "ProductLine")
        member __.Style = OptionalColumn(reader, getOrdinal, reader.GetString, "Style")
        member __.RiderExperience = OptionalColumn(reader, getOrdinal, reader.GetString, "RiderExperience")
        member __.Read() =
            { ProductModelID = __.ProductModelID.Read()
              Name = __.Name.Read()
              rowguid = __.rowguid.Read()
              ModifiedDate = __.ModifiedDate.Read()
              Summary = __.Summary.Read()
              Manufacturer = __.Manufacturer.Read()
              Copyright = __.Copyright.Read()
              ProductURL = __.ProductURL.Read()
              WarrantyPeriod = __.WarrantyPeriod.Read()
              WarrantyDescription = __.WarrantyDescription.Read()
              NoOfYears = __.NoOfYears.Read()
              MaintenanceDescription = __.MaintenanceDescription.Read()
              Wheel = __.Wheel.Read()
              Saddle = __.Saddle.Read()
              Pedal = __.Pedal.Read()
              BikeFrame = __.BikeFrame.Read()
              Crankset = __.Crankset.Read()
              PictureAngle = __.PictureAngle.Read()
              PictureSize = __.PictureSize.Read()
              ProductPhotoID = __.ProductPhotoID.Read()
              Material = __.Material.Read()
              Color = __.Color.Read()
              ProductLine = __.ProductLine.Read()
              Style = __.Style.Read()
              RiderExperience = __.RiderExperience.Read() }

        member __.ReadIfNotNull(column: Column) =
            if column.IsNull() then None else Some(__.Read())

    [<CLIMutable>]
    type vGetAllCategories =
        { ParentProductCategoryName: string
          ProductCategoryName: Option<string>
          ProductCategoryID: Option<int> }

    type vGetAllCategoriesReader(reader: System.Data.SqlClient.SqlDataReader, getOrdinal) =
        member __.ParentProductCategoryName = RequiredColumn(reader, getOrdinal, reader.GetString, "ParentProductCategoryName")
        member __.ProductCategoryName = OptionalColumn(reader, getOrdinal, reader.GetString, "ProductCategoryName")
        member __.ProductCategoryID = OptionalColumn(reader, getOrdinal, reader.GetInt32, "ProductCategoryID")
        member __.Read() =
            { ParentProductCategoryName = __.ParentProductCategoryName.Read()
              ProductCategoryName = __.ProductCategoryName.Read()
              ProductCategoryID = __.ProductCategoryID.Read() }

        member __.ReadIfNotNull(column: Column) =
            if column.IsNull() then None else Some(__.Read())

    type HydraReader(reader: System.Data.SqlClient.SqlDataReader) =
        let mutable accFieldCount = 0
        let buildGetOrdinal fieldCount =
            let dictionary = 
                [0..reader.FieldCount-1] 
                |> List.map (fun i -> reader.GetName(i), i)
                |> List.sortBy snd
                |> List.skip accFieldCount
                |> List.take fieldCount
                |> dict
            accFieldCount <- accFieldCount + fieldCount
            fun col -> dictionary.Item col

        let addressReader = lazy AddressReader(reader, buildGetOrdinal 9)
        let customerReader = lazy CustomerReader(reader, buildGetOrdinal 15)
        let customerAddressReader = lazy CustomerAddressReader(reader, buildGetOrdinal 5)
        let productReader = lazy ProductReader(reader, buildGetOrdinal 17)
        let productCategoryReader = lazy ProductCategoryReader(reader, buildGetOrdinal 5)

        member __.Address = addressReader.Value
        member __.Customer = customerReader.Value
        member __.CustomerAddress = customerAddressReader.Value
        member __.Product = productReader.Value
        member __.ProductCategory = productCategoryReader.Value
        member __.ProductDescription = ProductDescriptionReader(reader, buildGetOrdinal 1)
        member __.ProductModel = ProductModelReader(reader, buildGetOrdinal 1)
        member __.ProductModelProductDescription = ProductModelProductDescriptionReader(reader, buildGetOrdinal 1)
        member __.SalesOrderDetail = SalesOrderDetailReader(reader, buildGetOrdinal 1)
        member __.SalesOrderHeader = SalesOrderHeaderReader(reader, buildGetOrdinal 1)
        member __.vProductAndDescription = vProductAndDescriptionReader (reader, buildGetOrdinal 1)
        member __.vProductModelCatalogDescription = vProductModelCatalogDescriptionReader (reader, buildGetOrdinal 1)
        member __.vGetAllCategories = vGetAllCategoriesReader (reader, buildGetOrdinal 1)

        member private __.AccFieldCount with get () = accFieldCount and set (value) = accFieldCount <- value
        member private __.GetReaderByName(entity: string, isOption: bool) =
            match entity, isOption with
            | "Address", false -> __.Address.Read >> box
            | "Address", true -> __.Address.ReadIfNotNull >> box
            | "Customer", false -> __.Customer.Read >> box
            | "Customer", true -> __.Customer.ReadIfNotNull >> box
            | "CustomerAddress", false -> __.CustomerAddress.Read >> box
            | "CustomerAddress", true -> __.CustomerAddress.ReadIfNotNull >> box
            | "Product", false -> __.Product.Read >> box
            | "Product", true -> __.Product.ReadIfNotNull >> box
            | "ProductCategory", false -> __.ProductCategory.Read >> box
            | "ProductCategory", true -> __.ProductCategory.ReadIfNotNull >> box
            | "ProductDescription", false -> __.ProductDescription.Read >> box
            | "ProductDescription", true -> __.ProductDescription.ReadIfNotNull >> box
            | "ProductModelProductDescription", false -> __.ProductModelProductDescription.Read >> box
            | "ProductModelProductDescription", true -> __.ProductModelProductDescription.ReadIfNotNull >> box
            | "SalesOrderDetail", false -> __.SalesOrderDetail.Read >> box
            | "SalesOrderDetail", true -> __.SalesOrderDetail.ReadIfNotNull >> box
            | "SalesOrderHeader", false -> __.SalesOrderHeader.Read >> box
            | "SalesOrderHeader", true -> __.SalesOrderHeader.ReadIfNotNull >> box
            | _ -> failwith $"Invalid entity: {entity}"

        static member Read(reader: System.Data.SqlClient.SqlDataReader) = 
            let hydra = HydraReader(reader)
            
            let isPrimitive (t: System.Type) = t.IsPrimitive || t = typedefof<string>
            let getOrdinalAndIncrement() = 
                let ordinal = hydra.AccFieldCount
                hydra.AccFieldCount <- hydra.AccFieldCount + 1
                ordinal

            let getValue (ordinal) () = reader.GetValue(ordinal)
            
            let tryGetValue (ordinal) () =
                reader.GetValue(ordinal)
                |> Option.ofObj
                |> Option.bind (function :? System.DBNull -> None | o -> Some o)
                |> box 

            let buildEntityReadFn (t: System.Type) = 
                let tp, isOpt = 
                    if t.IsGenericType && t.GetGenericTypeDefinition() = typedefof<Option<_>> 
                    then t.GenericTypeArguments.[0], true
                    else t, false
                let isPrim = isPrimitive tp
                match isPrim, isOpt with
                | true, false -> getValue (getOrdinalAndIncrement())
                | true, true -> tryGetValue (getOrdinalAndIncrement())
                | _ -> hydra.GetReaderByName(tp.Name, isOpt)

            // Return a fn that will hydrate 'T (which may be a tuple or a single primitive)
            // This fn will be called once per each record returned by the data reader.
            let t = typeof<'T>
            if t.Name.StartsWith "Tuple" then
                let readEntityFns = t.GenericTypeArguments |> Array.map buildEntityReadFn
                fun () ->
                    let entities = readEntityFns |> Array.map (fun read -> read())
                    let tuple = Microsoft.FSharp.Reflection.FSharpValue.MakeTuple(entities, t)
                    tuple :?> 'T
            else
                let readEntityFn = t |> buildEntityReadFn
                fun () -> 
                    readEntityFn() :?> 'T
      