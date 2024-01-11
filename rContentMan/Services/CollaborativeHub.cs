using Domain.DTOs;
using Microsoft.AspNetCore.SignalR;

namespace rContentMan.Services
{
    public class CollaborativeHub:Hub
    {
        public async Task SendElementContent(ElementHubDto dto)
        {
            await Clients.Client(dto.DocumentId.ToString()).SendAsync("UpdateElement", dto);
        }
    }
}
