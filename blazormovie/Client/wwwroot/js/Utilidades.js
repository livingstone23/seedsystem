function pruebaPuntoNetStatic() {
    DotNet.invokeMethodAsync("blazormovie.Client", "ObtenerCurrentCount")
        .then(resultado => {
            console.log("conteo desde javascript " + resultado);
        });
}


function pruebaPuntoNETInstancia(dotnetHelper) {
    dotnetHelper.invokeMethodAsync("IncrementCount");
}

/*Metodo para desloguear a los usuarios*/
function timerInactivo(dotnetHelper) {
    var timer;
    document.onmousemove = resetTimer;
    document.onkeypress = resetTimer;

    function resetTimer() {
        clearTimeout(timer);
        timer = setTimeout(logout, 108000000);
    }

    function logout() {
        dotnetHelper.invokeMethodAsync("Logout");
    }
}