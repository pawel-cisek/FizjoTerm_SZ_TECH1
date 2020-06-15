namespace TabMenu2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class s1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Visit", "VisitSettled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Visit", "VisitSettled");
        }
    }
}
