namespace Course_Work.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class citiesw : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblCities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        City_name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.tblOffers", "cityId", c => c.Int(nullable: false));
            AddColumn("dbo.tblOffers", "cityName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblOffers", "cityName");
            DropColumn("dbo.tblOffers", "cityId");
            DropTable("dbo.tblCities");
        }
    }
}
