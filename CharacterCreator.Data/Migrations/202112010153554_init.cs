namespace CharacterCreator.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Background",
                c => new
                    {
                        BackgroundId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false, maxLength: 3000),
                        Feature = c.String(nullable: false, maxLength: 3000),
                    })
                .PrimaryKey(t => t.BackgroundId);
            
            CreateTable(
                "dbo.Character",
                c => new
                    {
                        CharacterId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        PlayerId = c.Int(nullable: false),
                        Strength = c.Int(nullable: false),
                        Dexterity = c.Int(nullable: false),
                        Constitution = c.Int(nullable: false),
                        Intelligence = c.Int(nullable: false),
                        Wisdom = c.Int(nullable: false),
                        Charisma = c.Int(nullable: false),
                        Race = c.String(nullable: false),
                        CharacterClass = c.String(nullable: false),
                        BackgroundId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CharacterId)
                .ForeignKey("dbo.Background", t => t.BackgroundId, cascadeDelete: true)
                .ForeignKey("dbo.Player", t => t.PlayerId, cascadeDelete: true)
                .Index(t => t.PlayerId)
                .Index(t => t.BackgroundId);
            
            CreateTable(
                "dbo.Player",
                c => new
                    {
                        PlayerId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 120),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.PlayerId);
            
            CreateTable(
                "dbo.Skill",
                c => new
                    {
                        SkillId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 240),
                        Description = c.String(nullable: false),
                        AbilityType = c.String(),
                    })
                .PrimaryKey(t => t.SkillId);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.SkillCharacter",
                c => new
                    {
                        Skill_SkillId = c.Int(nullable: false),
                        Character_CharacterId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Skill_SkillId, t.Character_CharacterId })
                .ForeignKey("dbo.Skill", t => t.Skill_SkillId, cascadeDelete: true)
                .ForeignKey("dbo.Character", t => t.Character_CharacterId, cascadeDelete: true)
                .Index(t => t.Skill_SkillId)
                .Index(t => t.Character_CharacterId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.SkillCharacter", "Character_CharacterId", "dbo.Character");
            DropForeignKey("dbo.SkillCharacter", "Skill_SkillId", "dbo.Skill");
            DropForeignKey("dbo.Character", "PlayerId", "dbo.Player");
            DropForeignKey("dbo.Character", "BackgroundId", "dbo.Background");
            DropIndex("dbo.SkillCharacter", new[] { "Character_CharacterId" });
            DropIndex("dbo.SkillCharacter", new[] { "Skill_SkillId" });
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.Character", new[] { "BackgroundId" });
            DropIndex("dbo.Character", new[] { "PlayerId" });
            DropTable("dbo.SkillCharacter");
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.Skill");
            DropTable("dbo.Player");
            DropTable("dbo.Character");
            DropTable("dbo.Background");
        }
    }
}
