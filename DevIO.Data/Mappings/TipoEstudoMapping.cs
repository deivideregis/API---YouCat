using DevIO.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevIO.Data.Mappings
{
    public class TipoEstudoMapping : IEntityTypeConfiguration<TipoEstudo>
    {
        public void Configure(EntityTypeBuilder<TipoEstudo> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Descricao)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.ToTable("TipoEstudo");
        }
    }
}
