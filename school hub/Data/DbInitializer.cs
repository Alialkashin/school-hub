using school_hub.Data;
using school_hub.Models;
using Microsoft.AspNetCore.Identity;

public static class DbInitializer
{
    public static void Seed(AppDBContext context)
    {
        if (!context.Sections.Any())
        {
            context.Sections.AddRange(
               new StudySection { Name = "بكالوريا علمي", Description = "المرحلة المتوسطة من التعليم", ImagePath = "/images/primary.jpg", SectionType = enSectionType.StudySection },
               new StudySection { Name = "بكالوريا أدبي", Description = "المرحلة المتوسطة من التعليم", ImagePath = "/images/middle.jpg", SectionType = enSectionType.StudySection }

           );
            context.SaveChanges();
        }

        if (!context.StudyPlans.Any())
        {
            context.StudyPlans.AddRange(
               new StudyPlan { Name = "الدورة الشتوية", Description = "خطة تعليمية للصف الأول", ImagePath = "/images/plan1.png", StudySectionId = 1 },
               new StudyPlan { Name = "الدورة الصيفية", Description = "خطة تعليمية للصف الثاني", ImagePath = "/images/plan2.png", StudySectionId = 1 }

           );
            context.SaveChanges();
        }



        if (!context.Users.Any())
        {
            var passwordHasher = new PasswordHasher<User>();

            var admin1 = new Admin
            {
                UserName = "admin1",
                Email = "admin1@example.com",
                ProfilePicturePath = "images/admin1.png",
                IsActive = true,
                UserType = enUserType.Admin
            };
            admin1.PasswordHash = passwordHasher.HashPassword(admin1, "Admin@123");

            var admin2 = new Admin
            {
                UserName = "admin2",
                Email = "admin2@example.com",
                ProfilePicturePath = "images/admin2.png",
                IsActive = true,
                UserType = enUserType.Admin
            };
            admin2.PasswordHash = passwordHasher.HashPassword(admin2, "Admin@123");

            var student1 = new Student
            {
                UserName = "student1",
                Email = "student1@example.com",
                ProfilePicturePath = "images/student1.png",
                IsActive = true,
                UserType = enUserType.Student
            };
            student1.PasswordHash = passwordHasher.HashPassword(student1, "Student@123");

            var student2 = new Student
            {
                UserName = "student2",
                Email = "student2@example.com",
                ProfilePicturePath = "images/student2.png",
                IsActive = true,
                UserType = enUserType.Student
            };
            student2.PasswordHash = passwordHasher.HashPassword(student2, "Student@123");

            var teacher = new Teacher
            {
                UserName = "teacher1",
                Email = "teacher1@example.com",
                ProfilePicturePath = "images/teacher1.png",
                IsActive = true,
                UserType = enUserType.Teacher
            };
            teacher.PasswordHash = passwordHasher.HashPassword(teacher, "Teacher@123");

            context.Users.AddRange(admin1, admin2, student1, student2, teacher);
            context.SaveChanges();
        }
        if (!context.Subjects.Any())
        {
            context.Subjects.AddRange(
               new Subject { Name = "الرياضيات", Description = "مادة الرياضيات ", ImagePath = "/images/math.png", TotalDuration = 40, TeacherId = 5, StudyPlanId = 1 },
               new Subject { Name = "اللغة العربية", Description = "مادة اللغة العربية", ImagePath = "/images/arabic.png", TotalDuration = 35, TeacherId = 5, StudyPlanId = 1 }

           );
            context.SaveChanges();
        }

       if (!context.Units.Any())
        {
            context.Units.AddRange(
               new Unit { Name = "الوحدة الأولى", Description = "مقدمة في الأعداد", ImagePath = "images/unit1.png", SubjectId = 1 },
               new Unit { Name = "الوحدة الثانية", Description = "الجمع والطرح", ImagePath = "images/unit2.png", SubjectId = 1 }

           );
            context.SaveChanges();
        }

    }
}
