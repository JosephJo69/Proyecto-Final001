using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.Text.Json;

namespace Proyecto001_WF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnBuscar_ClickAsync(object sender, EventArgs e)
        {
            string prompt = TB_Prompt.Text;
            if (string.IsNullOrEmpty(prompt))
            {
                MessageBox.Show("El prompt no puede estar vacío.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Enviar prompt a la API de Gemini
            string apiKey = "AIzaSyCmjIQK4RoPzvjxxepC3hBiEgTLaMkIe1k";
            string respuestaGemini = await EnviarPromptAGemini(apiKey, prompt);

            string textoRespuesta = ExtraerTextoGemini(respuestaGemini);

            // 2. Guardar en SQL Server
            string connectionString = "Server=USUARIO_PC\\SQLEXPRESS;Database=DB_ProyectoFinal;Integrated Security=True; TrustServerCertificate=True;";
            GuardarEnBaseDeDatos(connectionString, prompt, respuestaGemini);

            // 3. Crear documento Word
            string rutaWord = $"RespuestaGemini_{DateTime.Now:yyyyMMddHHmmss}.docx";
            CrearDocumentoWord(rutaWord, textoRespuesta);

            // 3b. Crear documento PowerPoint
            string rutaPptx = $"RespuestaGemini_{DateTime.Now:yyyyMMddHHmmss}.pptx";
            var ppt = new PowerPoint();
            ppt.CrearDocumentoPowerPoint(rutaPptx, textoRespuesta);

            // (Opcional) Mostrar mensaje y abrir el archivo
            MessageBox.Show($"Respuesta de Gemini guardada en: {rutaPptx}", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            AbrirArchivo(rutaPptx);
            

            MessageBox.Show($"Respuesta de Gemini guardada en: {rutaWord}", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // 4. Abrir el archivo Word automáticamente
            AbrirArchivo(rutaWord);
        }

        private async Task<string> EnviarPromptAGemini(string apiKey, string prompt)
        {
            string url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key={apiKey}";

            var body = new
            {
                contents = new[]
                {
                new {
                    parts = new object[]
                    {
                        new { text = prompt },
                    }
                }
            }
            };

            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(body), System.Text.Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    return $"Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}";
                }
            }
        }

        private string ExtraerTextoGemini(string json)
        {
            try
            {
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                var text = root
                    .GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                    .GetProperty("text")
                    .GetString();

                return text ?? "No se encontró texto en la respuesta.";
            }
            catch
            {
                return "No se pudo extraer el texto de la respuesta.";
            }
        }

        private void GuardarEnBaseDeDatos(string connectionString, string prompt, string respuesta)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            var command = new SqlCommand(
                "INSERT INTO REGISTRO_PF01 (Prompt, Resultado, Fecha) VALUES (@prompt, @respuesta, @fecha)", connection);
            command.Parameters.AddWithValue("@prompt", prompt);
            command.Parameters.AddWithValue("@respuesta", respuesta);
            command.Parameters.AddWithValue("@fecha", DateTime.Now);
            command.ExecuteNonQuery();
        }

        private void CrearDocumentoWord(string ruta, string texto)
        {
            using var wordDoc = WordprocessingDocument.Create(ruta, DocumentFormat.OpenXml.WordprocessingDocumentType.Document);
            var mainPart = wordDoc.AddMainDocumentPart();
            mainPart.Document = new Document(new Body(new Paragraph(new Run(new Text(texto)))));
        }

        private void AbrirArchivo(string ruta)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = ruta,
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo abrir el archivo automáticamente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
    

