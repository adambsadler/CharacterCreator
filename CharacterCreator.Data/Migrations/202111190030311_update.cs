namespace CharacterCreator.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Character", "Player_PlayerId", "dbo.Player");
            DropIndex("dbo.Character", new[] { "Player_PlayerId" });
            RenameColumn(table: "dbo.Character", name: "Player_PlayerId", newName: "PlayerId");
            CreateTable(
                "dbo.Background",
                c => new
                    {
                        BackgroundId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Feature = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.BackgroundId);
            
            AddColumn("dbo.Character", "BackgroundId", c => c.Int(nullable: false));
            AddColumn("dbo.Skill", "Character_CharacterId", c => c.Int());
            AlterColumn("dbo.Character", "PlayerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Character", "PlayerId");
            CreateIndex("dbo.Character", "BackgroundId");
            CreateIndex("dbo.Skill", "Character_CharacterId");
            AddForeignKey("dbo.Character", "BackgroundId", "dbo.Background", "BackgroundId", cascadeDelete: true);
            AddForeignKey("dbo.Skill", "Character_CharacterId", "dbo.Character", "CharacterId");
            AddForeignKey("dbo.Character", "PlayerId", "dbo.Player", "PlayerId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Character", "PlayerId", "dbo.Player");
            DropForeignKey("dbo.Skill", "Character_CharacterId", "dbo.Character");
            DropForeignKey("dbo.Character", "BackgroundId", "dbo.Background");
            DropIndex("dbo.Skill", new[] { "Character_CharacterId" });
            DropIndex("dbo.Character", new[] { "BackgroundId" });
            DropIndex("dbo.Character", new[] { "PlayerId" });
            AlterColumn("dbo.Character", "PlayerId", c => c.Int());
            DropColumn("dbo.Skill", "Character_CharacterId");
            DropColumn("dbo.Character", "BackgroundId");
            DropTable("dbo.Background");
            RenameColumn(table: "dbo.Character", name: "PlayerId", newName: "Player_PlayerId");
            CreateIndex("dbo.Character", "Player_PlayerId");
            AddForeignKey("dbo.Character", "Player_PlayerId", "dbo.Player", "PlayerId");
        }
    }
}
