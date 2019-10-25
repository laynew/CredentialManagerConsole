using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using CredentialManagerConsole.PasswordChange;
using CredentialManagerConsole.WindowsCredentialManager;

namespace CredentialManagerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                PrintUsage();
                return;
            }

            var username = args[0];
            var password = args[1];

            var credentialStore = new CredentialStore();

            var passwordChanger = new PasswordChanger(credentialStore, credentialStore);
            passwordChanger.ChangePasswordForUsername(username, password);
        }

        private static void PrintUsage()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("cmcp <username> <password>");
        }
    }
}
