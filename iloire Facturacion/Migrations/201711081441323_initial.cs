namespace iloire_Facturacion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Data.Entity;
    using System.Collections.Generic;
    using System.Data.Entity.Validation;

    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                {
                    CustomerID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 4000),
                    CompanyNumber = c.String(nullable: false, maxLength: 4000),
                    Address = c.String(nullable: false, maxLength: 4000),
                    CP = c.String(nullable: false, maxLength: 4000),
                    City = c.String(nullable: false, maxLength: 4000),
                    ContactPerson = c.String(nullable: false, maxLength: 4000),
                    Phone1 = c.String(nullable: false, maxLength: 4000),
                    Phone2 = c.String(maxLength: 4000),
                    Fax = c.String(maxLength: 4000),
                    Email = c.String(nullable: false, maxLength: 4000),
                    Notes = c.String(maxLength: 4000),
                })
                .PrimaryKey(t => t.CustomerID);

            CreateTable(
                "dbo.Invoices",
                c => new
                {
                    InvoiceID = c.Int(nullable: false, identity: true),
                    InvoiceNumber = c.Int(nullable: false),
                    CustomerID = c.Int(nullable: false),
                    Name = c.String(maxLength: 4000),
                    Notes = c.String(nullable: false, maxLength: 4000),
                    ProposalDetails = c.String(maxLength: 4000),
                    TimeStamp = c.DateTime(nullable: false),
                    DueDate = c.DateTime(nullable: false),
                    AdvancePaymentTax = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Paid = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.InvoiceID)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .Index(t => t.CustomerID);

            CreateTable(
                "dbo.InvoiceDetails",
                c => new
                {
                    InvoiceDetailsID = c.Int(nullable: false, identity: true),
                    InvoiceID = c.Int(nullable: false),
                    Article = c.String(nullable: false, maxLength: 4000),
                    Qty = c.Int(nullable: false),
                    Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    VAT = c.Decimal(nullable: false, precision: 18, scale: 2),
                    TimeStamp = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.InvoiceDetailsID)
                .ForeignKey("dbo.Invoices", t => t.InvoiceID, cascadeDelete: true)
                .Index(t => t.InvoiceID);

            CreateTable(
                "dbo.Providers",
                c => new
                {
                    ProviderID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 4000),
                    CompanyNumber = c.String(maxLength: 4000),
                    Address = c.String(maxLength: 4000),
                    CP = c.String(maxLength: 4000),
                    City = c.String(maxLength: 4000),
                    Phone1 = c.String(maxLength: 4000),
                    Phone2 = c.String(maxLength: 4000),
                    Fax = c.String(maxLength: 4000),
                    Email = c.String(maxLength: 4000),
                })
                .PrimaryKey(t => t.ProviderID);

            CreateTable(
                "dbo.Purchases",
                c => new
                {
                    PurchaseID = c.Int(nullable: false, identity: true),
                    Article = c.String(nullable: false, maxLength: 4000),
                    Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    VAT = c.Decimal(nullable: false, precision: 18, scale: 2),
                    ProviderID = c.Int(nullable: false),
                    Notes = c.String(maxLength: 4000),
                    TimeStamp = c.DateTime(nullable: false),
                    PurchaseTypeID = c.Int(nullable: false),
                    AdvancePaymentTax = c.Decimal(nullable: false, precision: 18, scale: 2),
                })
                .PrimaryKey(t => t.PurchaseID)
                .ForeignKey("dbo.Providers", t => t.ProviderID, cascadeDelete: true)
                .ForeignKey("dbo.PurchaseTypes", t => t.PurchaseTypeID, cascadeDelete: true)
                .Index(t => t.ProviderID)
                .Index(t => t.PurchaseTypeID);

            CreateTable(
                "dbo.PurchaseTypes",
                c => new
                {
                    PurchaseTypeID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 4000),
                    Descr = c.String(maxLength: 4000),
                })
                .PrimaryKey(t => t.PurchaseTypeID);

            CreateTable(
                "dbo.Users",
                c => new
                {
                    UserID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 4000),
                    Login = c.String(nullable: false, maxLength: 4000),
                    Password = c.String(nullable: false, maxLength: 4000),
                    Enabled = c.Boolean(nullable: false),
                    Email = c.String(nullable: false, maxLength: 4000),
                })
                .PrimaryKey(t => t.UserID);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Purchases", "PurchaseTypeID", "dbo.PurchaseTypes");
            DropForeignKey("dbo.Purchases", "ProviderID", "dbo.Providers");
            DropForeignKey("dbo.InvoiceDetails", "InvoiceID", "dbo.Invoices");
            DropForeignKey("dbo.Invoices", "CustomerID", "dbo.Customers");
            DropIndex("dbo.Purchases", new[] { "PurchaseTypeID" });
            DropIndex("dbo.Purchases", new[] { "ProviderID" });
            DropIndex("dbo.InvoiceDetails", new[] { "InvoiceID" });
            DropIndex("dbo.Invoices", new[] { "CustomerID" });
            DropTable("dbo.Users");
            DropTable("dbo.PurchaseTypes");
            DropTable("dbo.Purchases");
            DropTable("dbo.Providers");
            DropTable("dbo.InvoiceDetails");
            DropTable("dbo.Invoices");
            DropTable("dbo.Customers");
        }
        
    }
}
