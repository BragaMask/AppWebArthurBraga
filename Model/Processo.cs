using System.ComponentModel.DataAnnotations;

namespace AppWebArthurBraga.Model
{
    public class Processo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O número do processo é obrigatório.")]
        [StringLength(200, ErrorMessage = "O número deve ter no máximo 200 caracteres.")]
        public string Numero { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data é obrigatória.")]
        public DateOnly? Data { get; set; }

        [Required(ErrorMessage = "O interessado é obrigatório.")]
        [StringLength(200, ErrorMessage = "O interessado deve ter no máximo 200 caracteres.")]
        public string Interessado { get; set; } = string.Empty;

        [Required(ErrorMessage = "O assunto é obrigatório.")]
        [StringLength(300, ErrorMessage = "O assunto deve ter no máximo 300 caracteres.")]
        public string Assunto { get; set; } = string.Empty;

        [StringLength(2000, ErrorMessage = "A descrição deve ter no máximo 2000 caracteres.")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "A situação é obrigatória.")]
        [StringLength(50, ErrorMessage = "A situação deve ter no máximo 50 caracteres.")]
        public string Situacao { get; set; } = "Aberto";
    }
}
