namespace TabMenu2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class p2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patient", "Name", c => c.String());
            AddColumn("dbo.Patient", "Surname", c => c.String());
            AddColumn("dbo.Patient", "Pesel", c => c.String());
            AddColumn("dbo.Patient", "Adress", c => c.String());
            AddColumn("dbo.Patient", "Phone", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Patient", "Phone");
            DropColumn("dbo.Patient", "Adress");
            DropColumn("dbo.Patient", "Pesel");
            DropColumn("dbo.Patient", "Surname");
            DropColumn("dbo.Patient", "Name");
        }
    }
}
