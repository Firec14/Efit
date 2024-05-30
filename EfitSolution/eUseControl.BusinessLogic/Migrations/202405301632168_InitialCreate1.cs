namespace eUseControl.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactTables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 100),
                        Phone = c.String(nullable: false, maxLength: 15),
                        Message = c.String(nullable: false, maxLength: 500),
                        SubmittedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.ProductTables");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductTables",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        ProductCategory = c.String(nullable: false),
                        ProductPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Level = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId);
            
            DropTable("dbo.ContactTables");
        }
    }
}
