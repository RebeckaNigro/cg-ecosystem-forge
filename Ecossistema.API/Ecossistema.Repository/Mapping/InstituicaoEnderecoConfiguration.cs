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
    public class InstituicaoEnderecoConfiguration : IEntityTypeConfiguration<InstituicaoEndereco>
    {
        public void Configure(EntityTypeBuilder<InstituicaoEndereco> builder)
        {
            builder.ToTable("InstituicaoEndereco", "dbo")
                     .HasKey(x => x.Id);

            builder.HasOne(x => x.Instituicao)
                .WithMany(x => x.InstituicoesEnderecos)
                .HasForeignKey(y => y.InstituicaoId);

            builder.HasOne(x => x.Endereco)
                .WithMany(x => x.InstituicoesEnderecos)
                .HasForeignKey(y => y.EnderecoId);

            builder.HasOne(x => x.TipoEndereco)
                .WithMany(x => x.InstituicoesEnderecos)
                .HasForeignKey(y => y.TipoEnderecoId);

            builder.HasOne(x => x.UsuarioCriacao)
                .WithMany(x => x.UsuariosCriacoesInstituicoesEnderecos)
                .HasForeignKey(y => y.UsuarioCriacaoId);

            builder.HasOne(x => x.UsuarioOperacao)
                .WithMany(x => x.UsuariosOperacoesInstituicoesEnderecos)
                .HasForeignKey(y => y.UsuarioOperacaoId);
        }
    }
}
