namespace Praktisk_examination_intro_till_C;

public class MessageData
{
    public string Status { get;  }
    public DateTime Timestamp { get; }
    public string Username { get; }
    public string Message { get; }

    public MessageData(string status, DateTime timestamp, string username, string message)
    {
        Status = status;
        Username = username;
        Message = message;
        Timestamp = timestamp;
    }
    
}
public class UserMessage : MessageData
{
    public UserMessage(DateTime timestamp, string user, string messages) : base("message", timestamp, user, messages)
    {
        
    }
}


