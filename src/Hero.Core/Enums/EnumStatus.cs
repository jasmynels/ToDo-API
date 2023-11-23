using System.ComponentModel;

namespace Core.Enums
{
    public enum EnumStatus
    {
        [Description("A Fazer")]
        AFazer,
        [Description("Feito")]
        Feito,
        [Description("Revisar")]
        Revisar
    }
}
