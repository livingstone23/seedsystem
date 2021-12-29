using blazormovie.Shared.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace blazormovie.Client.Repository
{
    public interface IRepositorio
    {
        List<Pelicula> ObtenerPeliculas();

        Task<HttpResponseWrapper<object>> Post<T>(string url, T enviar);

        Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T enviar);

        Task<HttpResponseWrapper<T>> Get<T>(string url);
        Task<HttpResponseWrapper<object>> Put<T>(string url, T enviar);
        Task<HttpResponseWrapper<object>> Delete(string url);

    }
}
