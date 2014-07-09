namespace TicketsDemo.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carriage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        DefaultPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TrainId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Train", t => t.TrainId, cascadeDelete: true)
                .Index(t => t.TrainId);
            
            CreateTable(
                "dbo.Place",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        PriceMultiplier = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CarriageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Carriage", t => t.CarriageId, cascadeDelete: true)
                .Index(t => t.CarriageId);
            
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
            
            CreateIndex("dbo.PlaceInRun", "PlaceId");
            CreateIndex("dbo.Run", "TrainId");
            AddForeignKey("dbo.PlaceInRun", "PlaceId", "dbo.Place", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Run", "TrainId", "dbo.Train", "Id", cascadeDelete: false);//тут было true
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Run", "TrainId", "dbo.Train");
            DropForeignKey("dbo.PlaceInRun", "PlaceId", "dbo.Place");
            DropForeignKey("dbo.Carriage", "TrainId", "dbo.Train");
            DropForeignKey("dbo.Place", "CarriageId", "dbo.Carriage");
            DropIndex("dbo.Run", new[] { "TrainId" });
            DropIndex("dbo.PlaceInRun", new[] { "PlaceId" });
            DropIndex("dbo.Place", new[] { "CarriageId" });
            DropIndex("dbo.Carriage", new[] { "TrainId" });
            DropTable("dbo.Train");
            DropTable("dbo.Place");
            DropTable("dbo.Carriage");
        }
    }
}
