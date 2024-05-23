namespace FitnessProject.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NutritionDbTables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Protein = c.Double(nullable: false),
                        Carbohydrate = c.Double(nullable: false),
                        Fat = c.Double(nullable: false),
                        EnergyValue = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SessionsDbTables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cookie = c.String(),
                        UserId = c.Int(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UDbTables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 30),
                        Password = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 30),
                        Gender = c.String(nullable: false),
                        Age = c.Int(nullable: false),
                        Height = c.Int(nullable: false),
                        Weight = c.Single(nullable: false),
                        WeightTarget = c.Single(nullable: false),
                        WorkoutPacketID = c.Int(nullable: false),
                        LastLogin = c.DateTime(nullable: false),
                        LastIp = c.String(maxLength: 30),
                        Level = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UDbTables");
            DropTable("dbo.SessionsDbTables");
            DropTable("dbo.NutritionDbTables");
        }
    }
}
