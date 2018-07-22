namespace BlogSpace.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedBlogContentColumnToBlogColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blog", "BlogContent", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Blog", "BlogContent");
        }
    }
}
