namespace Course_Work.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblResume_Update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblResumes", "IsEmailed", c => c.Boolean(nullable: false));
            AddColumn("dbo.tblResumes", "IsSelected", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblResumes", "IsSelected");
            DropColumn("dbo.tblResumes", "IsEmailed");
        }
    }
}
