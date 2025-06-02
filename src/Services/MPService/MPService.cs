using MercadoPago.Client.Preference;
using MercadoPago.Resource.Preference;
using TipMeBackend.Controllers.DTOs;
using TipMeBackend.Models;

namespace TipMeBackend.Services.MPService
{
    public class MPService : IMPService
    {

        public MPService(Context context) { }

        public async Task<Response<string>> GetPreferenceId(PropinaDTO propinaDTO)
        {
            try
            {
                string id = "";

                // Crea el objeto de request de la preference
                var request = new PreferenceRequest
                {
                    Items = new List<PreferenceItemRequest>
                {
                    new PreferenceItemRequest
                    {
                        Title = "Propina",
                        Quantity = 1,
                        CurrencyId = "ARS",
                        UnitPrice = propinaDTO.Monto,
                    },
                },
                };

                // Crea la preferencia usando el client
                var client = new PreferenceClient();
                Preference preference = await client.CreateAsync(request);

                if (preference != null)
                {
                    return new Response<string>(id,200);
                }
                else
                {
                    return new Response<string>("Ha ocurrido un error al generar la preferencia.", 404);
                }
            }
            catch(Exception ex)
            {
                return new Response<string>($"Error: {ex.Message}", 400);
            }
        }
    }
}
