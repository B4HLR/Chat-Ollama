using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chat_Ollama.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ExplicadorDeTermos _explicador;

        public IndexModel(ILogger<IndexModel> logger, ExplicadorDeTermos explicador)
        {
            _logger = logger;
            _explicador = explicador;
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
                RespostaExplicada = await _explicador.Explica(Termo);
            }
        }
    }
}
