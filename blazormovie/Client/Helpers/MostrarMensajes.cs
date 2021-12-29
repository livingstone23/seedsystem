using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace blazormovie.Client.Helpers
{
    public class MostrarMensajes : IMostrarMensajes
    {
        private readonly IJSRuntime js;

        public MostrarMensajes(IJSRuntime js)
        {
            this.js = js;
        }

        public async Task MostrarMensajeError(string mensaje)
        {
            await MostrarMensaje("Error", mensaje, "error");
        }

        public async Task MostrarMensajeExitoso(string mensaje)
        {
            await MostrarMensaje("Exitoso", mensaje, "success");
        }

        /// <summary>
        /// metodo auxiliar
        /// </summary>
        /// <param name="titulo"></param>
        /// <param name="mensaje"></param>
        /// <param name="tipoMensaje"></param>
        /// <returns></returns>
        private async ValueTask MostrarMensaje(string titulo, string mensaje, string tipoMensaje)
        {
            await js.InvokeVoidAsync("Swal.fire", titulo, mensaje, tipoMensaje);
        }
    }
}
