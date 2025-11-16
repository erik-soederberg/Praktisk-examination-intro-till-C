using Praktisk_examination_intro_till_C;


await SocketManager.Connect();

Console.WriteLine("*** Welcome to SYS25D's chat ***");
Console.WriteLine("*** Username may only contain letters and must be between 3-10 characters. ***");

string userName;

while (true)
{
    Console.Write("Enter your name to join the chat: ");
    userName = Console.ReadLine();
    
    if (!string.IsNullOrWhiteSpace(userName) &&
        userName.Length >= 3 &&
        userName.Length <= 10 &&
        userName.All(char.IsLetter))
    {
        break; 
    }
    
    Console.WriteLine("Invalid input! Please enter a name with only letters and between 3–10 characters).");
    
}

await SocketManager.NotificationOnJoin(userName);

while (true)
{
    string userInput = Console.ReadLine();
    
    if (userInput == "quit" || userInput == "exit")
    {
        break;
    }
    
    await SocketManager.SendMessage(userInput, userName);
   
}

await SocketManager.Disconnect();