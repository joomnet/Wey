namespace Wey.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Benefits : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Benefits",
                c => new
                    {
                        Key = c.Guid(nullable: false),
                        AffiliateKey = c.Guid(nullable: false),
                        BenefitTypeKey = c.Guid(nullable: false),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ReceiverName = c.String(maxLength: 50),
                        ReceiverSurname = c.String(nullable: false, maxLength: 50),
                        ReceiverAddress = c.String(nullable: false, maxLength: 50),
                        ReceiverPostCode = c.String(nullable: false, maxLength: 50),
                        ReceiverCity = c.String(nullable: false, maxLength: 50),
                        ReceiverCountry = c.String(nullable: false, maxLength: 50),
                        ReceiverTelephone = c.String(nullable: false, maxLength: 50),
                        ReceiverEmail = c.String(nullable: false, maxLength: 320),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Key)
                .ForeignKey("dbo.Affiliates", t => t.AffiliateKey, cascadeDelete: true)
                .ForeignKey("dbo.BenefitTypes", t => t.BenefitTypeKey, cascadeDelete: true)
                .Index(t => t.AffiliateKey)
                .Index(t => t.BenefitTypeKey);
            
            CreateTable(
                "dbo.BenefitStates",
                c => new
                    {
                        Key = c.Guid(nullable: false),
                        BenefitKey = c.Guid(nullable: false),
                        BenefitStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Key)
                .ForeignKey("dbo.Benefits", t => t.BenefitKey, cascadeDelete: true)
                .Index(t => t.BenefitKey);
            
            CreateTable(
                "dbo.BenefitTypes",
                c => new
                    {
                        Key = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Key);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Benefits", "BenefitTypeKey", "dbo.BenefitTypes");
            DropForeignKey("dbo.BenefitStates", "BenefitKey", "dbo.Benefits");
            DropForeignKey("dbo.Benefits", "AffiliateKey", "dbo.Affiliates");
            DropIndex("dbo.BenefitStates", new[] { "BenefitKey" });
            DropIndex("dbo.Benefits", new[] { "BenefitTypeKey" });
            DropIndex("dbo.Benefits", new[] { "AffiliateKey" });
            DropTable("dbo.BenefitTypes");
            DropTable("dbo.BenefitStates");
            DropTable("dbo.Benefits");
        }
    }
}
