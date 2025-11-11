namespace Praktisk_examination_intro_till_C;

public class Message
{
    public DateTime TimeStamp { get; }
    public string User { get; }
    public string Messages { get; }

    public Message(DateTime timestamp, string user, string messages)
    {
        User = user;
        Messages = messages;
        TimeStamp = timestamp;
    }
    
}


