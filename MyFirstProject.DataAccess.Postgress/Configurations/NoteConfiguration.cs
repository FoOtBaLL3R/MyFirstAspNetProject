using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFirstProject.Core.Models;
using MyFirstProject.DataAccess.Postgress.Entities;

namespace MyFirstProject.DataAccess.Postgress.Configurations
{
    public class NoteConfiguration : IEntityTypeConfiguration<NoteEntity>
    {
        public void Configure(EntityTypeBuilder<NoteEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(n => n.Name)
                .HasMaxLength(Note.MAX_NAME_LENGTH)
                .IsRequired();

            builder.Property(n => n.Description)
                .HasMaxLength(Note.MAX_DESC_LENGTH)
                .IsRequired();

            builder.Property(n => n.CreatedAt)
                .IsRequired();
        }
    }
}
