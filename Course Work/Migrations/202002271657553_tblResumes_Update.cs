namespace Course_Work.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblResumes_Update : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tblResumes", "Offer_Id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tblResumes", "Offer_Id", c => c.String(nullable: false));
        }
    }
}
