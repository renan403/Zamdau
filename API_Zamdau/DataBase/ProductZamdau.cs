using API_Zamdau.User;
using Firebase.Database.Query;
using Newtonsoft.Json;

namespace API_Zamdau.DataBase
{
    public class ProductZamdau : BaseFirebase
    {


        public async Task<string> GetProductAsync(string guid)
        {
            try
            {
                await InitClientAdmin();
                var product = await _client.Child("products").Child(guid).OnceSingleAsync<AP_Product>();
                var rate = await GetRatingProduct(guid);

                product.Rating = rate.Item1;
                product.ReviewCount = rate.Item2;

                var json = JsonConvert.SerializeObject(product);
                return json;
            }
            catch (Exception ex)
            {

                return string.Empty;
            }

        }
        public async Task<string> GetAllProductAsync()
        {
            try
            {
                await InitClientAdmin();
                var product = (await _client.Child("products").OnceAsync<AP_Product>()).Select(b => b.Object);
                var json = JsonConvert.SerializeObject(product);
                return json;
            }
            catch (Exception ex)
            {

                return string.Empty;
            }

        }
        private async Task<Tuple<int, int>> GetRatingProduct(string guid)
        {
            try
            {
                
                var Allobjs = (await _client.Child("products").Child(guid).Child("Comments").OnceSingleAsync<Dictionary<string, AP_Comment>>()).Values;

                var Allcomments = Allobjs.Select(p => p.Rating).ToList();

                if (Allcomments.Any())
                {
                    var averageRating = Allcomments.Average();

                    return new Tuple<int, int>((int)Math.Round(averageRating), Allcomments.Count);
                }

                return new Tuple<int, int>(0,0);
            }
            catch (Exception)
            {

                return new Tuple<int, int>(0, 0);
            }
          
            

           
        }

    }
}
