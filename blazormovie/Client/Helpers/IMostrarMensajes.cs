using System.Threading.Tasks;

namespace blazormovie.Client.Helpers
{
    public interface IMostrarMensajes
    {
        Task MostrarMensajeError(string mensaje);

        Task MostrarMensajeExitoso(string mensaje);
    }
}
