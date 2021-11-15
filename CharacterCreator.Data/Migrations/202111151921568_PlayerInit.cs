namespace CharacterCreator.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlayerInit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Player",
                c => new
                    {
                        PlayerId = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 120),
                    })
                .PrimaryKey(t => t.PlayerId);
            
            AddColumn("dbo.Character", "Player_PlayerId", c => c.Guid());
            CreateIndex("dbo.Character", "Player_PlayerId");
            AddForeignKey("dbo.Character", "Player_PlayerId", "dbo.Player", "PlayerId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Character", "Player_PlayerId", "dbo.Player");
            DropIndex("dbo.Character", new[] { "Player_PlayerId" });
            DropColumn("dbo.Character", "Player_PlayerId");
            DropTable("dbo.Player");
        }
    }
}
