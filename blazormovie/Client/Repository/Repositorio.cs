using blazormovie.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace blazormovie.Client.Repository
{
    public class Repositorio : IRepositorio
    {



        private readonly HttpClient httpClient;

        public Repositorio(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }



        /// <summary>
        /// Propiedad para deserealizar
        /// </summary>
        private JsonSerializerOptions OpcionesPorDefectoJSON => new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };



        /// <summary>
        /// #1
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="enviar"></param>
        /// <returns></returns>
        public async Task<HttpResponseWrapper<object>> Post<T>(string url, T enviar)
        {
            var enviarJSON = JsonSerializer.Serialize(enviar);
            var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
            var responseHttp = await httpClient.PostAsync(url, enviarContent);
            return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
        }



        /// <summary>
        /// #2
        /// Metodo que permite deserealizar una respuesta de una peticion HttPost
        /// Permite indicar el tipo de dato que desea de respuesta
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="enviar"></param>
        /// <returns></returns>
        public async Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T enviar)
        {
            var enviarJSON = JsonSerializer.Serialize(enviar);
            var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
            var responseHttp = await httpClient.PostAsync(url, enviarContent);
            if (responseHttp.IsSuccessStatusCode)
            {
                var response = await DeserializarRespuesta<TResponse>(responseHttp, OpcionesPorDefectoJSON);
                return new HttpResponseWrapper<TResponse>(response, false, responseHttp);
            }
            else
            {
                return new HttpResponseWrapper<TResponse>(default, true, responseHttp);
            }
        }



        /// <summary>
        /// #3
        /// Permite centralizar la logica deserealizacion , del metodo #2
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpResponse"></param>
        /// <param name="jsonSerializerOptions"></param>
        /// <returns></returns>
        private async Task<T> DeserializarRespuesta<T>(HttpResponseMessage httpResponse, JsonSerializerOptions jsonSerializerOptions)
        {
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseString, jsonSerializerOptions);
        }



        /// <summary>
        /// #4
        /// Metodo que permite deserealizar una respuesta de una peticion HttGest
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<HttpResponseWrapper<T>> Get<T>(string url)
        {
            var responseHTTP = await httpClient.GetAsync(url);

            if (responseHTTP.IsSuccessStatusCode)
            {
                var response = await DeserializarRespuesta<T>(responseHTTP, OpcionesPorDefectoJSON);
                return new HttpResponseWrapper<T>(response, false, responseHTTP);
            }
            else
            {
                return new HttpResponseWrapper<T>(default, true, responseHTTP);
            }
        }



        /// <summary>
        /// #5
        /// Metodo que permite un consumo de respuesta para consumo metodo put
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="enviar"></param>
        /// <returns></returns>
        public async Task<HttpResponseWrapper<object>> Put<T>(string url, T enviar)
        {
            var enviarJSON = JsonSerializer.Serialize(enviar);
            var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
            var responseHttp = await httpClient.PutAsync(url, enviarContent);
            return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
        }



        public async Task<HttpResponseWrapper<object>> Delete(string url)
        {
            var responseHTTP = await httpClient.DeleteAsync(url);
            return new HttpResponseWrapper<object>(null, !responseHTTP.IsSuccessStatusCode, responseHTTP);
        }



        public List<Pelicula> ObtenerPeliculas()
        {
            return new List<Pelicula>()
{
                    new Pelicula(){Titulo = "Spider-Man: Far From Home", Lanzamiento  = new DateTime(2019, 7, 2)
                    , Poster="https://cronicaglobal.elespanol.com/uploads/s1/61/11/50/7/main-700b9bff30.jpeg"},
                    new Pelicula(){Titulo = "Moana", Lanzamiento  = new DateTime(2016, 11, 23)
                    , Poster="https://i.pinimg.com/736x/d5/1a/12/d51a12c0e614bdfaf4eccd859be9e4c4--cartoon-design-disney-wallpaper.jpg"},
                    new Pelicula(){Titulo = "Inception", Lanzamiento  = new DateTime(2010, 7, 16)
                    , Poster="https://1.bp.blogspot.com/-rOnPGT4DWnM/UlW7RfymSpI/AAAAAAAAE2I/bhMMoy8Dduw/s1600/Inception.jpg"}
                };
        }
    }
}
