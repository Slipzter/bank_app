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
            string userName = "";
            string passWord = "";

            //Alla kontons namn och saldo
            var zeusAccounts = new Dictionary<string, (int crowns, int pennies)>
            {
                { "Main account", (1352, 52) },
                { "Savings account", (2434, 89) }
            };
            var aphroditeAccounts = new Dictionary<string, (int crowns, int pennies)>
            {
                { "Main account", (1827, 02) },
                { "Savings account", (276, 82) },
                { "Credit account", (2763, 23) }
            };
            var poseidonAccounts = new Dictionary<string, (int crowns, int pennies)>
            {
                { "Main account", (1072, 70) },
                { "Stock savings account", (1723, 87) },
                { "Salary account", (263, 29) },
                { "Investments account", (11726, 76) }
            };
            var hermesAccounts = new Dictionary<string, (int crowns, int pennies)>
            {
                { "Main account", (876, 60) },
                { "Trading account", (4037, 67) },
                { "Gambling account", (109, 87) },
                { "Wall Street account", (29, 98) },
                { "Credit account", (38, 56) }
            };
            var athenaAccounts = new Dictionary<string, (int crowns, int pennies)>
            {
                { "Main account", (4102, 80) },
                { "Market account", (22293, 80) },
                { "Shared account", (12, 20) },
                { "Grocery account", (1, 87) },
                { "Real estate account", (676, 87) },
                { "Assets account", (287, 90) }
            };

            //Valuta
            string currency = "Kr";
            string currency2 = "Ören";

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

            //Sparar lösenord (oviktig)
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
                            //withdraw(userName);
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
                        return zeusAccounts.ElementAt(0).Key + $" balance: {zeusAccounts["Main account"].crowns + " " + currency + " " + zeusAccounts["Main account"].pennies + " " + currency2}\n\n"
                            + zeusAccounts.ElementAt(1).Key + $" balance: {zeusAccounts["Savings account"].crowns + " " + currency + " " + zeusAccounts["Savings account"].pennies + " " + currency2}";
                    }
                    else if (username == "Aphrodite")
                    {
                        return aphroditeAccounts.ElementAt(0).Key + $" balance: {aphroditeAccounts["Main account"].crowns + " " + currency + " " + aphroditeAccounts["Main account"].pennies + " " + currency2}\n\n"
                            + aphroditeAccounts.ElementAt(1).Key + $" balance: {aphroditeAccounts["Savings account"].crowns + " " + currency + " " + aphroditeAccounts["Savings account"].pennies + " " + currency2}\n\n"
                            + aphroditeAccounts.ElementAt(2).Key + $" balance: {aphroditeAccounts["Credit account"].crowns + " " + currency + " " + aphroditeAccounts["Credit account"].pennies + " " + currency2}";
                    }
                    else if (username == "Poseidon")
                    {
                        return poseidonAccounts.ElementAt(0).Key + $" balance: {poseidonAccounts["Main account"].crowns + " " + currency + " " + poseidonAccounts["Main account"].pennies + " " + currency2}\n\n"
                            + poseidonAccounts.ElementAt(1).Key + $" balance: {poseidonAccounts["Stock savings account"].crowns + " " + currency + " " + poseidonAccounts["Stock savings account"].pennies + " " + currency2}\n\n"
                            + poseidonAccounts.ElementAt(2).Key + $" balance: {poseidonAccounts["Salary account"].crowns + " " + currency + " " + poseidonAccounts["Salary account"].pennies + " " + currency2}\n\n"
                            + poseidonAccounts.ElementAt(3).Key + $" balance: {poseidonAccounts["Investments account"].crowns + " " + currency + " " + poseidonAccounts["Investments account"].pennies + " " + currency2}";
                    }
                    else if (username == "Hermes")
                    {
                        return hermesAccounts.ElementAt(0).Key + $" balance: {hermesAccounts["Main account"].crowns + " " + currency + " " + hermesAccounts["Main account"].pennies + " " + currency2}\n\n"
                            + hermesAccounts.ElementAt(1).Key + $" balance: {hermesAccounts["Trading account"].crowns + " " + currency + " " + hermesAccounts["Trading account"].pennies + " " + currency2}\n\n"
                            + hermesAccounts.ElementAt(2).Key + $" balance: {hermesAccounts["Gambling account"].crowns + " " + currency + " " + hermesAccounts["Gambling account"].pennies + " " + currency2}\n\n"
                            + hermesAccounts.ElementAt(3).Key + $" balance: {hermesAccounts["Wall Street account"].crowns + " " + currency + " " + hermesAccounts["Wall Street account"].pennies + " " + currency2}\n\n"
                            + hermesAccounts.ElementAt(4).Key + $" balance: {hermesAccounts["Credit account"].crowns + " " + currency + " " + hermesAccounts["Credit account"].pennies + " " + currency2}";
                    }
                    else if (username == "Athena")
                    {
                        return athenaAccounts.ElementAt(0).Key + $" balance: {athenaAccounts["Main account"].crowns + " " + currency + " " + athenaAccounts["Main account"].pennies + " " + currency2}\n\n"
                            + athenaAccounts.ElementAt(1).Key + $" balance: {athenaAccounts["Market account"].crowns + " " + currency + " " + athenaAccounts["Market account"].pennies + " " + currency2}\n\n"
                            + athenaAccounts.ElementAt(2).Key + $" balance: {athenaAccounts["Shared account"].crowns + " " + currency + " " + athenaAccounts["Shared account"].pennies + " " + currency2}\n\n"
                            + athenaAccounts.ElementAt(3).Key + $" balance: {athenaAccounts["Grocery account"].crowns + " " + currency + " " + athenaAccounts["Grocery account"].pennies + " " + currency2}\n\n"
                            + athenaAccounts.ElementAt(4).Key + $" balance: {athenaAccounts["Real estate account"].crowns + " " + currency + " " + athenaAccounts["Real estate account"].pennies + " " + currency2}\n\n"
                            + athenaAccounts.ElementAt(5).Key + $" balance: {athenaAccounts["Assets account"].crowns + " " + currency + " " + athenaAccounts["Assets account"].pennies + " " + currency2}";
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

                    Dictionary<string, (int crowns, int pennies)> chosenAccounts = new Dictionary<string, (int, int)>();

                    if (username == "Zeus")
                    {
                        chosenAccounts = zeusAccounts;
                    }
                    else if (username == "Aphrodite")
                    {
                        chosenAccounts = aphroditeAccounts;
                    }
                    else if (username == "Poseidon")
                    {
                        chosenAccounts = poseidonAccounts;
                    }
                    else if (username == "Hermes")
                    {
                        chosenAccounts = hermesAccounts;
                    }
                    else if (username == "Athena")
                    {
                        chosenAccounts = athenaAccounts;
                    }

                    //Visar alla individuella konton och frågar användaren vilket konto man ska överföra från
                    bool tMenu = true;
                    while (tMenu)
                    {
                        Console.WriteLine("Your accounts: \n");

                        foreach (var accountInfo in chosenAccounts)
                        {
                            Console.WriteLine("{0} balance: {1} {2}, {3} {4}", accountInfo.Key, accountInfo.Value.crowns, currency, accountInfo.Value.pennies, currency2);
                        }
                        transferFrom(chosenAccounts, username);
                        break;
                    }
                }

                //Kollar om man skrivit in rätt konto att överföra FRÅN
                void transferFrom(Dictionary<string, (int crowns, int pennies)> chosenaccounts, string username)
                {
                    Dictionary<string, (int crowns, int pennies)> fromAccount = new Dictionary<string, (int, int)>();

                    int accountSelector = 0;

                    Console.Write("\nWhat account would you like to transfer from?: ");
                    string input = Console.ReadLine(); 

                    bool tIncorrect = true;
                    bool tCorrect = false;
                    while (tIncorrect)
                    {
                        for (accountSelector = 0; accountSelector < chosenaccounts.Count; accountSelector++)
                        {
                            //Kollar om man skrivit in rätt kontonamn att överföra FRÅN första kontot
                            if (string.Equals(input, chosenaccounts.ElementAt(accountSelector).Key, StringComparison.CurrentCultureIgnoreCase))
                            {
                                //Återställer input till rättstavat namn
                                input = chosenaccounts.ElementAt(accountSelector).Key;
                                fromAccount.Add(chosenaccounts.ElementAt(accountSelector).Key, chosenaccounts.ElementAt(accountSelector).Value);
                                tIncorrect = false;
                                tCorrect = true;
                                break;
                            }
                        }
                        if (tCorrect == false)
                        {
                            Console.Write("Sorry, please enter one of your registered accounts to transfer from: ");
                            accountSelector = 0;
                            input = Console.ReadLine();
                        }
                    }
                    Console.WriteLine("\nYou selected transfer from: " + fromAccount.ElementAt(0).Key + "\n");
                    transferTo(fromAccount, chosenaccounts, username, input);
                }
                
                //Kollar om man skrivit in rätt kontonamn att överföra TILL
                void transferTo(Dictionary<string, (int crowns, int pennies)> fromaccount, Dictionary<string, (int crowns, int pennies)> chosenaccounts, string username, string account)
                {
                    Dictionary<string, (int crowns, int pennies)> toAccount = new Dictionary<string, (int, int)>();

                    int accountSelector = 0;

                    Console.Write("To which account?: ");
                    string input = Console.ReadLine();

                    bool tIncorrect = true;
                    bool tCorrect = false;
                    while (tIncorrect)
                    {
                        if (string.Equals(input, fromaccount.ElementAt(0).Key, StringComparison.CurrentCultureIgnoreCase))
                        {
                            Console.WriteLine("\nSorry, you can't transfer money from an account to itself.");
                            Console.Write("Please try again: ");
                            input = Console.ReadLine();
                        }
                        else
                        {
                            for (accountSelector = 0; accountSelector < chosenaccounts.Count; accountSelector++)
                            {
                                //Kollar om man skrivit in rätt kontonamn att överföra TILL andra kontot
                                if (string.Equals(input, chosenaccounts.ElementAt(accountSelector).Key, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    toAccount.Add(chosenaccounts.ElementAt(accountSelector).Key, chosenaccounts.ElementAt(accountSelector).Value);
                                    transfer(toAccount, fromaccount, chosenaccounts, username);
                                    tIncorrect = false;
                                    tCorrect = true;
                                    break;
                                }
                            }
                            if (tCorrect == false)
                            {
                                Console.Write("Sorry, please enter one of your registered accounts for transfer: ");
                                accountSelector = 0;
                                input = Console.ReadLine();
                            }
                        }
                    }  
                }

                void transfer(Dictionary<string, (int crowns, int pennies)> toaccount, Dictionary<string, (int crowns, int pennies)> fromaccount, Dictionary<string, (int crowns, int pennies)> chosenaccounts, string username)
                {
                    Console.WriteLine("\nYou chose to transfer from:\n{0} (balance: {1}) to {2} (balance: {3})\n", fromaccount.ElementAt(0).Key, fromaccount.ElementAt(0).Value, toaccount.ElementAt(0).Key, toaccount.ElementAt(0).Value);
                    Console.Write("\nEnter amount " + currency + " to transfer: ");
                    string crownsAmount = Console.ReadLine();
                    Console.Write("\nEnter amount " + currency2 + " to transfer: ");
                    string penniesAmount = Console.ReadLine();
                    bool notNumber = true;
                    while (notNumber)
                    {
                        if (int.TryParse(crownsAmount, out int crowns) && int.TryParse(penniesAmount, out int pennies))
                        {
                            if (crowns > fromaccount.ElementAt(0).Value.crowns && pennies > fromaccount.ElementAt(0).Value.pennies)
                            {
                                // user input     -   valute to store
                                // 200                  20000
                                // 200.5                20050
                                // 200.05               20005
                                // 200.200              error eller 20020
                                Console.WriteLine("Sorry you don't have enough money on this account\n");
                                Console.Write("\nEnter amount" + currency + "to transfer: ");
                                crownsAmount = Console.ReadLine();
                                Console.Write("\nEnter amount" + currency2 + "to transfer: ");
                                penniesAmount = Console.ReadLine();
                            }
                            else
                            {
                                int fromCrowns = fromaccount.ElementAt(0).Value.crowns;
                                int fromPennies = fromaccount.ElementAt(0).Value.pennies;
                                int toCrowns = toaccount.ElementAt(0).Value.crowns;
                                int toPennies = toaccount.ElementAt(0).Value.pennies;

                                fromCrowns -= crowns;
                                fromPennies -= pennies;
                                toCrowns += crowns;
                                toPennies += pennies;

                                if (toPennies >= 100)
                                {
                                    toPennies -= 100;
                                    toCrowns += 1;
                                }

                                chosenaccounts[fromaccount.ElementAt(0).Key] = (fromCrowns, fromPennies);
                                chosenaccounts[toaccount.ElementAt(0).Key] = (toCrowns, toPennies);

                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\nTransfer complete.\n");
                                Console.ResetColor();
                                Console.WriteLine("Your new account balances are:\n\n{0}: {1} {2}, {3} {4}\nand\n{5}: {6} {7}, {8} {9}\n", 
                                    fromaccount.ElementAt(0).Key, chosenaccounts[fromaccount.ElementAt(0).Key].crowns, currency, chosenaccounts[fromaccount.ElementAt(0).Key].pennies, currency2, 
                                    toaccount.ElementAt(0).Key, chosenaccounts[toaccount.ElementAt(0).Key].crowns, currency, chosenaccounts[toaccount.ElementAt(0).Key].pennies, currency2);

                                Console.WriteLine("Press any key to return to the main menu.");
                                if (username == "Zeus")
                                {
                                    zeusAccounts[fromaccount.ElementAt(0).Key] = chosenaccounts[fromaccount.ElementAt(0).Key];
                                    zeusAccounts[toaccount.ElementAt(0).Key] = chosenaccounts[toaccount.ElementAt(0).Key];
                                }
                                else if (username == "Aphrodite")
                                {
                                    aphroditeAccounts[fromaccount.ElementAt(0).Key] = chosenaccounts[fromaccount.ElementAt(0).Key];
                                    aphroditeAccounts[toaccount.ElementAt(0).Key] = chosenaccounts[toaccount.ElementAt(0).Key];
                                }
                                else if (username == "Poseidon")
                                {
                                    poseidonAccounts[fromaccount.ElementAt(0).Key] = chosenaccounts[fromaccount.ElementAt(0).Key];
                                    poseidonAccounts[toaccount.ElementAt(0).Key] = chosenaccounts[toaccount.ElementAt(0).Key];
                                }
                                else if (username == "Hermes")
                                {
                                    hermesAccounts[fromaccount.ElementAt(0).Key] = chosenaccounts[fromaccount.ElementAt(0).Key];
                                    hermesAccounts[toaccount.ElementAt(0).Key] = chosenaccounts[toaccount.ElementAt(0).Key];
                                }
                                else if (username == "Athena")
                                {
                                    athenaAccounts[fromaccount.ElementAt(0).Key] = chosenaccounts[fromaccount.ElementAt(0).Key];
                                    athenaAccounts[toaccount.ElementAt(0).Key] = chosenaccounts[toaccount.ElementAt(0).Key];
                                }
                                notNumber = false;
                                break;
                            }
                        }
                        else
                        {
                            Console.Write("\nSorry, enter amount " + currency + " to transfer: ");
                            crownsAmount = Console.ReadLine();
                            Console.Write("\nEnter amount " + currency2 + " to transfer: ");
                            penniesAmount = Console.ReadLine();
                        }
                    }
                    chosenaccounts.Clear();
                }

                //Uttag
                //void withdraw(string username)
                //{
                //    Console.Clear();

                //    Dictionary<string, float> chosenAccounts = new Dictionary<string, float>();

                //    if (username == "Zeus")
                //    {
                //        chosenAccounts.Add(zeusAccounts[0], zeusBalance[0]);
                //        chosenAccounts.Add(zeusAccounts[1], zeusBalance[1]);
                //    }
                //    else if (username == "Aphrodite")
                //    {
                //        chosenAccounts.Add(athenaAccounts[0], athenaBalance[0]);
                //    }
                //    else if (username == "Poseidon")
                //    {
                //        chosenAccounts.Add(poseidonAccounts[0], poseidonBalance[0]);
                //        chosenAccounts.Add(poseidonAccounts[1], poseidonBalance[1]);
                //    }
                //    else if (username == "Hermes")
                //    {
                //        chosenAccounts.Add(hermesAccounts[0], hermesBalance[0]);
                //        chosenAccounts.Add(hermesAccounts[1], hermesBalance[1]);
                //    }
                //    else if (username == "Athena")
                //    {
                //        chosenAccounts.Add(athenaAccounts[0], athenaBalance[0]);
                //    }

                //    //Visar alla individuella konton och frågar användaren vilket konto man ska överföra från
                //    bool tMenu = true;
                //    while (tMenu)
                //    {
                //        Console.WriteLine("Your accounts: \n");

                //        foreach (var accountInfo in chosenAccounts)
                //        {
                //            Console.WriteLine("{0} balance: {1}", accountInfo.Key, accountInfo.Value);
                //        }
                //        withdrawFrom(chosenAccounts, username);
                //        break;
                //    }
                //}

                ////Kollar om man skrivit in rätt konto att överföra FRÅN
                //void withdrawFrom(Dictionary<string, float> chosenaccounts, string username)
                //{
                //    Console.Write("\nWhat account would you like to deposit from?: ");
                //    string fromAccount = Console.ReadLine();
                //    bool fromFirst = true;
                //    bool tIncorrect = true;
                //    while (tIncorrect)
                //    {
                //        //Kollar om man skrivit in rätt kontonamn att överföra FRÅN första kontot
                //        if (string.Equals(fromAccount, chosenaccounts.ElementAt(0).Key, StringComparison.CurrentCultureIgnoreCase))
                //        {
                //            //Återställer input till rättstavat namn
                //            fromAccount = chosenaccounts.ElementAt(0).Key;
                //            tIncorrect = false;
                //            break;
                //        }
                //        else if (string.Equals(fromAccount, chosenaccounts.ElementAt(1).Key, StringComparison.CurrentCultureIgnoreCase))
                //        {
                //            fromAccount = chosenaccounts.ElementAt(1).Key;
                //            fromFirst = false;
                //            tIncorrect = false;
                //            break;
                //        }
                //        else
                //        {
                //            Console.Write("Sorry, please enter one of your registered accounts to transfer from: ");
                //            fromAccount = Console.ReadLine();
                //        }
                //    }
                //    Console.WriteLine("\nYou selected withdrawal from: " + fromAccount + "\n");
                //    withdrawal(fromAccount, chosenaccounts, username, fromFirst);
                //}

                //void withdrawal(string fromaccount, Dictionary<string, float> chosenaccounts, string username, bool fromfirst)
                //{
                //    float fromBalance = 0;
                //    string chosenAccount = "";

                //    if (fromfirst)
                //    {
                //        fromBalance = chosenaccounts.ElementAt(0).Value;
                //        chosenAccount = chosenaccounts.ElementAt(0).Key;
                //    }
                //    else if (!fromfirst)
                //    {
                //        fromBalance = chosenaccounts.ElementAt(1).Value;
                //        chosenAccount = chosenaccounts.ElementAt(1).Key;
                //    }

                //    Console.Write("\nEnter amount to transfer: ");
                //    string transferAmount = Console.ReadLine();
                //    bool notNumber = true;
                //    while (notNumber)
                //    {
                //        if (float.TryParse(transferAmount, out float amount))
                //        {
                //            if (amount > fromBalance)
                //            {
                //                Console.WriteLine("Sorry you don't have enough money on this account\n");
                //                Console.Write("Try again: ");
                //                transferAmount = Console.ReadLine();
                //            }
                //            else
                //            {
                //                chosenaccounts[fromaccount] -= amount;

                //                Console.Clear();
                //                Console.ForegroundColor = ConsoleColor.Green;
                //                Console.WriteLine("\nWithdrawal complete.\n");
                //                Console.ResetColor();
                //                Console.WriteLine("Your new account balance is:\n\n{0}: {1}\n", chosenAccount, chosenaccounts[fromaccount]);
                //                Console.WriteLine("Press any key to return to the main menu.");
                //                if (username == "Zeus")
                //                {
                //                    if (fromfirst)
                //                    {
                //                        zeusBalance[0] = chosenaccounts.ElementAt(0).Value;
                //                    }
                //                    else if (!fromfirst)
                //                    {
                //                        zeusBalance[1] = chosenaccounts.ElementAt(1).Value;
                //                    }
                //                }
                //                else if (username == "Poseidon")
                //                {
                //                    poseidonBalance[0] = chosenaccounts.ElementAt(0).Value;
                //                    poseidonBalance[1] = chosenaccounts.ElementAt(1).Value;
                //                }
                //                else if (username == "Hermes")
                //                {
                //                    hermesBalance[0] = chosenaccounts.ElementAt(0).Value;
                //                    hermesBalance[1] = chosenaccounts.ElementAt(1).Value;
                //                }
                //                notNumber = false;
                //                break;
                //            }
                //        }
                //        else
                //        {
                //            Console.Write("Sorry, please enter amount to transfer: ");
                //            transferAmount = Console.ReadLine();
                //        }
                //    }
                //    chosenaccounts.Clear();
                //}

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
