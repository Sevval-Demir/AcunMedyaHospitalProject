namespace AcunMedyaHospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig1deneme1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "CreatedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appointments", "CreatedDate");
        }
    }
}
