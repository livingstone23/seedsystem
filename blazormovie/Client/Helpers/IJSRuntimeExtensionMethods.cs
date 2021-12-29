using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace blazormovie.Client.Helpers
{
    public static class IJSRuntimeExtensionMethods
    {

        /// <summary>
        /// Funcion de apoyo para desloguear
        /// Llamada desde MainLayout
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="js"></param>
        /// <param name="dotNetObjectReference"></param>
        /// <returns></returns>
        public static async ValueTask InicializarTimerInactivo<T>(this IJSRuntime js,DotNetObjectReference<T> dotNetObjectReference) where T : class
        {
            await js.InvokeVoidAsync("timerInactivo", dotNetObjectReference);
        }

        public static async ValueTask<bool> Confirm(this IJSRuntime js, string mensaje)
        {
            await js.InvokeVoidAsync("console.log", "prueba de console.log");
            return await js.InvokeAsync<bool>("confirm", mensaje);
        }



        /// <summary>
        /// Funcion que permite guardar contenido en localstorage
        /// </summary>
        /// <param name="js"></param>
        /// <param name="key"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static ValueTask<object> SetInLocalStorage(this IJSRuntime js, string key, string content)
           => js.InvokeAsync<object>("localStorage.setItem", key, content);



        /// <summary>
        /// Funcion que obtiene lo almacenado en localstorage
        /// </summary>
        /// <param name="js"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static ValueTask<string> GetFromLocalStorage(this IJSRuntime js, string key)
            => js.InvokeAsync<string>("localStorage.getItem", key);



        /// <summary>
        /// Funcion que remueve elementos de localstorage
        /// </summary>
        /// <param name="js"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static ValueTask<object> RemoveItem(this IJSRuntime js, string key)
            => js.InvokeAsync<object>("localStorage.removeItem", key);


    }
}
