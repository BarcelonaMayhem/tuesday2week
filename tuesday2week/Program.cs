using Microsoft.AspNetCore.Mvc;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
List<Processor> repo = [];
app.MapGet("/", () => repo);
app.MapPost("/post", (Processor dto) => repo.Add(dto)); 
app.MapPut("/update", ([FromQuery]int id, UpdateProcessorDTO dto) =>
{
    Processor buffer = repo.Find(p => p.Id == id);
    if (buffer != null)
    {
        if (buffer.Id != dto.id)
            buffer.Id = dto.id;
        if (buffer.Brand != dto.brand)
            buffer.Brand = dto.brand;
        if (buffer.Series != dto.series)
            buffer.Series = dto.series;
        if (buffer.Model != dto.model)
            buffer.Model = dto.model;
        if (buffer.Socket != dto.socket)
            buffer.Socket = dto.socket;
        if (buffer.Price != dto.price)
            buffer.Price = dto.price;
        return Results.Json(buffer);
    }
    else return Results.NotFound();
});
app.MapDelete("/delete", ([FromQuery] int id) =>
{
    Processor buffer = repo.Find(p => p.Id == id);
    repo.Remove(buffer);
});
app.Run();

public class Processor
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public string Series { get; set; }
    public string Model { get; set; }
    public string Socket { get; set; }
    public string Price { get; set; }
    public DateOnly CreateDT { get; set; }
};
record class UpdateProcessorDTO(int id, string brand, string series, string model, string socket, string price);