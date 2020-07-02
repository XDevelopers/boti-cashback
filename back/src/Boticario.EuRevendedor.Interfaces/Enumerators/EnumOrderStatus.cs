using System.ComponentModel;

namespace Boticario.EuRevendedor.Interfaces.Enumerators
{
    public enum EnumOrderStatus
    {
        [Description("Em Validação")]
        OnChecking,

        [Description("Aprovado")]
        Approved,

        [Description("Reprovado")]
        Unapproved
    }
}
