namespace DumbScrumWebMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserIDfield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "UserID");
        }
    }
}
