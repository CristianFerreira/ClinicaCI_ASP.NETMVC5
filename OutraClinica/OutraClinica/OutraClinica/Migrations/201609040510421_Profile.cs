namespace OutraClinica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Profile : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Funcionarios", "email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Funcionarios", "email", c => c.String());
        }
    }
}
