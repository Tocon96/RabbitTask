using RabbitTask.Services;
using RabbitTask.Utils;
using ILogger = RabbitTask.Services.ILogger;

namespace RabbitTask
{
    public class Startup
    {
        public IConfiguration configRoot
        {
            get;
        }
        public Startup(IConfiguration configuration)
        {
            configRoot = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ILogger, Logger>();
            services.AddSingleton<IEmailSenderFactory, EmailSenderFactory>();
            services.AddSingleton<IMessageDeserializer, MessageDeserializer>();
            services.AddSingleton<IMessageQueueConsumer, MessageQueueConsumer>();
            services.AddScoped<IMessageQueueProducer, MessageQueueProducer>();
            services.AddScoped<IEmailSender, SmtpSender>();
            services.AddScoped<ISenderTypeValidator, SenderTypeValidator>();
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddEndpointsApiExplorer();
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseRabbitConsumer();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
