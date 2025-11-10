namespace Praktisk_examination_intro_till_C;

using SocketIOClient;

public class SocketManager
{
    private static SocketIO? _client;

    private static readonly string Path = "/sys25d";
    
    public static List<string> Messages;
    
    private static readonly string MessageEvent = "message";      
    private static readonly string JoinEvent = "join";            
    private static readonly string UserJoinedChat = "user_joined_chat"; 
    private static readonly string UserLeftChat = "user_left_chat"; 
    
    static SocketManager()
    {
        Messages = [];
    }
    
    public static async Task Connect()
    {
        
        _client = new SocketIO("wss://api.leetcode.se", new SocketIOOptions
        {
            Path = Path
        });
        
        
        _client.On(MessageEvent, response =>
        {
            string receivedMessage = response.GetValue<string>();
            
            Console.WriteLine($"Received message: {receivedMessage}");
        });

        _client.On(UserJoinedChat, response =>
        {
            string userName = response.GetValue<string>();

            Console.WriteLine($"{userName} Joined the chat <3 ");

        });

        _client.On(UserLeftChat, response =>
        {
            string userName = response.GetValue<string>();

            Console.WriteLine($"{userName} Left the chat </3 ");
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
        await _client.EmitAsync(MessageEvent, message);
        Console.WriteLine($"You said: {message}");
    }
}

