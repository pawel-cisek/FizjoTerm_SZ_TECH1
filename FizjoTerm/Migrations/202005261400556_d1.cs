namespace TabMenu2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class d1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Referral", "Doctor", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Referral", "Doctor");
        }
    }
}
