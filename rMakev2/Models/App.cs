using Blazorise;

namespace rMakev2.Models
{
    public class App
    {
        public App(string appId)
        {
            appId = appId ?? throw new Exceptions("App Key is required");
            Id = appId;
            Portfolio = new Portfolio(this, "codename-rebel-creator");

            //Ui = new Ui(this);
        }
        public string Id { get; set; }
        public Portfolio Portfolio { get; set; }
        //public Ui Ui { get; set; }
        public Modal? SaveModal { get; set; }
        public Modal? PublishModal { get; set; }
        public Modal? MergeModal { get; set; }

        static string HashString(string text, string salt = "")
        {
            if (String.IsNullOrEmpty(text))
            {
                return String.Empty;
            }

            // Uses SHA256 to create the hash
            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                // Convert the string to a byte array first, to be processed
                byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(text + salt);
                byte[] hashBytes = sha.ComputeHash(textBytes);

                // Convert back to a string, removing the '-' that BitConverter adds
                string hash = BitConverter
                    .ToString(hashBytes)
                    .Replace("-", String.Empty);

                return hash;
            }


        }
    }

 
}
