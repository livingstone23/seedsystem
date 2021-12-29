using System;
using System.Timers;

namespace blazormovie.Client.Auth
{
    /// <summary>
    /// Clase para utilizar timer y renovar el token
    /// </summary>
    public class RenovadorToken : IDisposable
    {


        Timer timer;
        private readonly ILoginService loginService;


        public RenovadorToken(ILoginService loginService)
        {
            this.loginService = loginService;
        }


        /// <summary>
        /// El iniciador debe ser menor al tiempo estimado en la clase ProveedorAutenticacionJWT, 
        /// metodo DebeRenovarToken
        /// </summary>
        public void Iniciar()
        {
            timer = new Timer();
            timer.Interval = 60000; // 1 hora, cada hora verificara si el token se ha expirado
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }


        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            loginService.ManejarRenovacionToken();
        }


        public void Dispose()
        {
            timer?.Dispose();
        }


    }
}
