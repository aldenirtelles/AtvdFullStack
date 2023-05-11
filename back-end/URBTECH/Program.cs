using AutoMapper;
using Data.Contracts;
using Datas.Contracts.Requests;
using Datas.Contracts.Responses;
using Entities.Entities;
using Infrastructure.Configuration;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.SetIsOriginAllowed(_ => true) // permitindo o cors para qualquer origem (só para atividade!! isso não deve ser feito!)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Config Services
builder.Services.AddDbContext<ContextBase>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// INTERFACE E REPOSITORIO
builder.Services.AddSingleton<ICadastroUsuario, RepositorioUsuario>();

var config = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.CreateMap<UsuarioDto, Usuario>();
    cfg.CreateMap<Usuario, UsuarioDto>();
    cfg.CreateMap<UsuarioResponse, Usuario>();
    cfg.CreateMap<Usuario, UsuarioResponse>();
    cfg.CreateMap<LoginResponse, Usuario>();
    cfg.CreateMap<Usuario, LoginResponse>();
    cfg.CreateMap<PerfilResponse, Usuario>();
    cfg.CreateMap<Usuario, PerfilResponse>();
});

IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();
