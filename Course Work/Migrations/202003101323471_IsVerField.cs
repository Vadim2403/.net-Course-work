namespace Course_Work.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsVerField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblOffers", "IsVerified", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblOffers", "IsVerified");
        }
    }
}
