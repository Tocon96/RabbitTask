using RabbitTask.Services;

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
            services.AddSingleton<IMessageQueueConsumer, MessageQueueConsumer>();
            services.AddScoped<IMessageQueueProducer, MessageQueueProducer>();
            services.AddScoped<IEmailSenderFactory, EmailSenderFactory>();
            services.AddScoped<IEmailSender, SmtpSender>();
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
