using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Megarender.DataBus;
using Megarender.DataBus.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Megarender.IdentityService.CQRS
{
    public class SendCodeHandler : IRequestHandler<SendCodeCommand, Guid>
    {
        private readonly AppDbContext _identityDbContext;
        private readonly UtilsService _utils;
        private readonly ApplicationOptions _options;
        private readonly IMessageProducerService _messageProducer;
        public SendCodeHandler(AppDbContext identityDbContext, IOptions<ApplicationOptions> options, UtilsService utils, IMessageProducerService messageProducer)        
        {
            _identityDbContext = identityDbContext;
            _utils = utils;
            _messageProducer = messageProducer;
            _options = options?.Value ?? throw new NullReferenceException(nameof(ApplicationOptions));
        }
        public async Task<Guid> Handle(SendCodeCommand request, CancellationToken cancellationToken = default)
        {
            Guid result;
            var code = _utils.GenerateCode(_options.LowerBoundCode, _options.UpperBoundCode);
            var identityExist = await _identityDbContext.Identities.FirstOrDefaultAsync(u=>u.Phone.Equals(request.Phone), cancellationToken);

            if(identityExist!=null)
            {                
                identityExist.Code=code;                
                result = identityExist.Id;
            }
            else 
            {
                var identity = new Identity {
                    Id=Guid.NewGuid(),
                    Phone = request.Phone,
                    Code = code
                };

                await _identityDbContext.Identities.AddAsync(identity, cancellationToken);
                result = identity.Id;
            }
            await _identityDbContext.SaveChangesAsync(cancellationToken);
            
            _messageProducer.Enqueue(new CodeGeneratedEvent
                {
                    Code = code,
                    UserId = result
                });
            
            return result;
        }
    }
}