using Microsoft.AspNetCore.SignalR;
using SignalRExample2.Infraestructure;

namespace SignalRChat.Hubs;

public class TextEditorHub : Hub
{
    private readonly ApplicationDbContext _context;

    public TextEditorHub(ApplicationDbContext context)
    {  
        _context = context; 
    }

    public async Task Create(int id)
    {
        var pauta = this._context.Pautas.FirstOrDefault(x => x.Id == id);

        if (pauta is not null)
        {
            await Clients.Caller.SendAsync("ReceiveChanges", "", pauta.Texto);
        }
        else
        {
            var novaPauta = new Pauta() { Id = id, Texto = "" };
            this._context.Pautas.Add(novaPauta);
            this._context.SaveChanges();

            await Clients.Others.SendAsync("ReceiveChanges", "", "");
        }
    }

    public async Task SendChanges(string user, string newText)
    {
        await Clients.Others.SendAsync("ReceiveChanges", user, newText);

        SaveOnDb(newText);
    }

    private void SaveOnDb(string newText)
    {
        var pauta = this._context.Pautas.FirstOrDefault();

        if (pauta is not null)
        {
            pauta.Texto = newText;
            this._context.SaveChangesAsync();
        }
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