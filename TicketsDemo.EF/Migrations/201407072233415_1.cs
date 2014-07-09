namespace TicketsDemo.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
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
                        Number = c.String(),
                        StartLocation = c.String(),
                        EndLocation = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PlaceInRun",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlaceId = c.Int(nullable: false),
                        RunId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Place", t => t.PlaceId, cascadeDelete: true)
                .ForeignKey("dbo.Run", t => t.RunId, cascadeDelete: true)
                .Index(t => t.PlaceId)
                .Index(t => t.RunId);
            
            CreateTable(
                "dbo.Reservation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                        PlaceInRunId = c.Int(nullable: false),
                        TicketId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PlaceInRun", t => t.PlaceInRunId, cascadeDelete: true)
                .Index(t => t.PlaceInRunId);
            
            CreateTable(
                "dbo.Ticket",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        ReservationId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Reservation", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.PriceComponent",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TicketId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ticket", t => t.TicketId, cascadeDelete: true)
                .Index(t => t.TicketId);
            
            CreateTable(
                "dbo.Run",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        TrainId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Train", t => t.TrainId, cascadeDelete: false)
                .Index(t => t.TrainId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlaceInRun", "RunId", "dbo.Run");
            DropForeignKey("dbo.Run", "TrainId", "dbo.Train");
            DropForeignKey("dbo.Ticket", "Id", "dbo.Reservation");
            DropForeignKey("dbo.PriceComponent", "TicketId", "dbo.Ticket");
            DropForeignKey("dbo.Reservation", "PlaceInRunId", "dbo.PlaceInRun");
            DropForeignKey("dbo.PlaceInRun", "PlaceId", "dbo.Place");
            DropForeignKey("dbo.Carriage", "TrainId", "dbo.Train");
            DropForeignKey("dbo.Place", "CarriageId", "dbo.Carriage");
            DropIndex("dbo.Run", new[] { "TrainId" });
            DropIndex("dbo.PriceComponent", new[] { "TicketId" });
            DropIndex("dbo.Ticket", new[] { "Id" });
            DropIndex("dbo.Reservation", new[] { "PlaceInRunId" });
            DropIndex("dbo.PlaceInRun", new[] { "RunId" });
            DropIndex("dbo.PlaceInRun", new[] { "PlaceId" });
            DropIndex("dbo.Place", new[] { "CarriageId" });
            DropIndex("dbo.Carriage", new[] { "TrainId" });
            DropTable("dbo.Run");
            DropTable("dbo.PriceComponent");
            DropTable("dbo.Ticket");
            DropTable("dbo.Reservation");
            DropTable("dbo.PlaceInRun");
            DropTable("dbo.Train");
            DropTable("dbo.Place");
            DropTable("dbo.Carriage");
        }
    }
}
