namespace TicketsDemo.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Place", "CarriageId", "dbo.Carriage");
            DropForeignKey("dbo.Carriage", "TrainId", "dbo.Train");
            DropForeignKey("dbo.PlaceInRun", "PlaceId", "dbo.Place");
            DropForeignKey("dbo.Run", "TrainId", "dbo.Train");
            DropIndex("dbo.Carriage", new[] { "TrainId" });
            DropIndex("dbo.Place", new[] { "CarriageId" });
            DropIndex("dbo.PlaceInRun", new[] { "PlaceId" });
            DropIndex("dbo.Run", new[] { "TrainId" });
            DropTable("dbo.Carriage");
            DropTable("dbo.Place");
            DropTable("dbo.Train");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Train",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        StartLocation = c.String(),
                        EndLocation = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Place",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        PriceMultiplier = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CarriageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Carriage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        DefaultPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TrainId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Run", "TrainId");
            CreateIndex("dbo.PlaceInRun", "PlaceId");
            CreateIndex("dbo.Place", "CarriageId");
            CreateIndex("dbo.Carriage", "TrainId");
            AddForeignKey("dbo.Run", "TrainId", "dbo.Train", "Id", cascadeDelete: false);
            AddForeignKey("dbo.PlaceInRun", "PlaceId", "dbo.Place", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Carriage", "TrainId", "dbo.Train", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Place", "CarriageId", "dbo.Carriage", "Id", cascadeDelete: true);
        }
    }
}
