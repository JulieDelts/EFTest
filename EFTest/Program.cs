using System.Linq.Expressions;
using EFTest;
using Microsoft.EntityFrameworkCore;

var factory = new CookbookContextFactory();

Console.WriteLine("Use basic CRUD operations");
await CheckBasicOperations(factory);

Console.WriteLine("Check Entity states in EF");
await CheckEntityStates(factory);

Console.WriteLine("Analyse tracking in different contexts");
await ChangeTracking(factory);

Console.WriteLine("Change an object tracking status");
await DetachEntities(factory);

Console.WriteLine("Get a list of objects without subsequent EF tracking");
await StopTracking(factory);

Console.WriteLine("Populate the Dish table");
await PopulateDishTable(factory); 

Console.WriteLine("Write raw sql query");
await WriteRawSql(factory);

Console.WriteLine("Analyse an expression tree");
await AnalyseExpressionTree(factory);   

Console.WriteLine("Check how a transaction works");
await UseTansaction(factory);


static async Task AnalyseExpressionTree(CookbookContextFactory factory)
{ 
    using var context = factory.CreateDbContext();
    var newDish = new Dish { Name = "Lasagna", Notes = "An Italian dish" };
    context.Add(newDish);
    await context.SaveChangesAsync();

    var dishes = await context.Dishes
                              .Where(d => d.Name.StartsWith("L"))
                              .ToListAsync(); // startsWith -> like N'L%'

    Func<Dish,bool> f = d => d.Name.StartsWith("L"); //in-memory, without EF 
    Expression<Func<Dish, bool>> ex = d => d.Name.StartsWith("L"); // while connected to a db with EF

}
static async Task UseTansaction(CookbookContextFactory factory)
{
    using var context = factory.CreateDbContext();
    using var transaction = await context.Database.BeginTransactionAsync();
    try
    {
        context.Dishes.Add(new Dish { Name = "Qwe", Notes = "Something alien" });
        await context.SaveChangesAsync();

        await context.Database.ExecuteSqlRawAsync("SELECT 1/0 as Bad");
        await transaction.CommitAsync(); //rollback because of the error
    }
    catch (Exception ex)
    { 
        Console.Error.WriteLine($"Something bad happened: {ex}");
    }
}
static async Task PopulateDishTable(CookbookContextFactory factory)
{
    using var context = factory.CreateDbContext();
    var dish1 = new Dish { Name = "Fish and Chips", Notes = "A really delicious English meal", Stars = 4 };
    var dish2 = new Dish { Name = "Pizza", Notes = "A famous Italian dish", Stars = 4 };
    var dish3 = new Dish { Name = "Tiramisu", Notes = "An Italian dessert", Stars = 5 };
    var dish4 = new Dish { Name = "Pelmeni", Notes = "An interesting Russian dish", Stars = 3 };
    var dish5 = new Dish { Name = "Maki sushi", Notes = "Just divine", Stars = 6 };
    await context.AddRangeAsync(dish1, dish2, dish3, dish4, dish5);
    await context.SaveChangesAsync();
}
static async Task CheckBasicOperations(CookbookContextFactory factory)
{
    using var context = factory.CreateDbContext();
    Console.WriteLine("Add Porridge for breakfast");
    var porridge = new Dish { Name = "Breakfast Porridge", Notes = "It's really delicious", Stars = 4 };
    context.Dishes.Add(porridge);
    await context.SaveChangesAsync();
    Console.WriteLine($"Added the dish successfully");

    Console.WriteLine("Check stars of Porridge for breakfast");
    var dishes = await context.Dishes.Where(d => d.Name.Contains("Porridge")).ToListAsync();
    if (dishes.Count != 1) Console.Error.WriteLine("Something bad happened");
    Console.WriteLine("Checked stars of the dish successfully");
    Console.WriteLine($"Porridge has {dishes[0].Stars} stars");

    Console.WriteLine("Change Porridge for breakfast");
    porridge.Stars = 5;
    await context.SaveChangesAsync();
    Console.WriteLine($"Changed the dish stars to {porridge.Stars} successfully");

    Console.WriteLine("Remove Porridge for breakfast");
    context.Dishes.Remove(porridge);
    await context.SaveChangesAsync();
    Console.WriteLine("Removed the dish successfully");
}
static async Task WriteRawSql(CookbookContextFactory factory)
{
    using var context = factory.CreateDbContext();
    var dishes = await context.Dishes
                       .FromSqlRaw("""SELECT * FROM public."Dishes" """)
                       .ToArrayAsync(); //get data
    Console.WriteLine(dishes[0].Name);

    var filter = "%i";
    var dishesList = await context.Dishes
                           .FromSqlInterpolated($"""SELECT * FROM public."Dishes" WHERE "Name" LIKE {filter}""")
                           .ToListAsync(); //get data with filters
    
    for (int i = 0; i < dishesList.Count; i++)
    {
        Console.Write($"{dishesList[i].Name}, ");  
    }

    await context.Database.ExecuteSqlRawAsync("""DELETE FROM public."Dishes" WHERE "Stars" NOT IN (5,6)"""); //write data
                   
}
static async Task StopTracking(CookbookContextFactory factory)
{
    using var context = factory.CreateDbContext();
    var newDish = new Dish { Name = "Wurst", Notes = "A German dish" };
    context.Add(newDish);

    await context.SaveChangesAsync();
    var dishes = await context.Dishes.AsNoTracking().ToArrayAsync(); //get a list of objects without tracking (when there are no changes in the future)
    var state = context.Entry(dishes[0]).State;

    context.Remove(newDish);
    await context.SaveChangesAsync();
}
static async Task DetachEntities(CookbookContextFactory factory)
{
    using var context = factory.CreateDbContext();
    var newDish = new Dish { Name = "Blini", Notes = "A Russian dish" };
    context.Add(newDish);
    await context.SaveChangesAsync();

    context.Entry(newDish).State = EntityState.Detached; //Make EF context forget the object
    var state = context.Entry(newDish).State; // Detached

    context.Dishes.Update(newDish); //override the entire dish object
    await context.SaveChangesAsync();

    context.Remove(newDish);
    await context.SaveChangesAsync();
}
static async Task CheckEntityStates(CookbookContextFactory factory)
{
    using var context = factory.CreateDbContext();

    var newDish = new Dish { Name = "Ramen", Notes = "A Japanese dish" };
    var state = context.Entry(newDish).State; // Detached

    context.Dishes.Add(newDish);
    state = context.Entry(newDish).State; // Added

    await context.SaveChangesAsync();
    state = context.Entry(newDish).State; // Unchanged

    newDish.Notes = "A fabulous japanese dish";
    state = context.Entry(newDish).State; // Modified

    await context.SaveChangesAsync();
    state = context.Entry(newDish).State; // Unchanged

    context.Dishes.Remove(newDish);
    state = context.Entry(newDish).State; // Deleted

    await context.SaveChangesAsync();
    state = context.Entry(newDish).State; // Detached again
}
static async Task ChangeTracking(CookbookContextFactory factory)
{
    using var context = factory.CreateDbContext();
    var newDish = new Dish { Name = "Lasagna", Notes = "An Italian dish" };
    context.Add(newDish);
    await context.SaveChangesAsync();
    newDish.Notes = "An awesome Italian dish";

    var entry = context.Entry(newDish);
    var originalValue = entry.OriginalValues[nameof(Dish.Notes)]?.ToString();
    var dishFromDatabase = await context.Dishes.SingleAsync(d => d.Id == newDish.Id); //An awesome Italian dish (saved in the current context but not in the DB)

    using var contextNew = factory.CreateDbContext();
    var dishFromDatabaseNew = await context.Dishes.SingleAsync(d => d.Id == newDish.Id); // An Italian dish

    context.Remove(newDish);
    await context.SaveChangesAsync();
}

