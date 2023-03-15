namespace WebsiteNhacOnl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TablePhanHoiNguoiDung : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PhanHoiNDs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HoTen = c.String(),
                        NamSinh = c.String(),
                        Sdt = c.String(),
                        Mail = c.String(),
                        DiaChi = c.String(),
                        NdPhanHoi = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PhanHoiNDs");
        }
    }
}
