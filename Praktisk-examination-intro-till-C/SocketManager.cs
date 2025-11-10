namespace Praktisk_examination_intro_till_C;

using SocketIOClient;

public class SocketManager
{
    private static SocketIO _client;

    private static readonly string Path = "/sys25d";
    
    public static List<string> messages;
    
    private static readonly string EventName = "ahmad123_message";
    
    static SocketManager()
    {
        messages = [];
    }
    
    public static async Task Connect()
    {
        
        _client = new SocketIO("wss://api.leetcode.se", new SocketIOOptions
        {
            Path = Path
        });
        
      
        _client.On(EventName, response =>
        {
            string receivedMessage = response.GetValue<string>();
            
            Console.WriteLine($"Received message: {receivedMessage}");
        });
        
        _client.OnConnected += (sender, args) =>
        {
            Console.WriteLine("Connected!");
        };
        
        
        _client.OnDisconnected += (sender, args) =>
        {
            Console.WriteLine("Disconnected!");
        };

        
        await _client.ConnectAsync();
        
        await Task.Delay(2000);
        
        Console.WriteLine($"Connected: {_client.Connected}");
    }
    
    public static async Task SendMessage(string message)
    {
        await _client.EmitAsync(EventName, message);
        Console.WriteLine($"You said: {message}");
    }
}

