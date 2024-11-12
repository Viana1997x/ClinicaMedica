using Core.Interfaces;
using Core.Repositories;
using Core.Services;
using Core.Mapping;
using System.Data;
using Microsoft.Data.Sqlite;
using AutoMapper;
using Dapper;

var builder = WebApplication.CreateBuilder(args);

SQLitePCL.Batteries.Init(); // Inicializa a biblioteca SQLite

// Adiciona os controladores ao contêiner
builder.Services.AddControllers();

// Configuração do Swagger para geração de documentação da API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);  // Inclui os comentários XML para Swagger
});

// Configuração de Dapper com SQLite
builder.Services.AddScoped<IDbConnection>(sp => new SqliteConnection(
    builder.Configuration.GetConnectionString("DefaultConnection")
));

// Configuração do AutoMapper e injeção de dependências de Repositórios e Serviços
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));  // Registro do perfil AutoMapper
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IMedicoRepository, MedicoRepository>();
builder.Services.AddScoped<IConsultaRepository, ConsultaRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IMedicoService, MedicoService>();
builder.Services.AddScoped<IConsultaService, ConsultaService>();

var app = builder.Build();

// Método para garantir que o banco de dados SQLite esteja criado e inicializado corretamente
InitializeDatabase(app.Services);

// Configuração do middleware para o ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

/// <summary>
/// Método para inicializar o banco de dados SQLite
/// </summary>
/// <param name="services"></param>
void InitializeDatabase(IServiceProvider services)
{
    using (var scope = services.CreateScope())
    {
        var scopedServices = scope.ServiceProvider;
        using var connection = scopedServices.GetRequiredService<IDbConnection>();

        // Criação de tabela de exemplo para Clientes (adapte conforme sua necessidade)
        var clienteTable = @"
            CREATE TABLE IF NOT EXISTS Clientes (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Nome TEXT NOT NULL,
                CPF TEXT NOT NULL,
                Endereco TEXT NOT NULL,
                DataNascimento TEXT NOT NULL,
                Telefone TEXT NOT NULL,
                Senha TEXT NOT NULL
            );";

        connection.Execute(clienteTable);

        // Criação de tabela de exemplo para Médicos (adapte conforme sua necessidade)
        var medicoTable = @"
            CREATE TABLE IF NOT EXISTS Medicos (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Nome TEXT NOT NULL,
                CPF TEXT NOT NULL,
                Endereco TEXT NOT NULL,
                DataNascimento TEXT NOT NULL,
                Telefone TEXT NOT NULL,
                AreaEspecializacao TEXT NOT NULL,
                Senha TEXT NOT NULL
            );";

        connection.Execute(medicoTable);

        // Criação de tabela de exemplo para Consultas
        var consultaTable = @"
            CREATE TABLE IF NOT EXISTS Consultas (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                ClienteId INTEGER,
                MedicoId INTEGER,
                DataConsulta TEXT NOT NULL,
                FOREIGN KEY(ClienteId) REFERENCES Clientes(Id),
                FOREIGN KEY(MedicoId) REFERENCES Medicos(Id)
            );";

        connection.Execute(consultaTable);
    }
}