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
    public class PessoaEnderecoConfiguration : IEntityTypeConfiguration<PessoaEndereco>
    {
        public void Configure(EntityTypeBuilder<PessoaEndereco> builder)
        {
            builder.ToTable("PessoaEndereco", "dbo")
                     .HasKey(x => x.Id);

            builder.HasOne(x => x.Pessoa)
                .WithMany(x => x.PessoasEnderecos)
                .HasForeignKey(y => y.PessoaId);

            builder.HasOne(x => x.Endereco)
                .WithMany(x => x.PessoasEnderecos)
                .HasForeignKey(y => y.EnderecoId);

            builder.HasOne(x => x.TipoEndereco)
                .WithMany(x => x.PessoasEnderecos)
                .HasForeignKey(y => y.TipoEnderecoId);

            builder.HasOne(x => x.UsuarioCriacao)
                .WithMany(x => x.UsuariosCriacoesPessoasEnderecos)
                .HasForeignKey(y => y.UsuarioCriacaoId);

            builder.HasOne(x => x.UsuarioOperacao)
                .WithMany(x => x.UsuariosOperacoesPessoasEnderecos)
                .HasForeignKey(y => y.UsuarioOperacaoId);
        }
    }
}