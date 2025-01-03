using System;
using AutoMapper;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers;

public class AuctionUpdatedConsumer : IConsumer<AuctionUpdated>
{
    private readonly IMapper _mapper;

    public AuctionUpdatedConsumer(IMapper mapper)
    {
        _mapper = mapper;
    }
    public async Task Consume(ConsumeContext<AuctionUpdated> context)
    {
        Console.WriteLine("--> consuming auction updated..." + context.Message.Id);

        var item = _mapper.Map<Item>(context.Message);

        await DB.Update<Item>().MatchID(context.Message.Id).ModifyWith(item).ExecuteAsync();
        Console.WriteLine("--> consuming auction successfull..." + item.Model);
    }
}
