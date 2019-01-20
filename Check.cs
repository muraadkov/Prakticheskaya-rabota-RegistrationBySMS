using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Twilio.TwiML;
namespace Registration
{

    public class Check
    {
        User user = new User
        {
            Login = "muratzhan",
            Password = "1123123aA&",
            DoublePassword = "1123123aA&",
            Email = "muratzhan@mail.ru",
            MobileNumber = "+77087553663"
        };
        public void ValidationCheck()
        {
            int lengthPassword = user.Password.Length;
            int minimalLengthOfPassword = 8;
            Console.WriteLine("Введите логин: ");
            user.Login = Console.ReadLine();
            try
            {
                if (user.Login.Contains(" ") | string.IsNullOrEmpty(user.Login))
                {
                    throw new ArgumentException();
                }
            }
            catch(ArgumentException)
            {
                Console.WriteLine("Логин не соответствует требованиям");
                throw new ArgumentException();
            }
            Console.WriteLine("Введите e-mail: ");
            user.Email = Console.ReadLine();
            try
            {
                if (!user.Email.Contains('@') | !user.Email.Contains('.'))
                {
                    throw new ArgumentException();
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine("E-mail не соответствует требованиям");
                throw new ArgumentException();
            }
            Console.WriteLine("Введите пароль: ");
            user.Password = ReadPassword();
            try
            {
                if (lengthPassword < minimalLengthOfPassword)
                {
                    throw new ArgumentException();
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Пароль не соответствует требованиям");
                throw new ArgumentException();
            }
            try
            {
                if (!user.Password.Contains('!') && !user.Password.Contains('$') && !user.Password.Contains('%') && !user.Password.Contains(':') && !user.Password.Contains('?') && !user.Password.Contains('&') && !user.Password.Contains('*') && !user.Password.Contains('(') && !user.Password.Contains(')') && !user.Password.Contains('+') && !user.Password.Contains('-') && !user.Password.Contains(']') && !user.Password.Contains('[') && !user.Password.Contains('\'') && !user.Password.Contains('/') && !user.Password.Contains('.') && !user.Password.Contains('?') && !user.Password.Contains('<') && !user.Password.Contains('>'))
                {
                    throw new ArgumentException();
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Пароль не соответствует требованиям");
                throw new ArgumentException();
            }
            try
            {
                if (!user.Password.Any(Char.IsNumber) || !user.Password.Any(Char.IsUpper) || !user.Password.Any(Char.IsLower))
                {
                    throw new ArgumentException();
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Пароль не соответствует требованиям");
                throw new ArgumentException();
            }
            Console.WriteLine("Повторите пароль: ");
            user.DoublePassword = ReadPassword();
            try {
            if (user.DoublePassword != user.Password)
            {
                throw new ArgumentException();
            }
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Пароли не совпадают");
                throw new ArgumentException();
            }
            Console.WriteLine("Введите номер телефона: ");
            user.MobileNumber = Console.ReadLine();
            try {
            if (string.IsNullOrEmpty(user.MobileNumber) || user.MobileNumber.Length > 12 || user.MobileNumber.Contains('-') || user.MobileNumber.Contains(' '))
            {
                throw new ArgumentException();
            }
            }
            catch(ArgumentException)
            {
                throw new ArgumentException("Мобильный телефон неверный");
                throw new ArgumentException();
            }

        }
        public static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {

                        password = password.Substring(0, password.Length - 1);
                        int pos = Console.CursorLeft;
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }
            Console.WriteLine();
            return password;
        }
        public void SendSms()
        {
            Random rand = new Random();
            var accountSid = "AC2ec5f1cd0280d496beef7f07a50a2b89";
            var authToken = "ec5f9a40c06da9049d54d4237ed90962";
            string code = Convert.ToString(rand.Next(1000, 9999));
            TwilioClient.Init(accountSid, authToken);
            var to = new PhoneNumber(user.MobileNumber);
            var from = new PhoneNumber("+17372270725");
            var message = MessageResource.Create(
                to: to,
                from: from,
                body: code);
            Console.WriteLine(message.Body);
            Console.WriteLine("Введите свой код: ");
            string checkCode = Console.ReadLine();
            if (checkCode == code)
            {
                Console.WriteLine("Регистрация прошла успешно!");
            }
            Console.Read();
        }
    }
}
