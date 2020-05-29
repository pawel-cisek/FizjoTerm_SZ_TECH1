namespace TabMenu2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class r1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Visit", "Report_IdReport", "dbo.Report");
            DropIndex("dbo.Visit", new[] { "Report_IdReport" });
            CreateTable(
                "dbo.VisitReports",
                c => new
                    {
                        Visit_IdVisit = c.Int(nullable: false),
                        Report_IdReport = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Visit_IdVisit, t.Report_IdReport })
                .ForeignKey("dbo.Visit", t => t.Visit_IdVisit, cascadeDelete: true)
                .ForeignKey("dbo.Report", t => t.Report_IdReport, cascadeDelete: true)
                .Index(t => t.Visit_IdVisit)
                .Index(t => t.Report_IdReport);
            
            DropColumn("dbo.Visit", "Report_IdReport");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Visit", "Report_IdReport", c => c.Int());
            DropForeignKey("dbo.VisitReports", "Report_IdReport", "dbo.Report");
            DropForeignKey("dbo.VisitReports", "Visit_IdVisit", "dbo.Visit");
            DropIndex("dbo.VisitReports", new[] { "Report_IdReport" });
            DropIndex("dbo.VisitReports", new[] { "Visit_IdVisit" });
            DropTable("dbo.VisitReports");
            CreateIndex("dbo.Visit", "Report_IdReport");
            AddForeignKey("dbo.Visit", "Report_IdReport", "dbo.Report", "IdReport");
        }
    }
}
