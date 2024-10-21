namespace KAMStoreNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        BrandID = c.Long(nullable: false, identity: true),
                        BrandName = c.String(),
                        CategoryID = c.Long(),
                    })
                .PrimaryKey(t => t.BrandID)
                .ForeignKey("dbo.Categories", t => t.CategoryID)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Long(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        Cart_ID = c.Long(nullable: false, identity: true),
                        Cart_Name = c.String(),
                        Cart_Price = c.Decimal(precision: 18, scale: 2),
                        Cart_Img = c.String(),
                        CartGroup_ID = c.Long(nullable: false),
                        Quantity = c.Long(nullable: false),
                        OrderId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Cart_ID);
            
            CreateTable(
                "dbo.InformationUsers",
                c => new
                    {
                        UserId = c.Long(nullable: false, identity: true),
                        Id = c.String(),
                        FullName = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        OrderId = c.Long(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.InformationUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Long(nullable: false, identity: true),
                        ProductName = c.String(),
                        Price = c.Decimal(precision: 18, scale: 2),
                        CreatedDate = c.DateTime(),
                        AvailabilityStatus = c.String(),
                        Img = c.String(),
                        CategoryID = c.Long(),
                        BrandID = c.Long(),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.Brands", t => t.BrandID)
                .ForeignKey("dbo.Categories", t => t.CategoryID)
                .Index(t => t.CategoryID)
                .Index(t => t.BrandID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Products", "BrandID", "dbo.Brands");
            DropForeignKey("dbo.Orders", "UserId", "dbo.InformationUsers");
            DropForeignKey("dbo.Brands", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "BrandID" });
            DropIndex("dbo.Products", new[] { "CategoryID" });
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.Brands", new[] { "CategoryID" });
            DropTable("dbo.Products");
            DropTable("dbo.Orders");
            DropTable("dbo.InformationUsers");
            DropTable("dbo.Carts");
            DropTable("dbo.Categories");
            DropTable("dbo.Brands");
        }
    }
}
