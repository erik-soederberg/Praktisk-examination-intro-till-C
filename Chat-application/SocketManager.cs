namespace Praktisk_examination_intro_till_C;

using SocketIOClient;
using System.Text.Json;


public class SocketManager
{
    private static SocketIO? _client;

    private static readonly string Path = "/sys25d";
    
    public static List<string> Messages;
    
    private static readonly string MessageEvent = "message";      
    private static readonly string JoinEvent = "join";         
    private static string _userName;

    
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
            try
            {
                string json = response.GetValue<string>();
                MessageData message = JsonSerializer.Deserialize<MessageData>(json);
                Console.WriteLine($"[{message.Timestamp:t}] {message.Username}: {message.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing message: {ex.Message}");
                Console.WriteLine(response.ToString());
            }

        });

        _client.On("join", response =>
        {
            try
            {
                string joinedUser = response.GetValue<string>();
                
                if (!string.Equals(joinedUser, _userName, StringComparison.OrdinalIgnoreCase))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{joinedUser} just joined the chat!");
                    Console.ResetColor();
                }
            }
            catch
            {
                Console.WriteLine("A new user joined.");
            }
        });

        _client.On("km_leave", response =>
        {
            try
            {
                string userLeft = response.GetValue<string>();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"{userLeft} has left the chat.");
                Console.ResetColor();
            }
            catch
            {
                Console.WriteLine("A user left the chat.");
            }
        });
        
        _client.OnConnected += (sender, args) =>
        {
            Console.WriteLine("Connected!");
        };
        
        await _client.ConnectAsync();
        
        Console.WriteLine($"Connected: {_client.Connected}");
    }
    
    public static async Task SendMessage(string message, string userName)
    {
        var userMessage = new UserMessage(DateTime.Now, userName, message);
        string jsonMessage = JsonSerializer.Serialize(userMessage);
        
        await _client.EmitAsync(MessageEvent, jsonMessage);
        Console.WriteLine($"You: {message}");
    }
    
    public static async Task NotificationOnJoin(string userName)
    {
        _userName = userName;
        await _client.EmitAsync(JoinEvent, userName);
        Console.WriteLine($"{userName} joined the chat <3");
    }


    public static async Task Disconnect()
    {
        if (_client != null)
        {
            await _client.DisconnectAsync();
            Console.WriteLine($"You have successfully disconnected.");
        }
    }
}   