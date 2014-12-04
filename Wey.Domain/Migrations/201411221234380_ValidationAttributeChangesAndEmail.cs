namespace Wey.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValidationAttributeChangesAndEmail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Email", c => c.String(nullable: false, maxLength: 320));
            AlterColumn("dbo.Roles", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Users", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Roles", "Name", c => c.String(nullable: false));
            DropColumn("dbo.Users", "Email");
        }
    }
}
