namespace FitnessProject.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UProgressDbTables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.String(),
                        UserId = c.Int(nullable: false),
                        WorkoutDone = c.Int(nullable: false),
                        NutritionList = c.String(),
                        SleepHours = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UProgressDbTables");
        }
    }
}
