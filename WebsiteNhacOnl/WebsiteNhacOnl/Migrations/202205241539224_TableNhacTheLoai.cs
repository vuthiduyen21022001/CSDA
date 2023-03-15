namespace WebsiteNhacOnl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableNhacTheLoai : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BaiHats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenBH = c.String(),
                        CaSi = c.String(),
                        TacGia = c.String(),
                        NgayCapNhat = c.DateTime(nullable: false),
                        HinhAnhBH = c.String(),
                        UrlBH = c.String(),
                        LoiBH = c.String(),
                        MaTL = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TheLoais", t => t.MaTL, cascadeDelete: true)
                .Index(t => t.MaTL);
            
            CreateTable(
                "dbo.TheLoais",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenTL = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BaiHats", "MaTL", "dbo.TheLoais");
            DropIndex("dbo.BaiHats", new[] { "MaTL" });
            DropTable("dbo.TheLoais");
            DropTable("dbo.BaiHats");
        }
    }
}
