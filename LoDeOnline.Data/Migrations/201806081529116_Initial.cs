namespace LoDeOnline.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TinTucs", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.TinTucs", "WritedById", "dbo.AspNetUsers");
            DropIndex("dbo.TinTucs", new[] { "CreatedById" });
            DropIndex("dbo.TinTucs", new[] { "WritedById" });
            DropTable("dbo.TinTucs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TinTucs",
                c => new
                    {
                        MaTin = c.Int(nullable: false),
                        ThoiGian = c.DateTime(nullable: false),
                        TieuDe = c.String(),
                        NoiDung = c.String(),
                        Id = c.Long(nullable: false, identity: true),
                        CreatedById = c.String(maxLength: 128),
                        WritedById = c.String(maxLength: 128),
                        DateCreated = c.DateTime(),
                        LastUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.MaTin);
            
            CreateIndex("dbo.TinTucs", "WritedById");
            CreateIndex("dbo.TinTucs", "CreatedById");
            AddForeignKey("dbo.TinTucs", "WritedById", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.TinTucs", "CreatedById", "dbo.AspNetUsers", "Id");
        }
    }
}
