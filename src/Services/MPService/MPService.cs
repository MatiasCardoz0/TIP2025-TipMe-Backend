using MercadoPago.Client.Preference;
using MercadoPago.Resource.Preference;
using TipMeBackend.Controllers.DTOs;
using TipMeBackend.Models;

namespace TipMeBackend.Services.MPService
{
    public class MPService : IMPService
    {

        public MPService(Context context) { }

        public async Task<Response<PreferenceID>> GetPreferenceId(decimal monto)
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
                            UnitPrice = monto,
                        },
                    },
                    BackUrls = new PreferenceBackUrlsRequest
                    {
                        Success = "https://03f4-186-57-136-121.ngrok-free.app/home",
                        Failure = "https://03f4-186-57-136-121.ngrok-free.app/home",
                        Pending = "https://03f4-186-57-136-121.ngrok-free.app/home",
                    },
                    AutoReturn = "approved",
                };

                // Crea la preferencia usando el client
                var client = new PreferenceClient();
                Preference preference = await client.CreateAsync(request);

                if (preference != null)
                {
                    id = preference.Id;
                    return new Response<PreferenceID>(new PreferenceID(id),200);
                }
                else
                {
                    return new Response<PreferenceID>("Ha ocurrido un error al generar la preferencia.", 404);
                }
            }
            catch(Exception ex)
            {
                return new Response<PreferenceID>($"Error: {ex.Message}", 400);
            }
        }
    }
}
