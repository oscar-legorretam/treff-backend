using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Mapper;
using Application.Utils;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Application.Features.ChatFeatures.Commands
{
    public class SaveChatMessageCommand : IRequest<ChatMessage>
    {
        public int UserId { get; set; }
        public int ChatId { get; set; }
        public string Message { get; set; }
        public class SaveChatMessageCommandHandler : IRequestHandler<SaveChatMessageCommand, ChatMessage>
        {
            private readonly IMessageRepository _context;
            public SaveChatMessageCommandHandler(IMessageRepository context)
            {
                _context = context;
            }
            public async Task<ChatMessage> Handle(SaveChatMessageCommand request, CancellationToken cancellationToken)
            {
                var response = await _context.SaveMessageAsync(request.Message, request.UserId, request.ChatId);

                if (response == null)
                {
                    throw new UnauthorizedAccessException();
                }

                return response;
            }
        }
    }
}
