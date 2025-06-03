using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chat_Ollama.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;
        private readonly AnaliseDeSentimento _analisador;

        public PrivacyModel(ILogger<PrivacyModel> logger, AnaliseDeSentimento Analizador)
        {
            _logger = logger;
            _analisador = Analizador;
        }

        [BindProperty]
        public string Termo { get; set; }

        public string RespostaExplicada { get; set; }

        public void OnGet()
        {
        }

        public async Task OnPostAsync()
        {
            if (!string.IsNullOrEmpty(Termo))
            {
                RespostaExplicada = await _analisador.Sentido(Termo);
            }
        }
    }
}
