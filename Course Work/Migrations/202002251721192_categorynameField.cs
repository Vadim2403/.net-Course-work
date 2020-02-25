namespace Course_Work.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class categorynameField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblOffers", "CategoryName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblOffers", "CategoryName");
        }
    }
}
