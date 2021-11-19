namespace CharacterCreator.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedBackgroundClasses : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Background", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Background", "Description", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Background", "Feature", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Background", "Feature", c => c.String(nullable: false));
            AlterColumn("dbo.Background", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Background", "Name", c => c.String(nullable: false));
        }
    }
}
