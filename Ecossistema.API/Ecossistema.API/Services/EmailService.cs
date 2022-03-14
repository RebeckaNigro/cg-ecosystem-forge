using Ecossistema.API.Models.DTO;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Ecossistema.API.Services
{
    public class EmailService : IEmailService
    {
        private readonly ConfiguracaoEmail configEmail;

        public EmailService(ConfiguracaoEmail config)
        {
            configEmail = config;
        }

        public async Task EnviarEmail(Mensagem mensagem)
        {
            if (configEmail == null)
                return;

            var mensagemEmail = CriaMensagemEmail(mensagem);

            await Envia(mensagemEmail);
        }

        private MimeMessage CriaMensagemEmail(Mensagem mensagem)
        {
            var mensagemEmail = new MimeMessage();
            mensagemEmail.From.Add(new MailboxAddress(configEmail.Name, configEmail.From));
            mensagemEmail.To.AddRange(mensagem.Para);
            mensagemEmail.Subject = mensagem.Assunto;

            mensagemEmail.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = mensagem.Conteudo };

            return mensagemEmail;
        }

        private async Task Envia(MimeMessage mensagemEmail)
        {
            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(configEmail.SmtpServer, configEmail.Port, SecureSocketOptions.StartTls);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.AuthenticateAsync(configEmail.UserName, configEmail.Password);

                await client.SendAsync(mensagemEmail);
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                await client.DisconnectAsync(true);
                client.Dispose();
            }
        }
    }
}
