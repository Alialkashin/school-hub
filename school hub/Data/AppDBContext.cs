using System.Buffers;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Abstractions;
using school_hub.Models;
using school_hub.Models.Exam;
using school_hub.Models.Lesson;
using school_hub.Models.Sections;
using school_hub.Models.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace school_hub.Data
{

	public class AppDBContext : IdentityDbContext<User,IdentityRole<int>,int>
	{
		public AppDBContext(DbContextOptions options) : base(options)
		{
			
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			#region composit keys
			modelBuilder.Entity<StudentRating>().HasKey(e => new { e.StudentId, e.LessonId });   //set composit primary key to StudentRating
			modelBuilder.Entity<StudentStudyPlan>().HasKey(e => new {e.StudentId,e.PlanId});     //set composit primary key to StudentSubscription
			modelBuilder.Entity<StudentView>().HasKey(e => new { e.StudentId, e.VideoId });       //set composit primary key to StudentViews
			modelBuilder.Entity<StudentAnswer>().HasKey(e => new {e.StudentExamId, e.AnswerId});
			#endregion

			#region primary keys
			modelBuilder.Entity<Answer>().HasKey(e => e.AnswerId);
			modelBuilder.Entity<Exam>().HasKey(e => e.ExamId);
			modelBuilder.Entity<Question>().HasKey(e => e.QuestionId);
			modelBuilder.Entity<StudentExam>().HasKey(e => e.StudentExamId);
			modelBuilder.Entity<Comment>().HasKey(e => e.CommentId);
			modelBuilder.Entity<Lesson>().HasKey(e => e.LessonId);
			modelBuilder.Entity<Reply>().HasKey(e => e.ReplyId);
			modelBuilder.Entity<Video>().HasKey(e => e.VideoId);
			modelBuilder.Entity<Section>().HasKey(e => e.SectionId);
			modelBuilder.Entity<Book>().HasKey(e => e.BookId);
			modelBuilder.Entity<StudyPlan>().HasKey(sp => sp.StudyPlanId);
			modelBuilder.Entity<Subject>().HasKey(s => s.SubjectId);
			#endregion

			#region default values
			//modelBuilder.Entity<Comment>().Property(p => p.CommentDate).HasDefaultValue(DateTime.Now);//set default value in current date
			//modelBuilder.Entity<Exam>().Property(p => p.ExamTime).HasDefaultValue(new TimeOnly().AddMinutes(15));//set default value to Exam time
			//modelBuilder.Entity<Reply>().Property(p => p.ReplyDate).HasDefaultValue(DateTime.Now);//set default value in current date
			//modelBuilder.Entity<StudentExam>().Property(p => p.ExamDate).HasDefaultValue(DateTime.Now);//set default value in current date
			//modelBuilder.Entity<StudentRating>().Property(p => p.RatingValue).HasDefaultValue(2);//set default value to ratting is 2
			//modelBuilder.Entity<StudentStudyPlan>().Property(p => p.PaymentStatus).HasDefaultValue(enPaymentStatus.Progress);
			//modelBuilder.Entity<User>().Property(p => p.IsActive).HasDefaultValue(true);
			#endregion

			#region Relationships

			modelBuilder.Entity<Question>()
				.HasMany(q => q.Answers)
				.WithOne(a => a.Question)
				.HasForeignKey(q => q.QuestionId )
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Exam>()
				.HasMany(e => e.Questions)
				.WithOne(q => q.Exam)
				.HasForeignKey(q => q.ExamId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Exam>()
				.HasOne(e => e.Lesson)
				.WithOne(l => l.Exam)
				.HasForeignKey<Exam>(e => e.LessonId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<LibrarySection>()
			.HasMany(ls => ls.Books)
			.WithOne(b => b.LibrarySection)
			.HasForeignKey(b => b.LibrarySectionId)
			.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<StudySection>()
			.HasMany(ss => ss.StudyPlans)
			.WithOne(sp => sp.StudySection)
			.HasForeignKey(sp => sp.StudySectionId)
			.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<StudyPlan>()
			.HasMany(sp => sp.Subjects)
			.WithOne(s => s.StudyPlan)
			.HasForeignKey(s => s.StudyPlanId)
			.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Subject>()
			.HasOne(s => s.Teacher)
			.WithMany(t => t.Subjects)
			.HasForeignKey(s => s.TeacherId)
			.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Unit>()
			.HasMany(u => u.Lessons)
			.WithOne(l => l.Unit)
			.HasForeignKey(l => l.UnitId)
			.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Unit>()
			.HasOne(u => u.Subject)
			.WithMany(s => s.Units)
			.HasForeignKey(u => u.SubjectId)
			.OnDelete(DeleteBehavior.NoAction);
			
			modelBuilder.Entity<Lesson>()
			.HasMany(l => l.Videos)
			.WithOne(v => v.Lesson)
			.HasForeignKey(v => v.LessonId)
			.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Video>()
			.HasMany(v => v.Comments)
			.WithOne(c => c.Video)
			.HasForeignKey( c => c.VideoId)
			.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Comment>()
			.HasMany(c => c.Replys)
			.WithOne(r => r.Comment)
			.HasForeignKey(r => r.CommentId)
			.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Comment>()
			.HasOne(c => c.Teacher)
			.WithMany(t => t.Comments)
			.HasForeignKey(c => c.TeacherId)
			.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Comment>()
			.HasOne(c => c.Student)
			.WithMany(s => s.Comments)
			.HasForeignKey(c => c.StudentId)
			.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Reply>()
			.HasOne(r => r.Student)
			.WithMany(s => s.Replies)
			.HasForeignKey(r => r.StudentId)
			.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Reply>()
			.HasOne(r => r.Teacher)
			.WithMany(t => t.Replies)
			.HasForeignKey(r => r.TeacherId)
			.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<StudentAnswer>()
				.HasOne(sa => sa.Answer)
				.WithMany(a => a.StudentAnswers)
				.HasForeignKey( sa => sa.AnswerId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<StudentAnswer>()
				.HasOne(sa => sa.Answer)
				.WithMany(a => a.StudentAnswers)
				.HasForeignKey(sa => sa.AnswerId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<StudentView>()
			.HasOne(sv => sv.Student)
			.WithMany(s => s.StudentViews)
			.HasForeignKey(sv => sv.StudentId)
			.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<StudentView>()
			.HasOne(sv => sv.Video)
			.WithMany(v => v.Views)
			.HasForeignKey(sv => sv.VideoId)
			.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<StudentRating>()
			.HasOne(sr => sr.Student)
			.WithMany(s => s.StudentRatings)
			.HasForeignKey(sr => sr.StudentId)
			.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<StudentRating>()
			.HasOne(sr => sr.Lesson)
			.WithMany(l => l.Ratings)
			.HasForeignKey(sr => sr.LessonId)
			.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<StudentStudyPlan>()
			.HasOne(ssp => ssp.StudyPlan)
			.WithMany(sp => sp.StudyPlanStudents)
			.HasForeignKey ( ssp => ssp.PlanId)
			.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<StudentStudyPlan>()
			.HasOne(ssp => ssp.Student)
			.WithMany(s => s.StudentStudyPlans)
			.HasForeignKey(ssp => ssp.StudentId)
			.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<StudentExam>()
				.HasOne(se => se.Exam)
				.WithMany(e => e.StudentExams)
				.HasForeignKey(se => se.ExamId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<StudentExam>()
				.HasOne(sa => sa.Student)
				.WithMany(s => s.StudentExams)
				.HasForeignKey(sa => sa.StudentId)
				.OnDelete(DeleteBehavior.NoAction);
			
			#endregion

			#region TPH
			modelBuilder.Entity<Section>().HasDiscriminator(p => p.SectionType)//TPH
				.HasValue<StudySection>(enSectionType.StudySection)
				.HasValue<LibrarySection>(enSectionType.LibrarySection);

			modelBuilder.Entity<User>().HasDiscriminator(p => p.UserType)//TPH
				.HasValue<Admin>(enUserType.Admin)
				.HasValue<Student>(enUserType.Student)
				.HasValue<Teacher>(enUserType.Teacher);
			#endregion
		}
		#region Tables
		public DbSet<Section> Sections { get; set; }
		public DbSet<Unit> Units { get; set; }
		public DbSet<Subject> Subjects { get; set; }
		public DbSet<StudyPlan> StudyPlans { get; set; }
		public DbSet<Lesson> Lessons { get; set; }
		public DbSet<Video> Videos { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Reply> Replys { get; set; }
		public DbSet<Book> Books { get; set; }
		public DbSet<Exam> Exams { get; set; }
		public DbSet<Question> Questions { get; set; }
		public DbSet<Answer> Answers { get; set; }
		public DbSet<StudentExam> StudentExams { get; set; }
		public DbSet<StudentView> StudentViews  { get; set; }
		public DbSet<StudentAnswer> StudentAnswers { get; set; }
		public DbSet<StudentRating> StudentRatings { get; set; }
		public DbSet<StudentStudyPlan> StudentSubscriptions { get; set; }
		#endregion
	}
}
