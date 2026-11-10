namespace bviracochaEXAMEN;

public partial class Login : ContentPage
{
    // Matriz 2x2 para usuarios y contraseñas
    private string[,] usuariosPermitidos = new string[2, 2] {
        { "estudiante", "moviles" },
        { "suda", "2025" }
    };

    public Login()
    {
        InitializeComponent();
    }

    [Obsolete]
    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string user = txtUsuario.Text?.Trim() ?? "";
        string pass = txtContrasena.Text?.Trim() ?? "";

        bool esValido = false;

        for (int i = 0; i < 2; i++)
        {
            if (usuariosPermitidos[i, 0] == user && usuariosPermitidos[i, 1] == pass)
            {
                esValido = true;
                break;
            }
        }

        if (esValido)
        {
            // Redirigir a Registro pasando el usuario
            await Navigation.PushAsync(new Registro(user));
        }
        else
        {
            await DisplayAlert("Error", "Dato incorrecto. Verifique sus credenciales.", "OK");
        }
    }
}
