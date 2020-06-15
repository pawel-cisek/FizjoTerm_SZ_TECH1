namespace TabMenu2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class t1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Referral", "Treatments", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Referral", "Treatments");
        }
    }
}
