using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace SistemaVentasVendedor
{
    public partial class Form1 : Form
    {
        private HttpClient client;
        private static string token = "";
        private string ultimoVendedor = "";
        private const string API_URL = "http://localhost:5282";

        public Form1()
        {
            InitializeComponent();
            client = new HttpClient();
            client.BaseAddress = new Uri(API_URL);
            this.Shown += async (s, e) => { await LoginAutomatico(); MessageBox.Show($"Token guardado: {token.Length} chars"); };
        }

        private async System.Threading.Tasks.Task LoginAutomatico()
        {
            try
            {
                var loginData = new { usuario = "admin", password = "admin123" };
                var json = JsonSerializer.Serialize(loginData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("/api/Auth/login", content);
                var jsonResp = await response.Content.ReadAsStringAsync();
                MessageBox.Show($"Login response: {jsonResp}", "Debug");
                if (response.IsSuccessStatusCode)
                {
                    var doc = JsonDocument.Parse(jsonResp);
                    token = doc.RootElement.GetProperty("token").GetString() ?? "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtMonto.Text))
            {
                MessageBox.Show("Complete todos los campos.");
                return;
            }
            if (!double.TryParse(txtMonto.Text, out double monto))
            {
                MessageBox.Show("Monto inválido.");
                return;
            }
            // Primero actualizamos el nombre del vendedor
            var updateRequest = new HttpRequestMessage(HttpMethod.Put, "/api/Vendedores/1");
            var updateData = new { nombre = txtNombre.Text };
            updateRequest.Content = new StringContent(JsonSerializer.Serialize(updateData), Encoding.UTF8, "application/json");
            updateRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            await client.SendAsync(updateRequest);

            var venta = new { idVendedor = 1, montoVenta = monto };
            var json = JsonSerializer.Serialize(venta);
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/Ventas/registrar");
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            try
            {
                MessageBox.Show($"Token al registrar: '{token.Substring(0, Math.Min(20, token.Length))}' Largo: {token.Length}");
                var response = await client.SendAsync(request);
                var respText = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    double comision = CalcularComision(monto);
                    lstVendedores.Items.Add($"{txtNombre.Text} | Q{monto:F2} | Comisión: Q{comision:F2}");
                    ultimoVendedor = txtNombre.Text;
                    txtNombre.Clear();
                    txtMonto.Clear();
                    await CargarResumen();
                    MessageBox.Show("¡Venta registrada!");
                }
                else
                {
                    MessageBox.Show($"Error {response.StatusCode}: {respText}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private double CalcularComision(double monto)
        {
            if (monto >= 10000) return monto * 0.10;
            else if (monto >= 5000) return monto * 0.07;
            else if (monto >= 1000) return monto * 0.05;
            else return monto * 0.03;
        }

        private async System.Threading.Tasks.Task CargarResumen()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "/api/Ventas/resumen");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var ventas = JsonSerializer.Deserialize<List<JsonElement>>(json);
                    if (ventas != null && ventas.Count > 0)
                    {
                        double totalVentas = 0;
                        double mayorVenta = 0;
                        string mejorVendedor = "";
                        foreach (var v in ventas)
                        {
                            double montoV = v.GetProperty("montoVenta").GetDouble();
                            totalVentas += montoV;
                            if (montoV > mayorVenta)
                            {
                                mayorVenta = montoV;
                                mejorVendedor = v.GetProperty("nombreVendedor").GetString() ?? txtNombre.Text;
                            }
                        }
                        lblResumen.Text = $"Total: {ventas.Count} ventas | Total: Q{totalVentas:F2} | Mejor Vendedor: {ultimoVendedor} (Q{mayorVenta:F2})";
                    }
                }
            }
            catch { }
        }
    }
}