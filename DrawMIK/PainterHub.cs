using Microsoft.AspNetCore.SignalR;

namespace DrawMIK
{
    public class PainterHub : Hub
    {
        public async Task SendDraw(int x, int y, int pos1, int pos2)
        {
            await Clients.Others.SendAsync("ReceiveDraw",x,y, pos1, pos2);
        }


    }
}
