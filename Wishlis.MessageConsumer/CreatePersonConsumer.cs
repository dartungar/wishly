using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using Wishlis.Application.DTO;
using Wishlis.Application.Messages;
using Wishlis.Application.Services;

namespace Wishlis.MessageConsumer;

public class CreatePersonConsumer : IConsumer<CreatePersonMessage>
{
    private readonly ILogger<CreatePersonConsumer> _logger;
    private readonly IPersonService _personService;
    private readonly IMapper _mapper;
    
    public CreatePersonConsumer(ILogger<CreatePersonConsumer> logger, IPersonService personService, IMapper mapper)
    {
        _logger = logger;
        _personService = personService;
        _mapper = mapper;
    }
    
    public async Task Consume(ConsumeContext<CreatePersonMessage> context)
    {
        await _personService.CreatePerson(_mapper.Map<PersonDto>(context.Message));
        _logger.LogInformation($"Created user: {context.Message.Name}");
    }
}