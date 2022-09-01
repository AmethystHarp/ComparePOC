using ComparePOC.Comparer;
using ComparePOC.Model;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<PersonEqualityComparer>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.MapGet("/helloworld", () => Results.Ok("Hello World!"));
app.MapGet("/persons", () =>
{
    var personsList = new List<Person>() { CreatePerson1Data(), CreatePerson2Data() };

    return Results.Json(personsList);
});
app.MapGet("/common-person-compare", () =>
{
    var person1 = CreatePerson1Data();
    var person2 = CreatePerson2Data();

    return Results.Ok(person1.Equals(person2));
});
app.MapGet("/equality-comparer-person-compare", (PersonEqualityComparer personEqualityComparer) =>
{
    var person1 = CreatePerson1Data();
    var person2 = CreatePerson2Data();

    return Results.Ok(personEqualityComparer.Equals(person1, person2));
});

app.Run();

Person CreatePerson1Data() => new Person {
    Id = 1,
    Age = 21,
    Name = "Smurf",
    CurrentAddress = new Address
    {
        AddressLine1 = "Casa de Moras",
        AddressLine2 = "num 34"
    }
};

Person CreatePerson2Data() => new Person{
    Id = 2,
        Age = 21,
        Name = "Smurfete",
        CurrentAddress = new Address
        {
            AddressLine1 = "Casa de Juros",
            AddressLine2 = "num 34"
        }
};