namespace VentasAPI.Services
{
    public class ComisionService
    {
        public decimal ObtenerPorcentajeComision(decimal monto)
        {
            if (monto >= 5000)
            {
                return 0.10m;
            }
            else if (monto >= 3000)
            {
                return 0.07m;
            }
            else if (monto >= 1000)
            {
                return 0.05m;
            }
            else
            {
                return 0.03m;
            }
        }

        public decimal CalcularComision(decimal monto)
        {
            var porcentaje = ObtenerPorcentajeComision(monto);
            return monto * porcentaje;
        }
    }
}