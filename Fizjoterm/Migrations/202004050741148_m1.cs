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
                        PatientID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Pesel = c.String(),
                        Adress = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.PatientID);
            
            CreateTable(
                "dbo.Referral",
                c => new
                    {
                        ReferralID = c.Int(nullable: false, identity: true),
                        PatientID = c.Int(nullable: false),
                        Diagnosis = c.String(),
                        Icd10 = c.String(),
                        NbOfDays = c.Int(nullable: false),
                        DateReferral = c.DateTime(nullable: false),
                        DateSaved = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ReferralID)
                .ForeignKey("dbo.Patient", t => t.PatientID, cascadeDelete: true)
                .Index(t => t.PatientID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Referral", "PatientID", "dbo.Patient");
            DropIndex("dbo.Referral", new[] { "PatientID" });
            DropTable("dbo.Referral");
            DropTable("dbo.Patient");
        }
    }
}
