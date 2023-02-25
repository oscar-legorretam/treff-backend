using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.CategoryFeatures.Queries;
using Application.Features.ChatFeatures.Commands;
using Application.Features.ProductFeatures.Commands;
using Application.Features.ProductFeatures.Queries;
using Application.Features.ProjectFeatures.Commands;
using Application.Features.ProjectFeatures.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApi.Hubs;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ChatController : BaseApiController
    {
        private readonly IHubContext<ChatHub> _chatHub;

        public ChatController(IHubContext<ChatHub> chatHub)
        {
            _chatHub = chatHub;
        }

        //[HttpPost("messages")]
        //public async Task Post(ChatMessage message)
        //{
        //    // run some logic...

        //    await _chatHub.Clients.All.ReceiveMessage(message);
        //}

        [HttpPost("add")]
        public async Task Add(ChatMessage message)
        {
            message.Message = "se conectó al chat";
            await _chatHub.Clients.Group(message.Group.ToString())
                .SendAsync("UsersAdded", message);
            // run some logic...

            //await _chatHub.Clients.Group(message.Group).SendAsync("", message);
        }
        [HttpPost("messages")]
        public async Task Post(ChatMessage message)
        {
            var request = new SaveChatMessageCommand();
            request.UserId = message.UserId;
            request.Message = message.Message;
            request.ChatId = message.ChatId;
            await Mediator.Send(request);

            await _chatHub.Clients.Group(message.Group.ToString())
                .SendAsync("UsersAdded", message);
            // run some logic...

            //await _chatHub.Clients.Group(message.Group).SendAsync("", message);
        }

        [HttpPost("getChat")]
        public async Task<IActionResult> GetChat(GetOrCreateChatCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}