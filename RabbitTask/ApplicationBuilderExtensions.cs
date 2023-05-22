using RabbitTask.Services;

namespace RabbitTask
{
    public static class ApplicationBuilderExtentions
    {
        private static IMessageQueueConsumer Consumer { get; set; }

        public static IApplicationBuilder UseRabbitConsumer(this IApplicationBuilder app)
        {
            try
            {
                Consumer = app.ApplicationServices.GetService<IMessageQueueConsumer>();
                var applifetime = app.ApplicationServices.GetService<IHostApplicationLifetime>();

                applifetime.ApplicationStarted.Register(OnStarted);
                return app;
            }
            catch
            {
                throw;
            }
        }

        private static void OnStarted()
        {
            try
            {
                Consumer.Register();
            }
            catch
            {
                throw;
            }
        }

    }
}