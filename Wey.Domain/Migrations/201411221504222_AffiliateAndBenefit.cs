namespace Wey.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AffiliateAndBenefit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Affiliates",
                c => new
                    {
                        Key = c.Guid(nullable: false),
                        CompanyName = c.String(nullable: false, maxLength: 50),
                        Address = c.String(nullable: false, maxLength: 50),
                        TelephoneNumber = c.String(maxLength: 50),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Key)
                .ForeignKey("dbo.Users", t => t.Key)
                .Index(t => t.Key);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Affiliates", "Key", "dbo.Users");
            DropIndex("dbo.Affiliates", new[] { "Key" });
            DropTable("dbo.Affiliates");
        }
    }
}
