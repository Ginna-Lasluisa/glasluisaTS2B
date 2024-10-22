using System.Text.RegularExpressions;

namespace glasluisaTS2B.Views;

public partial class vHome : ContentPage
{
    public vHome()
    {
        InitializeComponent();
    }

    private void btnCalcular_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtNotaSeg1.Text) ||
            string.IsNullOrWhiteSpace(txtNotaExa1.Text) ||
            string.IsNullOrWhiteSpace(txtNotaSeg2.Text) ||
            string.IsNullOrWhiteSpace(txtNotaExa2.Text))
        {
            DisplayAlert("Error", "Todos los campos son obligatorios.", "OK");
            return;
        }

        if (pkEstudiantes.SelectedIndex == 0)
        {
            DisplayAlert("Aviso", "Se debe, seleccionar un Alumno", "OK");
            pkEstudiantes.SelectedIndex = -1;
        }
        else
        {
            if (pkEstudiantes.SelectedIndex != -1)
            {
                string NombreEstudiante = pkEstudiantes.SelectedItem.ToString();

                double NotaSeg1 = Convert.ToDouble(txtNotaSeg1.Text) * 0.3;
                double NotaExa1 = Convert.ToDouble(txtNotaExa1.Text) * 0.2;
                double NotaParcial1 = NotaSeg1 + NotaExa1;

                double NotaSeg2 = Convert.ToDouble(txtNotaSeg2.Text) * 0.3;
                double NotaExa2 = Convert.ToDouble(txtNotaExa2.Text) * 0.2;
                double NotaParcial2 = NotaSeg2 + NotaExa2;

                double NotaFinal = NotaParcial1 + NotaParcial2;

                lblNotaParcial1.Text = $"Nota Parcial 1: {NotaParcial1:F2}"; // F2 para formato de 2 decimales
                lblNotaParcial2.Text = $"Nota Parcial 2: {NotaParcial2:F2}";
                lblNotaFinal.Text = $"Nota Final: {NotaFinal:F2}";

                string fecha = dpkFecha.Date.ToString("d");
                if (NotaFinal >= 7)
                {
                    string mensaje = $"Nombre: {NombreEstudiante}\n" +
                                 $"Fecha: {fecha}\n" +
                                 $"Nota Parcial 1: {NotaParcial1:F2}\n" +
                                 $"Nota Parcial 2: {NotaParcial2:F2}\n" +
                                 $"Nota Final: {NotaFinal:F2}\n" +
                                 $"Estado: Aprobado";

                    DisplayAlert("Nota del Estudiante", mensaje, "OK");
                }
                if (NotaFinal >= 5 && NotaFinal <= 6.9)
                {
                    string mensaje = $"Nombre: {NombreEstudiante}\n" +
                                 $"Fecha: {fecha}\n" +
                                 $"Nota Parcial 1: {NotaParcial1:F2}\n" +
                                 $"Nota Parcial 2: {NotaParcial2:F2}\n" +
                                 $"Nota Final: {NotaFinal:F2}\n" +
                                 $"Estado: Complementario";

                    DisplayAlert("Nota del Estudiante", mensaje, "OK");
                }
                if (NotaFinal < 5)
                {
                    string mensaje = $"Nombre: {NombreEstudiante}\n" +
                                 $"Fecha: {fecha}\n" +
                                 $"Nota Parcial 1: {NotaParcial1:F2}\n" +
                                 $"Nota Parcial 2: {NotaParcial2:F2}\n" +
                                 $"Nota Final: {NotaFinal:F2}\n" +
                                 $"Estado: Reprobado";

                    DisplayAlert("Nota del Estudiante", mensaje, "OK");
                }
            }
            else
            {
                DisplayAlert("Error", "Todos los campos son obligatorios.", "OK");
                return;
            }
        }
        pkEstudiantes.SelectedIndex = -1;
        txtNotaSeg1.Text = string.Empty;
        txtNotaExa1.Text = string.Empty;
        txtNotaSeg2.Text = string.Empty;
        txtNotaExa2.Text = string.Empty;
        lblNotaParcial1.Text = string.Empty;
        lblNotaParcial2.Text = string.Empty;
        lblNotaFinal.Text = string.Empty;
    }

    private void ControlRango(object sender, TextChangedEventArgs e)
    {
        Regex regex = new Regex("^[0-9,]*$");

        if (!regex.IsMatch(e.NewTextValue))
        {
            DisplayAlert("Entrada inválida", "Solo se permiten números y comas para numeros decimales", "OK");
            ((Entry)sender).Text = string.Empty;
        }
        else
        {
            if (double.TryParse(e.NewTextValue, out double valorIngresado) && valorIngresado < 0.1 || valorIngresado > 10)
            {
                DisplayAlert("Error", "El valor debe estar entre 0,1 y 10", "OK");
                ((Entry)sender).Text = string.Empty;
            }
        }
    }
}
