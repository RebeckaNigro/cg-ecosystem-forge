//using Ecossistema.Domain.Entities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Ecossistema.Data.Mapping
//{
//    public class PermissaoConfiguration : IEntityTypeConfiguration<Permissao>
//    {
//        public void Configure(EntityTypeBuilder<Permissao> builder)
//        {
//            builder.ToTable("Permissao", "dbo")
//                     .HasKey(x => x.Id);

//            builder.HasOne(x => x.UsuarioCriacao)
//                .WithMany(x => x.UsuariosCriacoesPermissoes)
//                .HasForeignKey(y => y.UsuarioCriacaoId);

//            builder.HasOne(x => x.UsuarioOperacao)
//                .WithMany(x => x.UsuariosOperacoesPermissoes)
//                .HasForeignKey(y => y.UsuarioOperacaoId);
//        }
//    }
//}