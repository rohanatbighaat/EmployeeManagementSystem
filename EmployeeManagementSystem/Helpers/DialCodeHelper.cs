using EmployeeManagementSystem.Model;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;

namespace EmployeeManagementSystem.Helpers
{
    public class DialCodeHelper
    {
       
        public async Task<string> GetDialCodeAsync(string countryName)
        {
            string apiUrl = "https://country-code-au6g.vercel.app/Country.json";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();
                        // Deserialize JSON response
                        var countries = JsonConvert.DeserializeObject<List<Country>>(jsonString);
                        //int i = 0;
                        //foreach (var county in countries)
                        //{
                          //  i++;
                            //Console.WriteLine($"Name: {county.Name}, Dial Code: {county.DialCode}");
                            //if (i == 5) break;
                        //}

                        // Find the country with matching name
                        var country = countries.FirstOrDefault(c => c.Name.Equals(countryName, StringComparison.OrdinalIgnoreCase));

                        if (country != null)
                        {
                            return country.Dial_Code;
                        }
                        else
                        {
                            throw new Exception("Country not found or dial code not available.");
                        }
                    }
                    else
                    {
                        throw new Exception("Failed to fetch country data from the API.");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error fetching dial code: " + ex.Message);
                }
            }
        }

    }
}
