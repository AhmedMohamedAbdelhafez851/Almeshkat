using Domains;
using Domains.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace BL.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<TrackUsersProcess> TrackUsersProcesses { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Year> Years { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Term> Terms { get; set; }
        public DbSet<Stage> Stages { get; set; } 
        public DbSet<SubStage> SubStages { get; set; }  
        public DbSet<StudentsStudyInfo> StudentsStudyInfos { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }
        public DbSet<TeacherSubject> TeacherSubjects { get; set; }
        public DbSet<StageSubject> StageSubjects { get; set; }
        public DbSet<TimeTable> TimeTables { get; set; }    
        public DbSet<TimeTableGroup> TimeTableGroups { get; set; }
        public DbSet<TimeTableGroupDetail> TimeTableGroupsDetails { get; set; }    


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships for StageSubject to prevent multiple cascade paths
            modelBuilder.Entity<StageSubject>()
                .HasOne(ss => ss.Term)
                .WithMany(t => t.StageSubjects)
                .HasForeignKey(ss => ss.TermId)
                .OnDelete(DeleteBehavior.Restrict);  // Change to Restrict or NoAction to avoid cascade conflict

            modelBuilder.Entity<StageSubject>()
                .HasOne(ss => ss.SubStage)
                .WithMany(s => s.StageSubjects)
                .HasForeignKey(ss => ss.SubStageId)
                .OnDelete(DeleteBehavior.Cascade);  // Keep Cascade if desired

            modelBuilder.Entity<StageSubject>()
                .HasOne(ss => ss.Subject)
                .WithMany(s => s.StageSubjects)
                .HasForeignKey(ss => ss.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);  // Keep Cascade if desired            // Additional configurations for TrackUsersProcess if needed


            modelBuilder.Entity<TrackUsersProcess>()
                .HasKey(t => t.TraPerProid); // Primary Key


            // Configure cascade behavior for StudentsStudyInf relationships
            modelBuilder.Entity<StudentsStudyInfo>()
                .HasOne(s => s.User)
                .WithMany()
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Keep cascade if this is your intended behavior

            modelBuilder.Entity<StudentsStudyInfo>()
                .HasOne(s => s.Year)
                .WithMany()
                .HasForeignKey(s => s.YearId)
                .OnDelete(DeleteBehavior.Restrict); // Use Restrict to avoid cascade path conflicts

            modelBuilder.Entity<StudentsStudyInfo>()
                .HasOne(s => s.SubStage)
                .WithMany()
                .HasForeignKey(s => s.SubStageId)
                .OnDelete(DeleteBehavior.Restrict); // Use Restrict to avoid cascade path conflicts


            modelBuilder.Entity<TimeTable>()
        .HasOne(t => t.SubStage)
        .WithMany()
        .HasForeignKey(t => t.SubStageId)
        .OnDelete(DeleteBehavior.Cascade); // Keep cascade if this is intended

            modelBuilder.Entity<TimeTable>()
                .HasOne(t => t.Subject)
                .WithMany()
                .HasForeignKey(t => t.SubjectId)
                .OnDelete(DeleteBehavior.Restrict); // Use Restrict to prevent multiple cascade paths

            modelBuilder.Entity<TimeTable>()
                .HasOne(t => t.Teacher)
                .WithMany()
                .HasForeignKey(t => t.TeacherId)
                .OnDelete(DeleteBehavior.Restrict); // Use Restrict to prevent multiple cascade paths

            modelBuilder.Entity<TimeTable>()
                .HasOne(t => t.TimeTableGroupDetail)
                .WithMany()
                .HasForeignKey(t => t.TimTabGroDetId)
                .OnDelete(DeleteBehavior.Restrict); // Use Restrict to prevent multiple cascade paths

            


        }
    }


}
