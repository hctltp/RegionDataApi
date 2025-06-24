using RegionDataApi.Business.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRegionDataBusiness(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();