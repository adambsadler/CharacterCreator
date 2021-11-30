namespace CharacterCreator.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Background", "Description", c => c.String(nullable: false, maxLength: 3000));
            AlterColumn("dbo.Background", "Feature", c => c.String(nullable: false, maxLength: 3000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Background", "Feature", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Background", "Description", c => c.String(nullable: false, maxLength: 500));
        }
    }
}
