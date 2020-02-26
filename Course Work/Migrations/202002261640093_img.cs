namespace Course_Work.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class img : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblOffers", "ImageName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblOffers", "ImageName");
        }
    }
}
