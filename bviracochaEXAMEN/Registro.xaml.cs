namespace bviracochaEXAMEN;

public partial class Registro : ContentPage
{
    private string usuarioActual;
    private double pagoMensualCalculado = 0;
    private double montoInicialIngresado = 0;

    public Registro(string usuario)
    {
        InitializeComponent();
        usuarioActual = usuario;
        lblUsuarioConectado.Text = $"Usuario conectado: {usuarioActual}";
    }

    private void OnCalcularClicked(object sender, EventArgs e)
    {
        if (double.TryParse(txtMontoInicial.Text, out montoInicialIngresado))
        {
            if (montoInicialIngresado >= 0 && montoInicialIngresado <= 1500)
            {
                double restante = 1500 - montoInicialIngresado;
                double recargoTotal = 1500 * 0.04; // 4% de 1500 = 60
                pagoMensualCalculado = (restante + recargoTotal) / 4;

                txtPagoMensual.Text = pagoMensualCalculado.ToString("F2");
            }
            else
            {
                DisplayAlert("Error", "El monto inicial no puede exceder $1500 ni ser menor a $0", "OK");
            }
        }
        else
        {
            DisplayAlert("Error", "Ingrese un monto inicial válido", "OK");
        }
    }

    private async void OnVerResumenClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
            string.IsNullOrWhiteSpace(txtApellido.Text) ||
            string.IsNullOrWhiteSpace(txtEdad.Text) ||
            pkPais.SelectedItem == null ||
            pkCiudad.SelectedItem == null ||
            string.IsNullOrWhiteSpace(txtPagoMensual.Text))
        {
            await DisplayAlert("Atención", "Por favor llene todos los campos y calcule el pago mensual.", "OK");
            return;
        }

        double pagoTotal = montoInicialIngresado + (pagoMensualCalculado * 4);

        await Navigation.PushAsync(new Resumen(
            usuarioActual,
            txtNombre.Text,
            txtApellido.Text,
            txtEdad.Text,
            dpFecha.Date?.ToString("dd/MM/yyyy") ?? string.Empty,
            pkCiudad.SelectedItem?.ToString() ?? string.Empty,
            pkPais.SelectedItem?.ToString() ?? string.Empty,
            montoInicialIngresado.ToString("F2"),
            pagoMensualCalculado.ToString("F2"),
            pagoTotal.ToString("F2")
        ));
    }
}




