using school_hub.Data;
using school_hub.Models;

public static class DbInitializer
{
    public static async Task SeedAsync(AppDBContext context)
    {
        if (!context.Sections.Any())
        {
            await context.Sections.OfType<StudySection>().AddRangeAsync(
                new StudySection { SectionId = 1, Name = "المرحلة الابتدائية", Description = "المرحلة الأولى من التعليم", ImagePath = "images/primary.png" },
                new StudySection { SectionId = 2, Name = "المرحلة الإعدادية", Description = "المرحلة المتوسطة من التعليم", ImagePath = "images/middle.png" },
                new StudySection { SectionId = 3, Name = "المرحلة الثانوية", Description = "المرحلة النهائية من التعليم المدرسي", ImagePath = "images/high.png" },
                new StudySection { SectionId = 4, Name = "المرحلة الجامعية", Description = "مرحلة التعليم العالي", ImagePath = "images/university.png" },
                new StudySection { SectionId = 5, Name = "دراسات عليا", Description = "الماجستير والدكتوراه", ImagePath = "images/postgrad.png" }
            );
            await context.SaveChangesAsync();
        }

        if (!context.StudyPlans.Any())
        {
            await context.StudyPlans.AddRangeAsync(
                new StudyPlan { StudyPlanId = 1, Name = "خطة الصف الأول", Description = "خطة تعليمية للصف الأول", ImagePath = "images/plan1.png", StudySectionId = 1 },
                new StudyPlan { StudyPlanId = 2, Name = "خطة الصف الثاني", Description = "خطة تعليمية للصف الثاني", ImagePath = "images/plan2.png", StudySectionId = 1 },
                new StudyPlan { StudyPlanId = 3, Name = "خطة المرحلة المتوسطة", Description = "خطة عامة للمرحلة المتوسطة", ImagePath = "images/plan3.png", StudySectionId = 2 },
                new StudyPlan { StudyPlanId = 4, Name = "خطة المرحلة الثانوية", Description = "خطة عامة للمرحلة الثانوية", ImagePath = "images/plan4.png", StudySectionId = 3 },
                new StudyPlan { StudyPlanId = 5, Name = "خطة الجامعيين", Description = "خطة دراسية للجامعة", ImagePath = "images/plan5.png", StudySectionId = 4 }
            );
            await context.SaveChangesAsync();
        }

        if (!context.Subjects.Any())
        {
            await context.Subjects.AddRangeAsync(
                new Subject { SubjectId = 1, Name = "الرياضيات", Description = "مادة الرياضيات الأساسية", ImagePath = "images/math.png", TotalDuration = 40, TeacherId = 1, StudyPlanId = 1 },
                new Subject { SubjectId = 2, Name = "اللغة العربية", Description = "مادة اللغة العربية", ImagePath = "images/arabic.png", TotalDuration = 35, TeacherId = 2, StudyPlanId = 1 },
                new Subject { SubjectId = 3, Name = "العلوم", Description = "مادة العلوم الطبيعية", ImagePath = "images/science.png", TotalDuration = 30, TeacherId = 3, StudyPlanId = 2 },
                new Subject { SubjectId = 4, Name = "التاريخ", Description = "مادة التاريخ العام", ImagePath = "images/history.png", TotalDuration = 25, TeacherId = 4, StudyPlanId = 3 },
                new Subject { SubjectId = 5, Name = "الفيزياء", Description = "مادة الفيزياء الحديثة", ImagePath = "images/physics.png", TotalDuration = 45, TeacherId = 5, StudyPlanId = 4 }
            );
            await context.SaveChangesAsync();
        }

        if (!context.Units.Any())
        {
            await context.Units.AddRangeAsync(
                new Unit { UnitId = 1, Name = "الوحدة الأولى", Description = "مقدمة في الأعداد", ImagePath = "images/unit1.png", SubjectId = 1 },
                new Unit { UnitId = 2, Name = "الوحدة الثانية", Description = "الجمع والطرح", ImagePath = "images/unit2.png", SubjectId = 1 },
                new Unit { UnitId = 3, Name = "الوحدة الأولى", Description = "قواعد النحو", ImagePath = "images/unit3.png", SubjectId = 2 },
                new Unit { UnitId = 4, Name = "الوحدة الثالثة", Description = "الكهرباء", ImagePath = "images/unit4.png", SubjectId = 5 },
                new Unit { UnitId = 5, Name = "الوحدة الرابعة", Description = "حروب العالم", ImagePath = "images/unit5.png", SubjectId = 4 }
            );
            await context.SaveChangesAsync();
        }
    }
}
