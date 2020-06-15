namespace TabMenu2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rc1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Referral", "ReferralCompleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Referral", "ReferralCompleted");
        }
    }
}
