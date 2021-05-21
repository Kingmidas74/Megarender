using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Megarender.DataBus;
using Megarender.DataBus.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Megarender.IdentityService.CQRS
{
    public class VerifyCodeHandler : IRequestHandler<VerifyCodeCommand, string>
    {
        private readonly AppDbContext _identityDbContext;
        private readonly UtilsService _utils;
        private readonly ApplicationOptions _options;
        private readonly IMessageProducerService _messageProducer;
        public VerifyCodeHandler(AppDbContext identityDbContext, UtilsService utils, IOptions<ApplicationOptions> options, IMessageProducerService messageProducer)
        {
            _identityDbContext = identityDbContext;
            _utils = utils;
            _messageProducer = messageProducer;
            _options = options?.Value ?? throw new NullReferenceException(nameof(ApplicationOptions));
        }

        public async Task<string> Handle(VerifyCodeCommand request, CancellationToken cancellationToken = default)
        {
            var identity = await _identityDbContext.Identities.FirstOrDefaultAsync(x=>x.Code.Equals(request.Code) && x.Id.Equals(request.Id), cancellationToken);

            if(identity == null) throw new ConstraintException();

            var user = await _identityDbContext.Users.FirstOrDefaultAsync(x => x.Phone.Equals(identity.Phone), cancellationToken);

            var salt = _utils.GenerateSalt(identity.Phone.Length);
            
            if (isLoginAttempt(user))
            {
                _identityDbContext.Identities.Remove(identity);                
                user.Password = _utils.HashedPassword(user.Phone, salt, _options.Pepper);
                user.Salt = salt;
                await _identityDbContext.SaveChangesAsync(cancellationToken);
                return user.Password;
            }

            var newUser = new User
            {
                Id = identity.Id,
                Password = _utils.HashedPassword(identity.Phone, salt, _options.Pepper),
                Phone = identity.Phone,
                Salt = salt
            };
            await _identityDbContext.Users.AddAsync(newUser, cancellationToken);
            _identityDbContext.Identities.Remove(identity);    
            await _identityDbContext.SaveChangesAsync(cancellationToken);
            
            
            _messageProducer.Enqueue(new UserRegistratedEvent
                {
                    UserId = newUser.Id
                }, new Dictionary<string, string>());
            return newUser.Password;
        }

        private bool isLoginAttempt(User user) => user != null;
        
    }
}