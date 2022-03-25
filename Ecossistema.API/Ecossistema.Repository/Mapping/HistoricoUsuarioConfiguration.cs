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
    public class HistoricoUsuarioConfiguration : IEntityTypeConfiguration<HistoricoUsuario>
    {
        public void Configure(EntityTypeBuilder<HistoricoUsuario> builder)
        {
            builder.ToTable("HistoricoUsuario", "dbo")
                     .HasKey(x => x.Id);

            builder.HasOne(x => x.Usuario)
                .WithMany(x => x.HistoricoUsuarios)
                .HasForeignKey(y => y.UsuarioId);

            builder.HasOne(x => x.Pessoa)
                .WithMany(x => x.HistoricoUsuarios)
                .HasForeignKey(y => y.PessoaId);

            builder.HasOne(x => x.Instituicao)
                .WithMany(x => x.HistoricoUsuarios)
                .HasForeignKey(y => y.InstituicaoId);

            builder.HasOne(x => x.Permissao)
                .WithMany(x => x.HistoricoUsuarios)
                .HasForeignKey(y => y.PermissaoId);

            builder.HasOne(x => x.Aprovacao)
                .WithMany(x => x.HistoricoUsuarios)
                .HasForeignKey(y => y.AprovacaoId);

            builder.HasOne(x => x.UsuarioCriacao)
                .WithMany(x => x.UsuariosCriacoesHistoricoUsuarios)
                .HasForeignKey(y => y.UsuarioCriacaoId);

            builder.HasOne(x => x.UsuarioOperacao)
                .WithMany(x => x.UsuariosOperacoesHistoricoUsuarios)
                .HasForeignKey(y => y.UsuarioOperacaoId);
        }
    }
}