namespace WebsiteNhacOnl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableTaiNhac : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaiNhacs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenNhac = c.String(),
                        File = c.String(),
                        Size = c.Long(nullable: false),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TaiNhacs");
        }
    }
}
