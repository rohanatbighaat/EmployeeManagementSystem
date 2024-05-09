using System.Text;

namespace EmployeeManagementSystem.Helpers
{
    public class ScnEncoder
    {
        public string EncodeScn(string scn)
        {
            byte[] bytesToEncode = Encoding.UTF8.GetBytes(scn);
            // Encode the bytes using Base64 encoding
            string encodedScn = Convert.ToBase64String(bytesToEncode);
            return encodedScn; // For demonstration, return as is
        }

        // Define a method to decode the Scn
        public string DecodeScn(string encodedScn)
        {
            byte[] decodedBytes = Convert.FromBase64String(encodedScn);
            // Convert the bytes back to string
            string scn = Encoding.UTF8.GetString(decodedBytes);
            return scn; // For demonstration, return as is
        }
    }
}
