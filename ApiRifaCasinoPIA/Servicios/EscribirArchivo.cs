namespace ApiRifaCasinoPIA.Servicios
{
    public class EscribirArchivo : IHostedService
    {
        private readonly IWebHostEnvironment env;
        private readonly string fileName = "Ejecucion.txt";

        public EscribirArchivo(IWebHostEnvironment env)
        {
            this.env = env;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Registrar("Ejecución iniciada: " + DateTime.Now.ToString("dd/MM/yy HH:mm:ss"));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Registrar("Ejecución finalizada: " + DateTime.Now.ToString("dd/MM/yy HH:mm:ss"));
            return Task.CompletedTask;
        }

        public void Registrar(string registro)
        {
            var ruta = $@"{env.ContentRootPath}\wwwroot\{fileName}";
            using StreamWriter writer = new StreamWriter(ruta, append: true);
            writer.WriteLine(registro);
        }
    }
}
