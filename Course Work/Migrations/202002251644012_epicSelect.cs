namespace Course_Work.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class epicSelect : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblOffers", "categoryId", c => c.Int(nullable: false));
            DropColumn("dbo.tblOffers", "category");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblOffers", "category", c => c.String(nullable: false));
            DropColumn("dbo.tblOffers", "categoryId");
        }
    }
}
