using FacturaSimple.Models;
using FluentValidation;

namespace FacturaSimple.Controllers
{

    public class ProductoValidator : AbstractValidator<Producto>
    {
        public ProductoValidator()
        {
            RuleFor(p => p.ProductoId).NotEmpty();
            RuleFor(p => p.Nombre).NotEmpty();
            RuleFor(p => p.Precio).NotEmpty();
        }
    }

}