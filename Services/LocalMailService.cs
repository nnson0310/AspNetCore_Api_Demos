namespace AspCoreWebAPIDemos.Services
{
    public class LocalMailService : IMailService
    {
        private const string _fromEmail = "admin@gmail.com";
        private const string _toEmail = "guest@gmail.com";

        public void Send(string subject, string content)
        {
            Console.WriteLine($"Mail from {_fromEmail} to {_toEmail} with {nameof(LocalMailService)}");
            Console.WriteLine($"{subject}");
            Console.WriteLine($"{content}");
        }
    }
}
