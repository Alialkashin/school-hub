using school_hub.Data;
using school_hub.Models;
using Microsoft.AspNetCore.Identity;

public static class DbInitializer
{
    public static async Task SeedAsync(AppDBContext context)
    {
        if (!context.Sections.Any())
        {
            await context.Sections.AddRangeAsync(
                new StudySection { SectionId = 1, Name = "بكالوريا علمي", Description = "المرحلة المتوسطة من التعليم", ImagePath = "images/primary.jpg", SectionType = enSectionType.StudySection },
                new StudySection { SectionId = 2, Name = "بكالوريا أدبي", Description = "المرحلة المتوسطة من التعليم", ImagePath = "images/middle.jpg", SectionType = enSectionType.StudySection }
             
            );
            await context.SaveChangesAsync();
        }

        if (!context.StudyPlans.Any())
        {
            await context.StudyPlans.AddRangeAsync(
                new StudyPlan { StudyPlanId = 1, Name = "الدورة الشتوية", Description = "خطة تعليمية للصف الأول", ImagePath = "images/plan1.jpg", StudySectionId = 1 },
                new StudyPlan { StudyPlanId = 2, Name = "الدورة الصيفية", Description = "خطة تعليمية للصف الثاني", ImagePath = "images/plan2.jpg", StudySectionId = 1 }
             
            );
            await context.SaveChangesAsync();
        }

        if (!context.Subjects.Any())
        {
            await context.Subjects.AddRangeAsync(
                new Subject { SubjectId = 1, Name = "الرياضيات", Description = "مادة الرياضيات ", ImagePath = "images/math.png", TotalDuration = 40, TeacherId = 1, StudyPlanId = 1 },
                new Subject { SubjectId = 2, Name = "اللغة العربية", Description = "مادة اللغة العربية", ImagePath = "images/arabic.png", TotalDuration = 35, TeacherId = 2, StudyPlanId = 1 }
            
            );
            await context.SaveChangesAsync();
        }

        if (!context.Units.Any())
        {
            await context.Units.AddRangeAsync(
                new Unit { UnitId = 1, Name = "الوحدة الأولى", Description = "مقدمة في الأعداد", ImagePath = "images/unit1.png", SubjectId = 1 },
                new Unit { UnitId = 2, Name = "الوحدة الثانية", Description = "الجمع والطرح", ImagePath = "images/unit2.png", SubjectId = 1 }
          
            );
            await context.SaveChangesAsync();
        }
        
        if (!context.Users.Any())
{
    var passwordHasher = new PasswordHasher<User>();

    var admin1 = new Admin
    {
        Id = 1,
        UserName = "admin1",
        Email = "admin1@example.com",
        ProfilePicturePath = "images/admin1.png",
        IsActive = true,
        UserType = enUserType.Admin
    };
    admin1.PasswordHash = passwordHasher.HashPassword(admin1, "Admin@123");

    var admin2 = new Admin
    {
        Id = 2,
        UserName = "admin2",
        Email = "admin2@example.com",
        ProfilePicturePath = "images/admin2.png",
        IsActive = true,
        UserType = enUserType.Admin
    };
    admin2.PasswordHash = passwordHasher.HashPassword(admin2, "Admin@123");

    var student1 = new Student
    {
        Id = 3,
        UserName = "student1",
        Email = "student1@example.com",
        ProfilePicturePath = "images/student1.png",
        IsActive = true,
        UserType = enUserType.Student
    };
    student1.PasswordHash = passwordHasher.HashPassword(student1, "Student@123");

    var student2 = new Student
    {
        Id = 4,
        UserName = "student2",
        Email = "student2@example.com",
        ProfilePicturePath = "images/student2.png",
        IsActive = true,
        UserType = enUserType.Student
    };
    student2.PasswordHash = passwordHasher.HashPassword(student2, "Student@123");

    var teacher = new Teacher
    {
        Id = 5,
        UserName = "teacher1",
        Email = "teacher1@example.com",
        ProfilePicturePath = "images/teacher1.png",
        IsActive = true,
        UserType = enUserType.Teacher
    };
    teacher.PasswordHash = passwordHasher.HashPassword(teacher, "Teacher@123");

    await context.Users.AddRangeAsync(admin1, admin2, student1, student2, teacher);
    await context.SaveChangesAsync();
}

    }
}
