// This code was generated by `SqlHydra.Sqlite` -- v0.510.0.0.
namespace Sqlite.AdventureWorks

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
        
module main =
    [<CLIMutable>]
    type Address =
        { AddressID: int64
          AddressLine1: string
          AddressLine2: Option<string>
          City: string
          StateProvince: string
          CountryRegion: string
          PostalCode: string
          rowguid: string
          ModifiedDate: System.DateTime }

    type AddressReader(reader: System.Data.IDataReader, getOrdinal) =
        member __.AddressID = RequiredColumn(reader, getOrdinal, reader.GetInt64, "AddressID")
        member __.AddressLine1 = RequiredColumn(reader, getOrdinal, reader.GetString, "AddressLine1")
        member __.AddressLine2 = OptionalColumn(reader, getOrdinal, reader.GetString, "AddressLine2")
        member __.City = RequiredColumn(reader, getOrdinal, reader.GetString, "City")
        member __.StateProvince = RequiredColumn(reader, getOrdinal, reader.GetString, "StateProvince")
        member __.CountryRegion = RequiredColumn(reader, getOrdinal, reader.GetString, "CountryRegion")
        member __.PostalCode = RequiredColumn(reader, getOrdinal, reader.GetString, "PostalCode")
        member __.rowguid = RequiredColumn(reader, getOrdinal, reader.GetString, "rowguid")
        member __.ModifiedDate = RequiredColumn(reader, getOrdinal, reader.GetDateTime, "ModifiedDate")
        member __.Read() =
            { AddressID = __.AddressID.Read()
              AddressLine1 = __.AddressLine1.Read()
              AddressLine2 = __.AddressLine2.Read()
              City = __.City.Read()
              StateProvince = __.StateProvince.Read()
              CountryRegion = __.CountryRegion.Read()
              PostalCode = __.PostalCode.Read()
              rowguid = __.rowguid.Read()
              ModifiedDate = __.ModifiedDate.Read() }

        member __.ReadIfNotNull() =
            if __.AddressID.IsNull() then None else Some(__.Read())

    [<CLIMutable>]
    type BuildVersion =
        { SystemInformationID: int64
          ``Database Version``: string
          VersionDate: System.DateTime
          ModifiedDate: System.DateTime }

    type BuildVersionReader(reader: System.Data.IDataReader, getOrdinal) =
        member __.SystemInformationID = RequiredColumn(reader, getOrdinal, reader.GetInt64, "SystemInformationID")
        member __.``Database Version`` = RequiredColumn(reader, getOrdinal, reader.GetString, "Database Version")
        member __.VersionDate = RequiredColumn(reader, getOrdinal, reader.GetDateTime, "VersionDate")
        member __.ModifiedDate = RequiredColumn(reader, getOrdinal, reader.GetDateTime, "ModifiedDate")
        member __.Read() =
            { SystemInformationID = __.SystemInformationID.Read()
              ``Database Version`` = __.``Database Version``.Read()
              VersionDate = __.VersionDate.Read()
              ModifiedDate = __.ModifiedDate.Read() }

        member __.ReadIfNotNull() =
            if __.SystemInformationID.IsNull() then None else Some(__.Read())

    [<CLIMutable>]
    type Customer =
        { CustomerID: int64
          NameStyle: int64
          Title: Option<string>
          FirstName: string
          MiddleName: Option<string>
          LastName: string
          Suffix: Option<string>
          CompanyName: Option<string>
          SalesPerson: Option<string>
          EmailAddress: Option<string>
          Phone: Option<string>
          PasswordHash: string
          PasswordSalt: string
          rowguid: string
          ModifiedDate: System.DateTime }

    type CustomerReader(reader: System.Data.IDataReader, getOrdinal) =
        member __.CustomerID = RequiredColumn(reader, getOrdinal, reader.GetInt64, "CustomerID")
        member __.NameStyle = RequiredColumn(reader, getOrdinal, reader.GetInt64, "NameStyle")
        member __.Title = OptionalColumn(reader, getOrdinal, reader.GetString, "Title")
        member __.FirstName = RequiredColumn(reader, getOrdinal, reader.GetString, "FirstName")
        member __.MiddleName = OptionalColumn(reader, getOrdinal, reader.GetString, "MiddleName")
        member __.LastName = RequiredColumn(reader, getOrdinal, reader.GetString, "LastName")
        member __.Suffix = OptionalColumn(reader, getOrdinal, reader.GetString, "Suffix")
        member __.CompanyName = OptionalColumn(reader, getOrdinal, reader.GetString, "CompanyName")
        member __.SalesPerson = OptionalColumn(reader, getOrdinal, reader.GetString, "SalesPerson")
        member __.EmailAddress = OptionalColumn(reader, getOrdinal, reader.GetString, "EmailAddress")
        member __.Phone = OptionalColumn(reader, getOrdinal, reader.GetString, "Phone")
        member __.PasswordHash = RequiredColumn(reader, getOrdinal, reader.GetString, "PasswordHash")
        member __.PasswordSalt = RequiredColumn(reader, getOrdinal, reader.GetString, "PasswordSalt")
        member __.rowguid = RequiredColumn(reader, getOrdinal, reader.GetString, "rowguid")
        member __.ModifiedDate = RequiredColumn(reader, getOrdinal, reader.GetDateTime, "ModifiedDate")
        member __.Read() =
            { CustomerID = __.CustomerID.Read()
              NameStyle = __.NameStyle.Read()
              Title = __.Title.Read()
              FirstName = __.FirstName.Read()
              MiddleName = __.MiddleName.Read()
              LastName = __.LastName.Read()
              Suffix = __.Suffix.Read()
              CompanyName = __.CompanyName.Read()
              SalesPerson = __.SalesPerson.Read()
              EmailAddress = __.EmailAddress.Read()
              Phone = __.Phone.Read()
              PasswordHash = __.PasswordHash.Read()
              PasswordSalt = __.PasswordSalt.Read()
              rowguid = __.rowguid.Read()
              ModifiedDate = __.ModifiedDate.Read() }

        member __.ReadIfNotNull() =
            if __.CustomerID.IsNull() then None else Some(__.Read())

    [<CLIMutable>]
    type CustomerAddress =
        { CustomerID: int64
          AddressID: int64
          AddressType: string
          rowguid: string
          ModifiedDate: System.DateTime }

    type CustomerAddressReader(reader: System.Data.IDataReader, getOrdinal) =
        member __.CustomerID = RequiredColumn(reader, getOrdinal, reader.GetInt64, "CustomerID")
        member __.AddressID = RequiredColumn(reader, getOrdinal, reader.GetInt64, "AddressID")
        member __.AddressType = RequiredColumn(reader, getOrdinal, reader.GetString, "AddressType")
        member __.rowguid = RequiredColumn(reader, getOrdinal, reader.GetString, "rowguid")
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
    type ErrorLog =
        { ErrorLogID: int64
          ErrorTime: System.DateTime
          UserName: string
          ErrorNumber: int64
          ErrorSeverity: Option<int64>
          ErrorState: Option<int64>
          ErrorProcedure: Option<string>
          ErrorLine: Option<int64>
          ErrorMessage: string }

    type ErrorLogReader(reader: System.Data.IDataReader, getOrdinal) =
        member __.ErrorLogID = RequiredColumn(reader, getOrdinal, reader.GetInt64, "ErrorLogID")
        member __.ErrorTime = RequiredColumn(reader, getOrdinal, reader.GetDateTime, "ErrorTime")
        member __.UserName = RequiredColumn(reader, getOrdinal, reader.GetString, "UserName")
        member __.ErrorNumber = RequiredColumn(reader, getOrdinal, reader.GetInt64, "ErrorNumber")
        member __.ErrorSeverity = OptionalColumn(reader, getOrdinal, reader.GetInt64, "ErrorSeverity")
        member __.ErrorState = OptionalColumn(reader, getOrdinal, reader.GetInt64, "ErrorState")
        member __.ErrorProcedure = OptionalColumn(reader, getOrdinal, reader.GetString, "ErrorProcedure")
        member __.ErrorLine = OptionalColumn(reader, getOrdinal, reader.GetInt64, "ErrorLine")
        member __.ErrorMessage = RequiredColumn(reader, getOrdinal, reader.GetString, "ErrorMessage")
        member __.Read() =
            { ErrorLogID = __.ErrorLogID.Read()
              ErrorTime = __.ErrorTime.Read()
              UserName = __.UserName.Read()
              ErrorNumber = __.ErrorNumber.Read()
              ErrorSeverity = __.ErrorSeverity.Read()
              ErrorState = __.ErrorState.Read()
              ErrorProcedure = __.ErrorProcedure.Read()
              ErrorLine = __.ErrorLine.Read()
              ErrorMessage = __.ErrorMessage.Read() }

        member __.ReadIfNotNull() =
            if __.ErrorLogID.IsNull() then None else Some(__.Read())

    [<CLIMutable>]
    type Product =
        { ProductID: int64
          Name: string
          ProductNumber: string
          Color: Option<string>
          StandardCost: int64
          ListPrice: int64
          Size: Option<string>
          Weight: Option<int64>
          ProductCategoryID: Option<int64>
          ProductModelID: Option<int64>
          SellStartDate: System.DateTime
          SellEndDate: Option<System.DateTime>
          DiscontinuedDate: Option<System.DateTime>
          ThumbNailPhoto: Option<byte []>
          ThumbnailPhotoFileName: Option<string>
          rowguid: string
          ModifiedDate: System.DateTime }

    type ProductReader(reader: System.Data.IDataReader, getOrdinal) =
        member __.ProductID = RequiredColumn(reader, getOrdinal, reader.GetInt64, "ProductID")
        member __.Name = RequiredColumn(reader, getOrdinal, reader.GetString, "Name")
        member __.ProductNumber = RequiredColumn(reader, getOrdinal, reader.GetString, "ProductNumber")
        member __.Color = OptionalColumn(reader, getOrdinal, reader.GetString, "Color")
        member __.StandardCost = RequiredColumn(reader, getOrdinal, reader.GetInt64, "StandardCost")
        member __.ListPrice = RequiredColumn(reader, getOrdinal, reader.GetInt64, "ListPrice")
        member __.Size = OptionalColumn(reader, getOrdinal, reader.GetString, "Size")
        member __.Weight = OptionalColumn(reader, getOrdinal, reader.GetInt64, "Weight")
        member __.ProductCategoryID = OptionalColumn(reader, getOrdinal, reader.GetInt64, "ProductCategoryID")
        member __.ProductModelID = OptionalColumn(reader, getOrdinal, reader.GetInt64, "ProductModelID")
        member __.SellStartDate = RequiredColumn(reader, getOrdinal, reader.GetDateTime, "SellStartDate")
        member __.SellEndDate = OptionalColumn(reader, getOrdinal, reader.GetDateTime, "SellEndDate")
        member __.DiscontinuedDate = OptionalColumn(reader, getOrdinal, reader.GetDateTime, "DiscontinuedDate")
        member __.ThumbNailPhoto = OptionalBinaryColumn(reader, getOrdinal, reader.GetValue, "ThumbNailPhoto")
        member __.ThumbnailPhotoFileName = OptionalColumn(reader, getOrdinal, reader.GetString, "ThumbnailPhotoFileName")
        member __.rowguid = RequiredColumn(reader, getOrdinal, reader.GetString, "rowguid")
        member __.ModifiedDate = RequiredColumn(reader, getOrdinal, reader.GetDateTime, "ModifiedDate")
        member __.Read() =
            { ProductID = __.ProductID.Read()
              Name = __.Name.Read()
              ProductNumber = __.ProductNumber.Read()
              Color = __.Color.Read()
              StandardCost = __.StandardCost.Read()
              ListPrice = __.ListPrice.Read()
              Size = __.Size.Read()
              Weight = __.Weight.Read()
              ProductCategoryID = __.ProductCategoryID.Read()
              ProductModelID = __.ProductModelID.Read()
              SellStartDate = __.SellStartDate.Read()
              SellEndDate = __.SellEndDate.Read()
              DiscontinuedDate = __.DiscontinuedDate.Read()
              ThumbNailPhoto = __.ThumbNailPhoto.Read()
              ThumbnailPhotoFileName = __.ThumbnailPhotoFileName.Read()
              rowguid = __.rowguid.Read()
              ModifiedDate = __.ModifiedDate.Read() }

        member __.ReadIfNotNull() =
            if __.ProductID.IsNull() then None else Some(__.Read())

    [<CLIMutable>]
    type ProductCategory =
        { ProductCategoryID: int64
          ParentProductCategoryID: Option<int64>
          Name: string
          rowguid: string
          ModifiedDate: System.DateTime }

    type ProductCategoryReader(reader: System.Data.IDataReader, getOrdinal) =
        member __.ProductCategoryID = RequiredColumn(reader, getOrdinal, reader.GetInt64, "ProductCategoryID")
        member __.ParentProductCategoryID = OptionalColumn(reader, getOrdinal, reader.GetInt64, "ParentProductCategoryID")
        member __.Name = RequiredColumn(reader, getOrdinal, reader.GetString, "Name")
        member __.rowguid = RequiredColumn(reader, getOrdinal, reader.GetString, "rowguid")
        member __.ModifiedDate = RequiredColumn(reader, getOrdinal, reader.GetDateTime, "ModifiedDate")
        member __.Read() =
            { ProductCategoryID = __.ProductCategoryID.Read()
              ParentProductCategoryID = __.ParentProductCategoryID.Read()
              Name = __.Name.Read()
              rowguid = __.rowguid.Read()
              ModifiedDate = __.ModifiedDate.Read() }

        member __.ReadIfNotNull() =
            if __.ProductCategoryID.IsNull() then None else Some(__.Read())

    [<CLIMutable>]
    type ProductDescription =
        { ProductDescriptionID: int64
          Description: string
          rowguid: string
          ModifiedDate: System.DateTime }

    type ProductDescriptionReader(reader: System.Data.IDataReader, getOrdinal) =
        member __.ProductDescriptionID = RequiredColumn(reader, getOrdinal, reader.GetInt64, "ProductDescriptionID")
        member __.Description = RequiredColumn(reader, getOrdinal, reader.GetString, "Description")
        member __.rowguid = RequiredColumn(reader, getOrdinal, reader.GetString, "rowguid")
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
        { ProductModelID: int64
          Name: string
          CatalogDescription: Option<string>
          rowguid: string
          ModifiedDate: System.DateTime }

    type ProductModelReader(reader: System.Data.IDataReader, getOrdinal) =
        member __.ProductModelID = RequiredColumn(reader, getOrdinal, reader.GetInt64, "ProductModelID")
        member __.Name = RequiredColumn(reader, getOrdinal, reader.GetString, "Name")
        member __.CatalogDescription = OptionalColumn(reader, getOrdinal, reader.GetString, "CatalogDescription")
        member __.rowguid = RequiredColumn(reader, getOrdinal, reader.GetString, "rowguid")
        member __.ModifiedDate = RequiredColumn(reader, getOrdinal, reader.GetDateTime, "ModifiedDate")
        member __.Read() =
            { ProductModelID = __.ProductModelID.Read()
              Name = __.Name.Read()
              CatalogDescription = __.CatalogDescription.Read()
              rowguid = __.rowguid.Read()
              ModifiedDate = __.ModifiedDate.Read() }

        member __.ReadIfNotNull() =
            if __.ProductModelID.IsNull() then None else Some(__.Read())

    [<CLIMutable>]
    type ProductModelProductDescription =
        { ProductModelID: int64
          ProductDescriptionID: int64
          Culture: string
          rowguid: string
          ModifiedDate: System.DateTime }

    type ProductModelProductDescriptionReader(reader: System.Data.IDataReader, getOrdinal) =
        member __.ProductModelID = RequiredColumn(reader, getOrdinal, reader.GetInt64, "ProductModelID")
        member __.ProductDescriptionID = RequiredColumn(reader, getOrdinal, reader.GetInt64, "ProductDescriptionID")
        member __.Culture = RequiredColumn(reader, getOrdinal, reader.GetString, "Culture")
        member __.rowguid = RequiredColumn(reader, getOrdinal, reader.GetString, "rowguid")
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
        { SalesOrderID: int64
          SalesOrderDetailID: int64
          OrderQty: int64
          ProductID: int64
          UnitPrice: int64
          UnitPriceDiscount: int64
          LineTotal: int64
          rowguid: string
          ModifiedDate: System.DateTime }

    type SalesOrderDetailReader(reader: System.Data.IDataReader, getOrdinal) =
        member __.SalesOrderID = RequiredColumn(reader, getOrdinal, reader.GetInt64, "SalesOrderID")
        member __.SalesOrderDetailID = RequiredColumn(reader, getOrdinal, reader.GetInt64, "SalesOrderDetailID")
        member __.OrderQty = RequiredColumn(reader, getOrdinal, reader.GetInt64, "OrderQty")
        member __.ProductID = RequiredColumn(reader, getOrdinal, reader.GetInt64, "ProductID")
        member __.UnitPrice = RequiredColumn(reader, getOrdinal, reader.GetInt64, "UnitPrice")
        member __.UnitPriceDiscount = RequiredColumn(reader, getOrdinal, reader.GetInt64, "UnitPriceDiscount")
        member __.LineTotal = RequiredColumn(reader, getOrdinal, reader.GetInt64, "LineTotal")
        member __.rowguid = RequiredColumn(reader, getOrdinal, reader.GetString, "rowguid")
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
        { SalesOrderID: int64
          RevisionNumber: int64
          OrderDate: System.DateTime
          DueDate: System.DateTime
          ShipDate: Option<System.DateTime>
          Status: int64
          OnlineOrderFlag: int64
          SalesOrderNumber: string
          PurchaseOrderNumber: Option<int64>
          AccountNumber: Option<string>
          CustomerID: int64
          ShipToAddressID: Option<int>
          BillToAddressID: Option<int>
          ShipMethod: string
          CreditCardApprovalCode: Option<string>
          SubTotal: int64
          TaxAmt: int64
          Freight: int64
          TotalDue: int64
          Comment: Option<string>
          rowguid: string
          ModifiedDate: System.DateTime }

    type SalesOrderHeaderReader(reader: System.Data.IDataReader, getOrdinal) =
        member __.SalesOrderID = RequiredColumn(reader, getOrdinal, reader.GetInt64, "SalesOrderID")
        member __.RevisionNumber = RequiredColumn(reader, getOrdinal, reader.GetInt64, "RevisionNumber")
        member __.OrderDate = RequiredColumn(reader, getOrdinal, reader.GetDateTime, "OrderDate")
        member __.DueDate = RequiredColumn(reader, getOrdinal, reader.GetDateTime, "DueDate")
        member __.ShipDate = OptionalColumn(reader, getOrdinal, reader.GetDateTime, "ShipDate")
        member __.Status = RequiredColumn(reader, getOrdinal, reader.GetInt64, "Status")
        member __.OnlineOrderFlag = RequiredColumn(reader, getOrdinal, reader.GetInt64, "OnlineOrderFlag")
        member __.SalesOrderNumber = RequiredColumn(reader, getOrdinal, reader.GetString, "SalesOrderNumber")
        member __.PurchaseOrderNumber = OptionalColumn(reader, getOrdinal, reader.GetInt64, "PurchaseOrderNumber")
        member __.AccountNumber = OptionalColumn(reader, getOrdinal, reader.GetString, "AccountNumber")
        member __.CustomerID = RequiredColumn(reader, getOrdinal, reader.GetInt64, "CustomerID")
        member __.ShipToAddressID = OptionalColumn(reader, getOrdinal, reader.GetInt32, "ShipToAddressID")
        member __.BillToAddressID = OptionalColumn(reader, getOrdinal, reader.GetInt32, "BillToAddressID")
        member __.ShipMethod = RequiredColumn(reader, getOrdinal, reader.GetString, "ShipMethod")
        member __.CreditCardApprovalCode = OptionalColumn(reader, getOrdinal, reader.GetString, "CreditCardApprovalCode")
        member __.SubTotal = RequiredColumn(reader, getOrdinal, reader.GetInt64, "SubTotal")
        member __.TaxAmt = RequiredColumn(reader, getOrdinal, reader.GetInt64, "TaxAmt")
        member __.Freight = RequiredColumn(reader, getOrdinal, reader.GetInt64, "Freight")
        member __.TotalDue = RequiredColumn(reader, getOrdinal, reader.GetInt64, "TotalDue")
        member __.Comment = OptionalColumn(reader, getOrdinal, reader.GetString, "Comment")
        member __.rowguid = RequiredColumn(reader, getOrdinal, reader.GetString, "rowguid")
        member __.ModifiedDate = RequiredColumn(reader, getOrdinal, reader.GetDateTime, "ModifiedDate")
        member __.Read() =
            { SalesOrderID = __.SalesOrderID.Read()
              RevisionNumber = __.RevisionNumber.Read()
              OrderDate = __.OrderDate.Read()
              DueDate = __.DueDate.Read()
              ShipDate = __.ShipDate.Read()
              Status = __.Status.Read()
              OnlineOrderFlag = __.OnlineOrderFlag.Read()
              SalesOrderNumber = __.SalesOrderNumber.Read()
              PurchaseOrderNumber = __.PurchaseOrderNumber.Read()
              AccountNumber = __.AccountNumber.Read()
              CustomerID = __.CustomerID.Read()
              ShipToAddressID = __.ShipToAddressID.Read()
              BillToAddressID = __.BillToAddressID.Read()
              ShipMethod = __.ShipMethod.Read()
              CreditCardApprovalCode = __.CreditCardApprovalCode.Read()
              SubTotal = __.SubTotal.Read()
              TaxAmt = __.TaxAmt.Read()
              Freight = __.Freight.Read()
              TotalDue = __.TotalDue.Read()
              Comment = __.Comment.Read()
              rowguid = __.rowguid.Read()
              ModifiedDate = __.ModifiedDate.Read() }

        member __.ReadIfNotNull() =
            if __.SalesOrderID.IsNull() then None else Some(__.Read())

type HydraReader(reader: System.Data.IDataReader) =
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
        
    let lazymainAddress = lazy (main.AddressReader(reader, buildGetOrdinal 9))
    let lazymainBuildVersion = lazy (main.BuildVersionReader(reader, buildGetOrdinal 4))
    let lazymainCustomer = lazy (main.CustomerReader(reader, buildGetOrdinal 15))
    let lazymainCustomerAddress = lazy (main.CustomerAddressReader(reader, buildGetOrdinal 5))
    let lazymainErrorLog = lazy (main.ErrorLogReader(reader, buildGetOrdinal 9))
    let lazymainProduct = lazy (main.ProductReader(reader, buildGetOrdinal 17))
    let lazymainProductCategory = lazy (main.ProductCategoryReader(reader, buildGetOrdinal 5))
    let lazymainProductDescription = lazy (main.ProductDescriptionReader(reader, buildGetOrdinal 4))
    let lazymainProductModel = lazy (main.ProductModelReader(reader, buildGetOrdinal 5))
    let lazymainProductModelProductDescription = lazy (main.ProductModelProductDescriptionReader(reader, buildGetOrdinal 5))
    let lazymainSalesOrderDetail = lazy (main.SalesOrderDetailReader(reader, buildGetOrdinal 9))
    let lazymainSalesOrderHeader = lazy (main.SalesOrderHeaderReader(reader, buildGetOrdinal 22))
    member __.``main.Address`` = lazymainAddress.Value
    member __.``main.BuildVersion`` = lazymainBuildVersion.Value
    member __.``main.Customer`` = lazymainCustomer.Value
    member __.``main.CustomerAddress`` = lazymainCustomerAddress.Value
    member __.``main.ErrorLog`` = lazymainErrorLog.Value
    member __.``main.Product`` = lazymainProduct.Value
    member __.``main.ProductCategory`` = lazymainProductCategory.Value
    member __.``main.ProductDescription`` = lazymainProductDescription.Value
    member __.``main.ProductModel`` = lazymainProductModel.Value
    member __.``main.ProductModelProductDescription`` = lazymainProductModelProductDescription.Value
    member __.``main.SalesOrderDetail`` = lazymainSalesOrderDetail.Value
    member __.``main.SalesOrderHeader`` = lazymainSalesOrderHeader.Value
    member private __.AccFieldCount with get () = accFieldCount and set (value) = accFieldCount <- value
    member private __.GetReaderByName(entity: string, isOption: bool) =
        match entity, isOption with
        | "main.Address", false -> __.``main.Address``.Read >> box
        | "main.Address", true -> __.``main.Address``.ReadIfNotNull >> box
        | "main.BuildVersion", false -> __.``main.BuildVersion``.Read >> box
        | "main.BuildVersion", true -> __.``main.BuildVersion``.ReadIfNotNull >> box
        | "main.Customer", false -> __.``main.Customer``.Read >> box
        | "main.Customer", true -> __.``main.Customer``.ReadIfNotNull >> box
        | "main.CustomerAddress", false -> __.``main.CustomerAddress``.Read >> box
        | "main.CustomerAddress", true -> __.``main.CustomerAddress``.ReadIfNotNull >> box
        | "main.ErrorLog", false -> __.``main.ErrorLog``.Read >> box
        | "main.ErrorLog", true -> __.``main.ErrorLog``.ReadIfNotNull >> box
        | "main.Product", false -> __.``main.Product``.Read >> box
        | "main.Product", true -> __.``main.Product``.ReadIfNotNull >> box
        | "main.ProductCategory", false -> __.``main.ProductCategory``.Read >> box
        | "main.ProductCategory", true -> __.``main.ProductCategory``.ReadIfNotNull >> box
        | "main.ProductDescription", false -> __.``main.ProductDescription``.Read >> box
        | "main.ProductDescription", true -> __.``main.ProductDescription``.ReadIfNotNull >> box
        | "main.ProductModel", false -> __.``main.ProductModel``.Read >> box
        | "main.ProductModel", true -> __.``main.ProductModel``.ReadIfNotNull >> box
        | "main.ProductModelProductDescription", false -> __.``main.ProductModelProductDescription``.Read >> box
        | "main.ProductModelProductDescription", true -> __.``main.ProductModelProductDescription``.ReadIfNotNull >> box
        | "main.SalesOrderDetail", false -> __.``main.SalesOrderDetail``.Read >> box
        | "main.SalesOrderDetail", true -> __.``main.SalesOrderDetail``.ReadIfNotNull >> box
        | "main.SalesOrderHeader", false -> __.``main.SalesOrderHeader``.Read >> box
        | "main.SalesOrderHeader", true -> __.``main.SalesOrderHeader``.ReadIfNotNull >> box
        | _ -> failwith $"Could not read type '{entity}' because no generated reader exists."

    static member private GetPrimitiveReader(t: System.Type, reader: System.Data.IDataReader, isOpt: bool) =
        let wrap get (ord: int) = 
                if isOpt 
                then (if reader.IsDBNull ord then None else get ord |> Some) |> box 
                else get ord |> box 
        
        if t = typedefof<int16> then Some(wrap reader.GetInt16)
        else if t = typedefof<int> then Some(wrap reader.GetInt32)
        else if t = typedefof<double> then Some(wrap reader.GetDouble)
        else if t = typedefof<System.Single> then Some(wrap reader.GetDouble)
        else if t = typedefof<decimal> then Some(wrap reader.GetDecimal)
        else if t = typedefof<bool> then Some(wrap reader.GetBoolean)
        else if t = typedefof<byte> then Some(wrap reader.GetByte)
        else if t = typedefof<int64> then Some(wrap reader.GetInt64)
        else if t = typedefof<byte []> then Some(wrap reader.GetValue)
        else if t = typedefof<string> then Some(wrap reader.GetString)
        else if t = typedefof<System.DateTime> then Some(wrap reader.GetDateTime)
        else if t = typedefof<System.Guid> then Some(wrap reader.GetGuid)
        else None

    static member Read(reader: System.Data.IDataReader) = 
            let hydra = HydraReader(reader)
            
            let getOrdinalAndIncrement() = 
                let ordinal = hydra.AccFieldCount
                hydra.AccFieldCount <- hydra.AccFieldCount + 1
                ordinal
            
            let buildEntityReadFn (t: System.Type) = 
                let t, isOpt = 
                    if t.IsGenericType && t.GetGenericTypeDefinition() = typedefof<Option<_>> 
                    then t.GenericTypeArguments.[0], true
                    else t, false
            
                match HydraReader.GetPrimitiveReader(t, reader, isOpt) with
                | Some primitiveReader -> 
                    let ord = getOrdinalAndIncrement()
                    fun () -> primitiveReader ord
                | None ->
                    let nameParts = t.FullName.Split([| '.'; '+' |])
                    let schemaAndType = nameParts |> Array.skip (nameParts.Length - 2) |> fun parts -> System.String.Join(".", parts)
                    hydra.GetReaderByName(schemaAndType, isOpt)
            
            // Return a fn that will hydrate 'T (which may be a tuple)
            // This fn will be called once per each record returned by the data reader.
            let t = typeof<'T>
            if FSharp.Reflection.FSharpType.IsTuple(t) then
                let readEntityFns = FSharp.Reflection.FSharpType.GetTupleElements(t) |> Array.map buildEntityReadFn
                fun () ->
                    let entities = readEntityFns |> Array.map (fun read -> read())
                    Microsoft.FSharp.Reflection.FSharpValue.MakeTuple(entities, t) :?> 'T
            else
                let readEntityFn = t |> buildEntityReadFn
                fun () -> 
                    readEntityFn() :?> 'T
        
