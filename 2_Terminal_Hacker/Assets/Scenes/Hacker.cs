using UnityEngine;

public class Hacker : MonoBehaviour {

    //Game configuration data
    const string menuHint = "(!) Type menu for the Menu.";
    string[] level1Passwords = { "books", "aisle", "shelf", "password", "font", "borrow" };
    string[] level2Passwords = { "prisoner", "handcuffs", "holster", "uniform", "arrest" };
    string[] level3Passwords = { "starfield", "telescope", "environment", "exploration", "astronauts" };

    //Game state
    int level;
    enum Screen { MainMenu, Password, Win };
    Screen currentScreen;
    string password;

    // Use this for initialization
    void Start () 
    {
        ShowMainMenu();
	}
	
    void ShowMainMenu()
    {
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine("Welcome to Anonimous Group.\nWhere to hack into?");
        Terminal.WriteLine("\n1. @Local Library");
        Terminal.WriteLine("2. @Police Station");
        Terminal.WriteLine("3. @NASA");
        Terminal.WriteLine("\nEnter your choice number: ");
    }


    void OnUserInput(string input)
    {
        if (input == "menu") // we can allways go direct to main menu
        {
            ShowMainMenu();
        } 
        else if(currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
        else if(currentScreen == Screen.Password)
        {
            CheckPassword(input);
        }
        else if(currentScreen == Screen.Win){
            if(input != null){
                ShowMainMenu();
            }
        }
    }

    void RunMainMenu(string input)
    {
        bool isValidLevelNumber = (input == "1" || input == "2" || input == "3");
        if(isValidLevelNumber){
            level = int.Parse(input);
            AskForPassword();
        }
        else if (input == "000") // easter egg
        {
            Terminal.ClearScreen();
            Terminal.WriteLine("Wakeup Neo...");
        }
        else
        {
            Terminal.WriteLine("Please choose a valid level!");
            Terminal.WriteLine(menuHint);
        }
    }


    void AskForPassword()
    {
        currentScreen = Screen.Password;
        Terminal.ClearScreen();
        SetRandomPassword();
        Terminal.WriteLine("Enter password, hint: " + password.Anagram());
        Terminal.WriteLine(menuHint);
    }

    private void SetRandomPassword()
    {
        switch (level)
        {
            case 1:
                password = level1Passwords[Random.Range(0, level1Passwords.Length)];
                break;
            case 2:
                password = level2Passwords[Random.Range(0, level2Passwords.Length)];
                break;
            case 3:
                password = level3Passwords[Random.Range(0, level3Passwords.Length)];
                break;
            default:
                Debug.LogError("Invalid level number");
                break;
        }
    }

    void CheckPassword(string input)
    {
        if (input == password)
        {
            DisplayWinScreen();
        }
        else
        {
          //  Terminal.WriteLine("Password incorrect!\nAccess denied.\nPlease try again.");
            AskForPassword();
        }
    }

    void DisplayWinScreen()
    {
        currentScreen = Screen.Win;
        int code = Random.Range(0, 255);
        Terminal.WriteLine("....................");
        Terminal.WriteLine("....................");
        Terminal.WriteLine("....................");
        Terminal.WriteLine("...server code " + code + "...");
        Terminal.WriteLine("$>user ROOT");
        Terminal.WriteLine("$>password... correct");
        Terminal.WriteLine("$>####### ACCESS GRANTED #######");
        ShowLevelAward();

    }

    void ShowLevelAward()
    {
        switch(level)
        {
            case 1:
                Terminal.WriteLine("\nHave a book.");
                Terminal.WriteLine(@"
     ___________
    /          /
   /          //
  /          //
 /__________//
(__________(/
"
                );
                Terminal.WriteLine("\nENTER to play again.");
                break;
            case 2:
                Terminal.WriteLine("\nYou got the prison key");
                Terminal.WriteLine(@"
 __
/0 \_______
\__/-=' = ' 
"
                );
                Terminal.WriteLine("\nENTER to play again for a greater challenge.");
                break;
            case 3:
                Terminal.WriteLine(@"
  _ __   __ _ ___  __ _
 | '_ \ / _` / __|/ _` |
 | | | | (_| \__ \ (_| |
 |_| |_|\__,_|___)\__,_|
"
                );
                Terminal.WriteLine("Welcome to NASA's internal system!");
                break;
            default:
                Debug.LogError("Invalid level reached");
                break;
        }
        //Terminal.WriteLine("\nENTER to play again.");
    }
}
