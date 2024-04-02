using Microsoft.AspNetCore.SignalR;

namespace SignalRChat.Hubs;

public class ChatHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    public async Task NotifyNewUser(string userName)
    {
        // Notificar todos os clientes sobre a entrada do novo usuário
        await Clients.AllExcept(Context.ConnectionId).SendAsync("ReceiveNotification", $"{userName} entrou na sessão.");
    }

    public async Task NotifyUserLeft(string userName)
    {
        // Notificar todos os clientes sobre a saída do usuário
        await Clients.AllExcept(Context.ConnectionId).SendAsync("ReceiveNotification", $"{userName} saiu da sessão.");
    }
}