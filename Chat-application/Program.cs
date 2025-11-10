using Praktisk_examination_intro_till_C;



Console.WriteLine($"Enter your name to join the chat: ");
string userName = Console.ReadLine();

await SocketManager.Connect();
await Task.Delay(3000);

await SocketManager.NotificationOnJoin(userName);


