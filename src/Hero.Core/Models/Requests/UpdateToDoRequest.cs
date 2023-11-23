using Core.Enums;

namespace Core.Models.Requests
{
    public class UpdateToDoRequest
    {
        public string Nome { get; set; }
        public string? Descricao { get; set; }
        public EnumStatus Status { get; set; }
    }
}
