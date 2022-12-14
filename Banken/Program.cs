using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Banken
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // fixa pinkod
            // fixa uttag

            string userName = "";
            string passWord = "";

            //Alla kontons sparade saldo
            //Ändra till Tuple?
            List<float> zeusBalance = new List<float> { 1352f, 2434.1f };
            List<float> aphroditeBalance = new List<float> { 1827.02f };
            List<float> poseidonBalance = new List<float> { 1072.7f };
            List<float> hermesBalance = new List<float> { 876.6f, 4037.01f };
            List<float> athenaBalance = new List<float> { 4102.08f };

            //Alla kontons individuella kontonamn
            List<string> zeusAccounts = new List<string> { "Main account", "Savings account" };
            List<string> aphroditeAccounts = new List<string> { "Main account" };
            List<string> poseidonAccounts = new List<string> { "Stock savings account" };
            List<string> hermesAccounts = new List<string> { "Main account", "Trading account" };
            List<string> athenaAccounts = new List<string> { "Market account" };

            //Valuta
            string currency = " SEK ";

            //Kollar om man skrivit in rätt kontonamn
            bool wrongAccount = true;

            //Lagrar alla konton
            string[] defaultAccounts = new string[]
            {
                "Zeus",
                "Aphrodite",
                "Poseidon",
                "Hermes",
                "Athena"
            };

            //Sparar lösenord
            List<string> savedPasswords = new List<string>
            {
                "",
                "",
                "",
                "",
                ""
            };

            selectUser(userName, passWord);
            mainMenu();




            //Visar registrerade konton och inloggningsalternativ
            Tuple<string, string> selectUser(string username, string password)
            {
                Console.WriteLine("Welcome to Chacademy bank services!\n");
                Console.WriteLine("Registered accounts: \n");
                foreach (string user in defaultAccounts)
                {
                    Console.WriteLine(user);
                }
                Console.Write("\nFrom the list above, please enter your desired account name: ");
                userName = Console.ReadLine();
                while (wrongAccount == true)
                {
                    //Kollar om man skrivit in giltigt kontonamn
                    if (!string.Equals(userName, defaultAccounts[0], StringComparison.CurrentCultureIgnoreCase)
                        && !string.Equals(userName, defaultAccounts[1], StringComparison.CurrentCultureIgnoreCase)
                        && !string.Equals(userName, defaultAccounts[2], StringComparison.CurrentCultureIgnoreCase)
                        && !string.Equals(userName, defaultAccounts[3], StringComparison.CurrentCultureIgnoreCase)
                        && !string.Equals(userName, defaultAccounts[4], StringComparison.CurrentCultureIgnoreCase))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("\nSorry, account \"" + userName + "\" cannot be found.");
                        Console.ResetColor();
                        Console.Write("\nPlease enter an account name from the list above: ");
                        userName = Console.ReadLine();
                    }
                    else
                    {
                        //Återställer case insensitive input till det ursprungliga kontonamnet
                        if (string.Equals(userName, defaultAccounts[0], StringComparison.CurrentCultureIgnoreCase))
                        {
                            userName = "Zeus";
                        }
                        else if (string.Equals(userName, defaultAccounts[1], StringComparison.CurrentCultureIgnoreCase))
                        {
                            userName = "Aphrodite";
                        }
                        else if (string.Equals(userName, defaultAccounts[2], StringComparison.CurrentCultureIgnoreCase))
                        {
                            userName = "Poseidon";
                        }
                        else if (string.Equals(userName, defaultAccounts[3], StringComparison.CurrentCultureIgnoreCase))
                        {
                            userName = "Hermes";
                        }
                        else if (string.Equals(userName, defaultAccounts[4], StringComparison.CurrentCultureIgnoreCase))
                        {
                            userName = "Athena";
                        }

                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write("\nAccount name ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("\"" + userName + "\"");
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write(" found.");
                        Console.ResetColor();
                        Console.Write("\nPlease set your new PIN code for this account: ");
                        passWord = Console.ReadLine();

                        //Sparar lösenordet från input i en lista
                        if (userName == "Zeus")
                        {
                            savedPasswords[0] = passWord;
                        }
                        else if (userName == "Aphrodite")
                        {
                            savedPasswords[1] = passWord;
                        }
                        else if (userName == "Poseidon")
                        {
                            savedPasswords[2] = passWord;
                        }
                        else if (userName == "Hermes")
                        {
                            savedPasswords[3] = passWord;
                        }
                        else if (userName == "Athena")
                        {
                            savedPasswords[4] = passWord;
                        }
                        wrongAccount = false;
                    }
                }
                return new Tuple<string, string>(userName, passWord);
            }

            void mainMenu()
            {
                Console.Clear();

                //Menyval
                List<string> _mainMenu = new List<string>()
                {
                    "1. Your accounts and account balance",
                    "2. Transactions",
                    "3. Withdrawal",
                    "4. Return to login page",
                    "5. Exit program"
                };

                //Avgör vilket menyval man är på
                bool[] choices = { true, false, false, false, false };

                //Räknare
                int x = 0;

                //Loop körs för att behålla menyn på skärmen
                bool showMenu = true;
                while (showMenu)
                {
                    Console.Write("You are currently logged in as: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(userName + "\n");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("\nYour PIN code for this account is: " + passWord + ".");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write(" Don't forget your password.\n\n");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("- MAIN MENU -");
                    Console.ResetColor();
                    Console.WriteLine("\n(You can navigate through the menu with the 'up' and 'down' arrow keys): \n");

                    //Booleans bestämmer vilket menyval som är markerat
                    if (choices[0] == true)
                    {
                        Console.WriteLine("[ " + _mainMenu[0] + " ]");
                    }
                    else if (choices[0] == false)
                    {
                        Console.WriteLine(" " + " " + _mainMenu[0]);
                    }
                    if (choices[1] == true)
                    {
                        Console.WriteLine("[ " + _mainMenu[1] + " ]");
                    }
                    else if (choices[1] == false)
                    {
                        Console.WriteLine(" " + " " + _mainMenu[1]);
                    }
                    if (choices[2] == true)
                    {
                        Console.WriteLine("[ " + _mainMenu[2] + " ]");
                    }
                    else if (choices[2] == false)
                    {
                        Console.WriteLine(" " + " " + _mainMenu[2]);
                    }
                    if (choices[3] == true)
                    {
                        Console.WriteLine("\n[ " + _mainMenu[3] + " ]");
                    }
                    else if (choices[3] == false)
                    {
                        Console.WriteLine("\n " + " " + _mainMenu[3]);
                    }
                    if (choices[4] == true)
                    {
                        Console.WriteLine("\n[ " + _mainMenu[4] + " ]");
                    }
                    else if (choices[4] == false)
                    {
                        Console.WriteLine("\n " + " " + _mainMenu[4]);
                    }

                    ConsoleKeyInfo key = Console.ReadKey();

                    //Navigering med 'upp' och 'ned' tangenter
                    if (key.Key == ConsoleKey.DownArrow)
                    {
                        if (x == 4)
                        {
                            choices[0] = true;
                            choices[x] = false;
                            x = 0;
                        }
                        else
                        {
                            choices[x + 1] = true;
                            choices[x] = false;
                            x++;
                        }

                    }
                    else if (key.Key == ConsoleKey.UpArrow)
                    {
                        if (x == 0)
                        {
                            choices[4] = true;
                            choices[x] = false;
                            x = 4;
                        }
                        else
                        {
                            choices[x - 1] = true;
                            choices[x] = false;
                            x--;
                        }

                    }

                    //Välj menyval med 'enter'
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        if (x == 0)
                        {
                            acountBalance(userName);
                            Console.WriteLine("\n" + acountBalance(userName));
                            Console.WriteLine("\n\n(Press any key to return to the menu...)");
                            Console.ReadKey();
                        }
                        else if (x == 1)
                        {
                            transactions(userName);
                            Console.ReadKey();
                        }
                        else if (x == 2)
                        {
                            withdraw(userName);
                            Console.ReadKey();
                        }
                        else if (x == 3)
                        {
                            wrongAccount = true;
                            Console.Clear();
                            selectUser(userName, passWord);
                        }
                        else if (x == 4)
                        {
                            exitProgram();
                        }
                    }
                    Console.Clear();
                }

                //Visar saldo
                string acountBalance(string username)
                {
                    if (username == "Zeus")
                    {
                        return zeusAccounts[0] + $" balance: {zeusBalance.ElementAt(0) + currency}\n\n"
                            + zeusAccounts[1] + $" balance: {zeusBalance.ElementAt(1) + currency}";
                    }
                    else if (username == "Aphrodite")
                    {
                        return aphroditeAccounts[0] + $" balance: {aphroditeBalance.ElementAt(0) + currency}";
                    }
                    else if (username == "Poseidon")
                    {
                        return poseidonAccounts[0] + $" balance: {poseidonBalance.ElementAt(0) + currency}\n\n"
                            + poseidonAccounts[1] + $" balance: {poseidonBalance.ElementAt(1) + currency}";
                    }
                    else if (username == "Hermes")
                    {
                        return hermesAccounts[0] + $" balance: {hermesBalance.ElementAt(0) + currency}\n\n"
                                + hermesAccounts[1] + $" balance: {hermesBalance.ElementAt(1) + currency}";
                    }
                    else if (username == "Athena")
                    {
                        return athenaAccounts[0] + $" balance: {athenaBalance.ElementAt(0) + currency}";
                    }
                    else
                    {
                        return "Error, account not found.";
                    }
                }

                //Överföring mellan konton
                void transactions(string username)
                {
                    Console.Clear();

                    Dictionary<string, float> chosenAccounts = new Dictionary<string, float>();

                    if (username == "Zeus")
                    {
                        chosenAccounts.Add(zeusAccounts[0], zeusBalance[0]);
                        chosenAccounts.Add(zeusAccounts[1], zeusBalance[1]);
                    }
                    else if (username == "Aphrodite")
                    {
                        Console.WriteLine("Apologies.\nSince you only have 1 registered account, transaction is therefore currently unavailable.\nPress any key to return to the main menu...");
                        Console.ReadKey();
                        mainMenu();
                    }
                    else if (username == "Poseidon")
                    {
                        chosenAccounts.Add(poseidonAccounts[0], poseidonBalance[0]);
                        chosenAccounts.Add(poseidonAccounts[1], poseidonBalance[1]);
                    }
                    else if (username == "Hermes")
                    {
                        chosenAccounts.Add(hermesAccounts[0], hermesBalance[0]);
                        chosenAccounts.Add(hermesAccounts[1], hermesBalance[1]);
                    }
                    else if (username == "Athena")
                    {
                        Console.WriteLine("Apologies.\nSince you only have 1 registered account, transaction is therefore currently unavailable.\nPress any key to return to the main menu...");
                        Console.ReadKey();
                        mainMenu();
                    }

                    //Visar alla individuella konton och frågar användaren vilket konto man ska överföra från
                    bool tMenu = true;
                    while (tMenu)
                    {
                        Console.WriteLine("Your accounts: \n");

                        foreach (var accountInfo in chosenAccounts)
                        {
                            Console.WriteLine("{0} balance: {1}", accountInfo.Key, accountInfo.Value);
                        }
                        transferFrom(chosenAccounts, username);
                        break;
                    }
                }

                //Kollar om man skrivit in rätt konto att överföra FRÅN
                void transferFrom(Dictionary<string, float> chosenaccounts, string username)
                {
                    Console.Write("\nWhat account would you like to transfer from?: ");
                    string fromAccount = Console.ReadLine();
                    bool tIncorrect = true;
                    while (tIncorrect)
                    {
                        //Kollar om man skrivit in rätt kontonamn att överföra FRÅN första kontot
                        if (string.Equals(fromAccount, chosenaccounts.ElementAt(0).Key, StringComparison.CurrentCultureIgnoreCase))
                        {
                            //Återställer input till rättstavat namn
                            fromAccount = chosenaccounts.ElementAt(0).Key;
                            tIncorrect = false;
                            break;
                        }
                        else if (string.Equals(fromAccount, chosenaccounts.ElementAt(1).Key, StringComparison.CurrentCultureIgnoreCase))
                        {
                            fromAccount = chosenaccounts.ElementAt(1).Key;
                            tIncorrect = false;
                            break;
                        }
                        else
                        {
                            Console.Write("Sorry, please enter one of your registered accounts to transfer from: ");
                            fromAccount = Console.ReadLine();
                        }
                    }
                    Console.WriteLine("\nYou selected transfer from: " + fromAccount + "\n");
                    transferTo(fromAccount, chosenaccounts, username);
                }

                //Kollar om man skrivit in rätt kontonamn att överföra TILL
                void transferTo(string fromaccount, Dictionary<string, float> chosenaccounts, string username)
                {
                    bool fromFirst;
                    Console.Write("To which account?: ");
                    string toAccount = Console.ReadLine();

                    bool tIncorrect = true;
                    while (tIncorrect)
                    {
                        if (string.Equals(toAccount, fromaccount, StringComparison.CurrentCultureIgnoreCase))
                        {
                            Console.WriteLine("\nSorry, you can't transfer money from an account to itself.");
                            Console.Write("Please try again: ");
                            toAccount = Console.ReadLine();
                        }
                        else
                        {
                            //Kollar om man skrivit in rätt kontonamn att överföra TILL andra kontot
                            if (string.Equals(toAccount, chosenaccounts.ElementAt(0).Key, StringComparison.CurrentCultureIgnoreCase))
                            {
                                fromFirst = false;
                                tIncorrect = false;
                                transfer(toAccount, fromaccount, chosenaccounts, username, fromFirst);
                                break;
                            }
                            else if (string.Equals(toAccount, chosenaccounts.ElementAt(1).Key, StringComparison.CurrentCultureIgnoreCase))
                            {
                                fromFirst = true;
                                tIncorrect = false;
                                transfer(toAccount, fromaccount, chosenaccounts, username, fromFirst);
                                break;
                            }
                            else
                            {
                                Console.Write("Sorry, please enter one of your registered accounts for transfer: ");
                                toAccount = Console.ReadLine();
                            }
                        }
                       
                    }  
                }

                void transfer(string toaccount, string fromaccount, Dictionary<string, float> chosenaccounts, string username, bool fromfirst)
                {
                    float fromBalance = 0;
                    float toBalance = 0;

                    if (fromfirst)
                    {
                        fromBalance = chosenaccounts.ElementAt(0).Value;
                        toBalance = chosenaccounts.ElementAt(1).Value;
                    }
                    else if (!fromfirst)
                    {
                        fromBalance = chosenaccounts.ElementAt(1).Value;
                        toBalance = chosenaccounts.ElementAt(0).Value;
                    }

                    Console.WriteLine("\nYou chose to transfer from:\n{0} (balance: {1}) to {2} (balance: {3})\n", fromaccount, fromBalance, toaccount, toBalance);
                    Console.Write("\nEnter amount to transfer: ");
                    string transferAmount = Console.ReadLine();
                    bool notNumber = true;
                    while (notNumber)
                    {
                        if (float.TryParse(transferAmount, out float amount))
                        {
                            if (amount > fromBalance)
                            {
                                Console.WriteLine("Sorry you don't have enough money on this account\n");
                                Console.Write("Try again: ");
                                transferAmount = Console.ReadLine();
                            }
                            else
                            {
                                chosenaccounts[fromaccount] -= amount;
                                chosenaccounts[toaccount] += amount;

                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\nTransfer complete.\n");
                                Console.ResetColor();
                                Console.WriteLine("Your new account balances are:\n\n{0}: {1}\nand\n{2}: {3}\n", chosenaccounts.ElementAt(0).Key, chosenaccounts.ElementAt(0).Value, chosenaccounts.ElementAt(1).Key, chosenaccounts.ElementAt(1).Value);
                                Console.WriteLine("Press any key to return to the main menu.");
                                if (username == "Zeus")
                                {
                                    zeusBalance[0] = chosenaccounts.ElementAt(0).Value;
                                    zeusBalance[1] = chosenaccounts.ElementAt(1).Value;
                                }
                                else if (username == "Poseidon")
                                {
                                    poseidonBalance[0] = chosenaccounts.ElementAt(0).Value;
                                    poseidonBalance[1] = chosenaccounts.ElementAt(1).Value;
                                }
                                else if (username == "Hermes")
                                {
                                    hermesBalance[0] = chosenaccounts.ElementAt(0).Value;
                                    hermesBalance[1] = chosenaccounts.ElementAt(1).Value;
                                }
                                notNumber = false;
                                break;
                            }
                        }
                        else
                        {
                            Console.Write("Sorry, please enter amunt to transfer: ");
                            transferAmount = Console.ReadLine();
                        }
                    }
                    chosenaccounts.Clear();
                }

                //Uttag
                void withdraw(string username)
                {
                    Console.Clear();

                    Dictionary<string, float> chosenAccounts = new Dictionary<string, float>();

                    if (username == "Zeus")
                    {
                        chosenAccounts.Add(zeusAccounts[0], zeusBalance[0]);
                        chosenAccounts.Add(zeusAccounts[1], zeusBalance[1]);
                    }
                    else if (username == "Aphrodite")
                    {
                        chosenAccounts.Add(athenaAccounts[0], athenaBalance[0]);
                    }
                    else if (username == "Poseidon")
                    {
                        chosenAccounts.Add(poseidonAccounts[0], poseidonBalance[0]);
                        chosenAccounts.Add(poseidonAccounts[1], poseidonBalance[1]);
                    }
                    else if (username == "Hermes")
                    {
                        chosenAccounts.Add(hermesAccounts[0], hermesBalance[0]);
                        chosenAccounts.Add(hermesAccounts[1], hermesBalance[1]);
                    }
                    else if (username == "Athena")
                    {
                        chosenAccounts.Add(athenaAccounts[0], athenaBalance[0]);
                    }

                    //Visar alla individuella konton och frågar användaren vilket konto man ska överföra från
                    bool tMenu = true;
                    while (tMenu)
                    {
                        Console.WriteLine("Your accounts: \n");

                        foreach (var accountInfo in chosenAccounts)
                        {
                            Console.WriteLine("{0} balance: {1}", accountInfo.Key, accountInfo.Value);
                        }
                        withdrawFrom(chosenAccounts, username);
                        break;
                    }
                }

                //Kollar om man skrivit in rätt konto att överföra FRÅN
                void withdrawFrom(Dictionary<string, float> chosenaccounts, string username)
                {
                    Console.Write("\nWhat account would you like to deposit from?: ");
                    string fromAccount = Console.ReadLine();
                    bool fromFirst = true;
                    bool tIncorrect = true;
                    while (tIncorrect)
                    {
                        //Kollar om man skrivit in rätt kontonamn att överföra FRÅN första kontot
                        if (string.Equals(fromAccount, chosenaccounts.ElementAt(0).Key, StringComparison.CurrentCultureIgnoreCase))
                        {
                            //Återställer input till rättstavat namn
                            fromAccount = chosenaccounts.ElementAt(0).Key;
                            tIncorrect = false;
                            break;
                        }
                        else if (string.Equals(fromAccount, chosenaccounts.ElementAt(1).Key, StringComparison.CurrentCultureIgnoreCase))
                        {
                            fromAccount = chosenaccounts.ElementAt(1).Key;
                            fromFirst = false;
                            tIncorrect = false;
                            break;
                        }
                        else
                        {
                            Console.Write("Sorry, please enter one of your registered accounts to transfer from: ");
                            fromAccount = Console.ReadLine();
                        }
                    }
                    Console.WriteLine("\nYou selected withdrawal from: " + fromAccount + "\n");
                    withdrawal(fromAccount, chosenaccounts, username, fromFirst);
                }

                void withdrawal(string fromaccount, Dictionary<string, float> chosenaccounts, string username, bool fromfirst)
                {
                    float fromBalance = 0;
                    string chosenAccount = "";

                    if (fromfirst)
                    {
                        fromBalance = chosenaccounts.ElementAt(0).Value;
                        chosenAccount = chosenaccounts.ElementAt(0).Key;
                    }
                    else if (!fromfirst)
                    {
                        fromBalance = chosenaccounts.ElementAt(1).Value;
                        chosenAccount = chosenaccounts.ElementAt(1).Key;
                    }

                    Console.Write("\nEnter amount to transfer: ");
                    string transferAmount = Console.ReadLine();
                    bool notNumber = true;
                    while (notNumber)
                    {
                        if (float.TryParse(transferAmount, out float amount))
                        {
                            if (amount > fromBalance)
                            {
                                Console.WriteLine("Sorry you don't have enough money on this account\n");
                                Console.Write("Try again: ");
                                transferAmount = Console.ReadLine();
                            }
                            else
                            {
                                chosenaccounts[fromaccount] -= amount;

                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\nWithdrawal complete.\n");
                                Console.ResetColor();
                                Console.WriteLine("Your new account balance is:\n\n{0}: {1}\n", chosenAccount, chosenaccounts[fromaccount]);
                                Console.WriteLine("Press any key to return to the main menu.");
                                if (username == "Zeus")
                                {
                                    if (fromfirst)
                                    {
                                        zeusBalance[0] = chosenaccounts.ElementAt(0).Value;
                                    }
                                    else if (!fromfirst)
                                    {
                                        zeusBalance[1] = chosenaccounts.ElementAt(1).Value;
                                    }
                                }
                                else if (username == "Poseidon")
                                {
                                    poseidonBalance[0] = chosenaccounts.ElementAt(0).Value;
                                    poseidonBalance[1] = chosenaccounts.ElementAt(1).Value;
                                }
                                else if (username == "Hermes")
                                {
                                    hermesBalance[0] = chosenaccounts.ElementAt(0).Value;
                                    hermesBalance[1] = chosenaccounts.ElementAt(1).Value;
                                }
                                notNumber = false;
                                break;
                            }
                        }
                        else
                        {
                            Console.Write("Sorry, please enter amount to transfer: ");
                            transferAmount = Console.ReadLine();
                        }
                    }
                    chosenaccounts.Clear();
                }

                //Stäng programmet
                void exitProgram()
                {
                    Console.WriteLine("\nAre you sure?\n");
                    Console.Write("Enter 'yes' to confirm, or 'no' to cancel: ");
                    string exit = Console.ReadLine();
                    if (exit == "yes")
                    {
                        Console.WriteLine("\nExiting program in: ");
                        Console.WriteLine("3...");
                        Thread.Sleep(1000);
                        Console.WriteLine("2...");
                        Thread.Sleep(1000);
                        Console.WriteLine("1...");
                        Thread.Sleep(1000);
                        Environment.Exit(0);
                    }
                    else
                    {
                        return;
                    }
                }

            }
        }
    }
}
