using Netrex.Frontend.Application.Commons.AppResponses;
using Netrex.Frontend.Application.ViewModels.ChatBotVMS;

namespace Netrex.Frontend.Application.Services.ChatBot.Interfaces
{
    public interface IChatBotManager
    {
        Task<ApiResponse<ChatResponseVM>> SendMessageAsync(string message);
    }
}
