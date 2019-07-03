namespace Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SearchDetail",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        KeyWord = c.String(maxLength: 50, storeType: "nvarchar"),
                        SearchTime = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SearchTotal",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        KeyWord = c.String(maxLength: 50, storeType: "nvarchar"),
                        SearchCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SearchTotal");
            DropTable("dbo.SearchDetail");
        }
    }
}
