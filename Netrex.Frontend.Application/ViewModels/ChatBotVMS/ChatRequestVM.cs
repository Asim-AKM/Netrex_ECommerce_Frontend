namespace Netrex.Frontend.Application.ViewModels.ChatBotVMS
{
    public class ChatRequestVM
    {
        public string? Message { get; set; }
    }
    public class ChatResponseVM
    {
        public string? Message { get; set; }
        public DateTime Timestamp { get; set; }
        public Guid SessionId { get; set; }
    }
    public class ChatMessageUIVM
    {
        public string? Content { get; set; }
        public bool IsUser { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
