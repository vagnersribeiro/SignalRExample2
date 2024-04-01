using Microsoft.AspNetCore.SignalR;

namespace SignalRChat.Hubs;

public class TextEditorHub : Hub
{
    public async Task SendChanges(string user, string newText)
    {
        //await Clients.AllExcept(Context.ConnectionId).SendAsync("ReceiveTextChanges", newText);
        await Clients.Others.SendAsync("ReceiveChanges", user, newText);
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