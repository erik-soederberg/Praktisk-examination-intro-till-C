using Praktisk_examination_intro_till_C;

await SocketManager.Connect();

await Task.Delay(3000);

await SocketManager.SendMessage("Hello World!");
