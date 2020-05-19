namespace TabMenu2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Patient",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Pesel = c.String(),
                        Name = c.String(),
                        Surname = c.String(),
                        Adress = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Physiotherapist",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Npwz = c.Int(nullable: false),
                        Name = c.String(),
                        Surname = c.String(),
                        Adress = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Referral",
                c => new
                    {
                        IdReferral = c.Int(nullable: false, identity: true),
                        IdPatient = c.Int(nullable: false),
                        Diagnosis = c.String(),
                        Icd10 = c.String(),
                        NbOfDays = c.Int(nullable: false),
                        DateReferral = c.DateTime(nullable: false),
                        DateSaved = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.IdReferral)
                .ForeignKey("dbo.Patient", t => t.IdPatient, cascadeDelete: true)
                .Index(t => t.IdPatient);
            
            CreateTable(
                "dbo.Report",
                c => new
                    {
                        IdReport = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.IdReport);
            
            CreateTable(
                "dbo.Visit",
                c => new
                    {
                        IdVisit = c.Int(nullable: false, identity: true),
                        IdPhysiotherapist = c.Int(nullable: false),
                        IdReferral = c.Int(nullable: false),
                        VisitDate = c.DateTime(nullable: false),
                        VisitTime = c.String(),
                        DateSaved = c.DateTime(nullable: false),
                        Report_IdReport = c.Int(),
                    })
                .PrimaryKey(t => t.IdVisit)
                .ForeignKey("dbo.Physiotherapist", t => t.IdPhysiotherapist, cascadeDelete: true)
                .ForeignKey("dbo.Referral", t => t.IdReferral, cascadeDelete: true)
                .ForeignKey("dbo.Report", t => t.Report_IdReport)
                .Index(t => t.IdPhysiotherapist)
                .Index(t => t.IdReferral)
                .Index(t => t.Report_IdReport);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Visit", "Report_IdReport", "dbo.Report");
            DropForeignKey("dbo.Visit", "IdReferral", "dbo.Referral");
            DropForeignKey("dbo.Visit", "IdPhysiotherapist", "dbo.Physiotherapist");
            DropForeignKey("dbo.Referral", "IdPatient", "dbo.Patient");
            DropIndex("dbo.Visit", new[] { "Report_IdReport" });
            DropIndex("dbo.Visit", new[] { "IdReferral" });
            DropIndex("dbo.Visit", new[] { "IdPhysiotherapist" });
            DropIndex("dbo.Referral", new[] { "IdPatient" });
            DropTable("dbo.Visit");
            DropTable("dbo.Report");
            DropTable("dbo.Referral");
            DropTable("dbo.Physiotherapist");
            DropTable("dbo.Patient");
        }
    }
}
