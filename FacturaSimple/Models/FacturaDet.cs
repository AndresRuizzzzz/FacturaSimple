namespace FacturaSimple.Models
{
    public class FacturaDet
    {
        public int FacturaDetId { get; set; }

        public int FacturaCabId { get; set; }

        public int ProductoId { get; set; }

        public int Cantidad { get; set; }

        public decimal Precio { get; set; }
    }
}
