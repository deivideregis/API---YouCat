using DevIO.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevIO.Data.Mappings
{
    public class EstudosMapping : IEntityTypeConfiguration<Estudos>
    {
        public void Configure(EntityTypeBuilder<Estudos> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.NumeroYouCat)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(p => p.Descricao)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(p => p.Pergunta)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(p => p.Resposta)
                 .HasColumnType("varchar(800)");

            builder.Property(p => p.Explicacao)
                .HasColumnType("varchar(800)");

            builder.Property(p => p.DataCadastro)
                .IsRequired()
                .HasColumnType("datetime");

            //// 1 : 1 => Estudos : TipoEstudo
            //builder.HasOne(f => f.Estudos)
            //    .WithOne(e => e.TipoEstudo);

            // 1 : N => Estudos: TipoEstudo
            builder.HasMany(f => f.TipoEstudo)
                .WithOne(p => p.Estudos)
                .HasForeignKey(p => p.EstudoId);

            builder.ToTable("Estudos");
        }
    }
}
