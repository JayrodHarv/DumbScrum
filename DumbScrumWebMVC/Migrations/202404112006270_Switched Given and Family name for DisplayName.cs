namespace DumbScrumWebMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SwitchedGivenandFamilynameforDisplayName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DisplayName", c => c.String());
            DropColumn("dbo.AspNetUsers", "GivenName");
            DropColumn("dbo.AspNetUsers", "FamilyName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "FamilyName", c => c.String());
            AddColumn("dbo.AspNetUsers", "GivenName", c => c.String());
            DropColumn("dbo.AspNetUsers", "DisplayName");
        }
    }
}
