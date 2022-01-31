using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Deviot.Administration.Api.Bases
{
    [ExcludeFromCodeCoverage]

    public abstract class EntityBaseModelView
    {
        [Required]
        public String Id { get; set; }
    }
}
