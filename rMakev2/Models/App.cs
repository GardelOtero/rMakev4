namespace rMakev2.Models
{
    public class App
    {
        public App(string appId, string dataToken)
        {
            appId = appId ?? throw new Exceptions("App Key is required");
            Id = appId;
            Portfolio = new Portfolio(this);
            //Ui = new Ui(this);
            if (dataToken == null)
                dataToken = Guid.NewGuid().ToString();
            else
                DataToken = dataToken;
        }
        public string Id { get; set; }
        public string DataToken;
        public Portfolio Portfolio { get; set; }
        //public Ui Ui { get; set; }

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
