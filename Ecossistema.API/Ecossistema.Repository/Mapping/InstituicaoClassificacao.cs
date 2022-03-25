using Ecossistema.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Data.Mapping
{
    public class InstituicaoClassificacaoConfiguration : IEntityTypeConfiguration<InstituicaoClassificacao>
    {
        public void Configure(EntityTypeBuilder<InstituicaoClassificacao> builder)
        {
            builder.ToTable("InstituicaoClassificacao", "dbo")
                .HasKey(x => x.Id);

            builder.HasOne(x => x.UsuarioCriacao)
                .WithMany(x => x.UsuariosCriacoesInstituicoesClassificacoes)
                .HasForeignKey(y => y.UsuarioCriacaoId);

            builder.HasOne(x => x.UsuarioOperacao)
                .WithMany(x => x.UsuariosOperacoesInstituicoesClassificacoes)
                .HasForeignKey(y => y.UsuarioOperacaoId);
        }
    }
}