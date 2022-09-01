using ComparePOC.Comparer;
using ComparePOC.Model;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<PersonEqualityComparer>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapGet("/helloworld", () => Results.Ok("Hello World!"));
app.MapGet("/persons", () => {
    var person1 = new Person
    {
        Id = 1,
        Age = 21,
        Name = "Smurf",
        CurrentAddress = new Address 
        {
            AddressLine1 = "Casa de Moras",
            AddressLine2 = "num 34"
        }
    };

    var person2 = new Person
    {
        Id = 2,
        Age = 21,
        Name = "Smurfete",
        CurrentAddress = new Address 
        {
            AddressLine1 = "Casa de Juros",
            AddressLine2 = "num 34"
        }
    };

    var personsList = new List<Person>() { person1, person2 };

    return Results.Json(personsList);
});
app.MapGet("/common-person-compare", () => {
    var person1 = new Person
    {
        Id = 1,
        Age = 21,
        Name = "Smurf",
        CurrentAddress = new Address 
        {
            AddressLine1 = "Casa de Moras",
            AddressLine2 = "num 34"
        }
    };

    var person2 = new Person
    {
        Id = 1,
        Age = 21,
        Name = "Smurf",
        CurrentAddress = new Address 
        {
            AddressLine1 = "Casa de Moras",
            AddressLine2 = "num 34"
        }
    };

    return Results.Ok(person1.Equals(person2));
});
app.MapGet("/equality-comparer-person-compare", (PersonEqualityComparer personEqualityComparer) => {
    var person1 = new Person
    {
        Id = 1,
        Age = 21,
        Name = "Smurf",
        CurrentAddress = new Address 
        {
            AddressLine1 = "Casa de Moras",
            AddressLine2 = "num 34"
        }
    };

    var person2 = new Person
    {
        Id = 2,
        Age = 21,
        Name = "Smurfete",
        CurrentAddress = new Address 
        {
            AddressLine1 = "Casa de Juros",
            AddressLine2 = "num 34"
        }
    };

    return Results.Ok(personEqualityComparer.Equals(person1,person2));
});

app.Run();
