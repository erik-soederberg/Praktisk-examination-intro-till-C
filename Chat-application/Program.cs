using Praktisk_examination_intro_till_C;


await SocketManager.Connect();
await Task.Delay(3000);

Console.WriteLine("Welcome to SYS25D's chat! :D ");
Console.WriteLine($"Enter your name to join the chat: ");
string userName = Console.ReadLine();

await SocketManager.NotificationOnJoin(userName);

while (true)
{
    string userInput = Console.ReadLine();
    
    if (userInput == "/exit" || userInput == "/quit")
    {
        break;
    }
    
    await SocketManager.SendMessage(userInput);
   
}