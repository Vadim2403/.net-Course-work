namespace Course_Work.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblResumes_Update2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tblResumes", "Additionals", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tblResumes", "Additionals", c => c.String(nullable: false));
        }
    }
}
