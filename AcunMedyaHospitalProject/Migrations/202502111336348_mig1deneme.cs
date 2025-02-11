namespace AcunMedyaHospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig1deneme : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Testimonials", newName: "PatientComments");
            AddColumn("dbo.PatientComments", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PatientComments", "Date");
            RenameTable(name: "dbo.PatientComments", newName: "Testimonials");
        }
    }
}
