using EFRelations;
using EFRelations.Models;
using Microsoft.EntityFrameworkCore;

var factory = new BrickContextFactory();
using var context = factory.CreateDbContext();
//await AddData();
await QueryData();
Console.WriteLine("Done");

async Task AddData()
{
    Vendor brickKing, heldDerSteine;
    await context.AddRangeAsync(new[]
     {
       brickKing = new Vendor() { Name = "Brick King" },
       heldDerSteine = new Vendor() {Name = "Held der Steine"}
    });
    await context.SaveChangesAsync();
    Tag rare, ninjago, minecraft;
    await context.AddRangeAsync(new[]
     {
       rare = new Tag() { Name = "Rare" },
       ninjago = new Tag() {Name = "Ninjago"},
       minecraft = new Tag() {Name = "Ninecraft"}
    });
    await context.SaveChangesAsync();

    await context.AddAsync(new BasePlate() { Name = "BasePlate with blue pattern", 
        Colour = Colour.Green, 
        Tags = new() { rare, minecraft },
        Length = 16,
        Width = 16,
        Availability = new()
        {
            new() { Vendor = brickKing, AvailableAmount = 15, PriceEur = 3.6m },
            new() {Vendor = heldDerSteine, AvailableAmount = 30, PriceEur = 4.1m}
        }});
    await context.SaveChangesAsync();
}
async Task QueryData()
{
    var availabilityData = await context.BrickAvailabilities
        .Include(ba => ba.Brick)
        .Include(ba => ba.Vendor)
        .ToArrayAsync();

    foreach (var availability in availabilityData)
    {
        Console.WriteLine($"Brick {availability.Brick.Name} available at {availability.Vendor.Name} for {availability.PriceEur}");
    }

    var brickWithVendorsAndTags = await context.Bricks
        .Include(nameof(Brick.Availability) + "." + nameof(BrickAvailability.Vendor))
        .Include(b => b.Tags)
        .ToArrayAsync();

    foreach (var item in brickWithVendorsAndTags)
    {
        Console.Write($"Brick {item.Name} ");
        if (item.Tags.Any()) Console.Write($"({string.Join(',', item.Tags.Select(t => t.Name))})");
        if (item.Availability.Any()) Console.Write($" is available at {string.Join(',', item.Availability.Select(a => a.Vendor.Name))}");
        Console.WriteLine();
    }

    //var simpleBricks = await context.Bricks.ToArrayAsync();
    //foreach (var item in simpleBricks)
    //{
    //    await context.Entry(item).Collection(i => i.Tags).LoadAsync();
    //    Console.Write($"Brick {item.Name} ");
    //    if (item.Tags.Any()) Console.Write($"({string.Join(',', item.Tags.Select(t => t.Name))})");
    //    Console.WriteLine();
    //}




}