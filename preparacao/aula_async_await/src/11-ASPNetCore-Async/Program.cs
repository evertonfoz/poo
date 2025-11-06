using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

// Pedagogical Program.cs: configura servidor Web API com controllers e middleware
// O foco aqui é demonstrar: por que aceitar CancellationToken em endpoints,
// como mapear cancelamento para um status apropriado e onde a lógica de I/O
// (repositório) deve permanecer assíncrona.
//
// Pontos-chave:
// - Aceitar CancellationToken no controller permite ao servidor reagir quando o
//   cliente cancela a requisição (ex.: fecha conexão) ou quando o servidor
//   decide cancelar por timeout. Isso evita trabalho desnecessário e libera
//   recursos.
// - Lógica de I/O permanece na borda (repositório), usando APIs assíncronas
//   que recebem tokens. O controller orquestra e propaga o token.
// - Middleware global pode traduzir OperationCanceledException/TaskCanceledException
//   em um status code apropriado (ex.: 499) e logar para monitoramento.
//
// Perguntas-guia:
// 1) Quem cancela: o cliente ou o servidor? (ambos; o cliente pode abortar a conexão, o servidor pode aplicar timeouts)
// 2) Onde deve ficar a lógica de retry ou políticas? (Application/Infra; o controller não deve conter retry complexos)
// 3) Como reportar cancelamento ao observability/tracing? (logar e expor métricas específicas)

var builder = WebApplication.CreateBuilder(args);

// Registrar controller support and DI for repository
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registro do repositório que simula I/O assíncrono
builder.Services.AddScoped<Repositories.IAsyncDataRepository, Repositories.MockDataRepository>();

var app = builder.Build();

// Middleware: mapear cancelamento para 499 (client closed request) e logar
app.Use(async (context, next) =>
{
    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
    try
    {
        await next();
    }
    catch (OperationCanceledException oce)
    {
        // 499 is a common non-standard code used by some proxies to indicate client closed request.
        // We log and return a friendly status. In production, adapt to your API convention.
        logger.LogInformation(oce, "Request cancelled: {Method} {Path}", context.Request.Method, context.Request.Path);
        if (!context.Response.HasStarted)
        {
            context.Response.StatusCode = 499; // Client Closed Request (non-standard but useful)
            await context.Response.WriteAsync("Request cancelled (OperationCanceled)");
        }
    }
    catch (Exception ex)
    {
        var logger2 = context.RequestServices.GetRequiredService<ILogger<Program>>();
        logger2.LogError(ex, "Unhandled exception processing request {Method} {Path}", context.Request.Method, context.Request.Path);
        throw; // let default handlers convert to 500
    }
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
