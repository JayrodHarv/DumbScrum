namespace DumbScrumWebMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedGivenNameandFamilyNamefieldstouser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "GivenName", c => c.String());
            AddColumn("dbo.AspNetUsers", "FamilyName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "FamilyName");
            DropColumn("dbo.AspNetUsers", "GivenName");
        }
    }
}
