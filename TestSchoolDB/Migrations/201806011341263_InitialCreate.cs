namespace TestSchoolDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassroomTeachers",
                c => new
                    {
                        ClassroomTeacherId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.ClassroomTeacherId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        Class = c.String(),
                        ClassroomTeacher_ClassroomTeacherId = c.Int(),
                    })
                .PrimaryKey(t => t.StudentId)
                .ForeignKey("dbo.ClassroomTeachers", t => t.ClassroomTeacher_ClassroomTeacherId)
                .Index(t => t.ClassroomTeacher_ClassroomTeacherId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "ClassroomTeacher_ClassroomTeacherId", "dbo.ClassroomTeachers");
            DropIndex("dbo.Students", new[] { "ClassroomTeacher_ClassroomTeacherId" });
            DropTable("dbo.Students");
            DropTable("dbo.ClassroomTeachers");
        }
    }
}
