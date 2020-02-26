namespace Course_Work.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class split : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Category_name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.tblOffers", "categoryId", c => c.Int(nullable: false));
            AddColumn("dbo.tblOffers", "CategoryName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblOffers", "CategoryName");
            DropColumn("dbo.tblOffers", "categoryId");
            DropTable("dbo.tblCategory");
        }
    }
}
