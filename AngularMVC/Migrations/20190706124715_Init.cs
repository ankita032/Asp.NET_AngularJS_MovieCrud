namespace AngularMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProducersActors",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id);

            CreateTable(
                "dbo.Movies",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    YearOfRelease = c.Int(),
                    ProducerId = c.Int()
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProducersActors", t => t.ProducerId);
        }
        public override void Down()
        {
            DropForeignKey("dbo.ProducersActors", "Id", "dbo.Movies");
            DropIndex("dbo.ProducersActors", new[] { "Id" });
            DropTable("dbo.Movies");
            DropTable("dbo.ProducersActors");
        }
    }
}