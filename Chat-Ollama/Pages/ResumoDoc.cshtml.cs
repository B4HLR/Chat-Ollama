using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;
using System.Text;
using UglyToad.PdfPig;

namespace Chat_Ollama.Pages
{
    public class ResumoDocModel : PageModel
    {
        private readonly ILogger<ResumoDocModel> _logger;
        private readonly ResumidorDeDocumentos _resumidor;

        public ResumoDocModel(ILogger<ResumoDocModel> logger, ResumidorDeDocumentos Resumidor)
        {
            _logger = logger;
            _resumidor = Resumidor;
        }

        [BindProperty]
        public IFormFile Termo { get; set; }

        public string RespostaExplicada { get; set; }

        public void OnGet()
        {
        }

        private string ExtrairTextoDoPdf(IFormFile arquivoPdf)
        {
            using var pdfDocument = PdfDocument.Open(arquivoPdf.OpenReadStream());
            var textoCompleto = new StringBuilder();

            foreach (var pagina in pdfDocument.GetPages())
            {
                textoCompleto.AppendLine(pagina.Text);
            }

            return textoCompleto.ToString();
        }

        public async Task OnPostAsync()
        {
            if (Termo != null && Termo.Length > 0)
            {
                string textoExtraido = ExtrairTextoDoPdf(Termo);
                RespostaExplicada = await _resumidor.Resumir(textoExtraido);
            }
            else
            {
                ModelState.AddModelError("Termo", "Por favor, envie um arquivo válido.");
            }
        }
    }
}
