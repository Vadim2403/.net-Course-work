namespace Course_Work.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblResumes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblResumes", "Offer_Id", c => c.String(nullable: false));
            AddColumn("dbo.tblResumes", "User_Id", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblResumes", "User_Id");
            DropColumn("dbo.tblResumes", "Offer_Id");
        }
    }
}
