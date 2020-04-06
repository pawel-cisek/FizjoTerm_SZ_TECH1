namespace TabMenu2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class p1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Patient", "Name");
            DropColumn("dbo.Patient", "Surname");
            DropColumn("dbo.Patient", "Pesel");
            DropColumn("dbo.Patient", "Adress");
            DropColumn("dbo.Patient", "Phone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Patient", "Phone", c => c.String());
            AddColumn("dbo.Patient", "Adress", c => c.String());
            AddColumn("dbo.Patient", "Pesel", c => c.String());
            AddColumn("dbo.Patient", "Surname", c => c.String());
            AddColumn("dbo.Patient", "Name", c => c.String());
        }
    }
}
