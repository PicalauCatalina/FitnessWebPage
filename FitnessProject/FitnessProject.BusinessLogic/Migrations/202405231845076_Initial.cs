namespace FitnessProject.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkoutDbTables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PacketName = c.String(nullable: false),
                        Json = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WorkoutDbTables");
        }
    }
}
