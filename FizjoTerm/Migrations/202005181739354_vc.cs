namespace TabMenu2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Visit", "VisitCompleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Visit", "VisitCompleted");
        }
    }
}
