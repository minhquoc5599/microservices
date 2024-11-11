using Shared.Services.Email;

namespace Contract.Services
{
	public interface ISMTPEmailService : IEmailService<MailRequest>
	{
	}
}
