namespace BlogSpace.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBlogContentColumnToTheBlog : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blog", "ShortDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Blog", "ShortDescription");
        }
    }
}
