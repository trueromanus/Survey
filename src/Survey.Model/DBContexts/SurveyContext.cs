using Microsoft.EntityFrameworkCore;
using Survey.Model.Entities;

namespace Survey.Model
{
	public class SurveyContext : DbContext
	{
		public SurveyContext ( DbContextOptions options ) : base ( options ) {
		}

		protected override void OnModelCreating ( ModelBuilder modelBuilder ) {
			modelBuilder.Entity<AnswerQuestion> ()
				.HasKey ( bc => new { bc.AnswerId , bc.QuestionId } );
			modelBuilder.Entity<AnswerQuestion> ()
				.HasOne ( bc => bc.Answer )
				.WithMany ( b => b.AnswerQuestions )
				.HasForeignKey ( bc => bc.AnswerId );
			modelBuilder.Entity<AnswerQuestion> ()
				.HasOne ( bc => bc.Question )
				.WithMany ( c => c.AnswerQuestions )
				.HasForeignKey ( bc => bc.QuestionId );
		}

		public DbSet<Question> Questions { get; set; }

		public DbSet<Answer> Answers { get; set; }

		public DbSet<AnswerQuestion> AnswerQuestions { get; set; }

		public DbSet<Questionary> Questionaries { get; set; }

		public DbSet<QuestionOption> QuestionOptions { get; set; }

	}

}
