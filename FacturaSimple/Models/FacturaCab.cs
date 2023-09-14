namespace FacturaSimple.Models
{
    public class FacturaCab
    {
        public int FacturaCabId { get; set; }

        public DateTime Fecha { get; set; }

        public List<FacturaDet> Detalle { get; set; }

    }
}
