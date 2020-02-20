namespace Course_Work.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newOfferField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblOffers", "UserID", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblOffers", "UserID");
        }
    }
}
