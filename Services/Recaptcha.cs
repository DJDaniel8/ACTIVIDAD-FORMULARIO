using Azure.Core;
using Newtonsoft.Json.Linq;
using System.Net;

namespace ACTIVIDAD_FORMULARIO.Services
{
    public class Recaptcha
    {
        private IConfiguration configuration;

        public Recaptcha(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool IsRecaptchaValid(HttpRequest requestbase)
        {
            var result = false;
            var recaptchaResponse = requestbase.Form["g-recaptcha-response"];
            var secretKey = configuration.GetSection("Recaptcha").GetSection("ClaveSecreta").Value;
            var apiUrl = "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}";
            var requestUri = string.Format(apiUrl, secretKey, recaptchaResponse);
            var request = (HttpWebRequest)WebRequest.Create(requestUri);

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    JObject jResponse = JObject.Parse(stream.ReadToEnd());
                    var isSuccess = jResponse.Value<bool>("success");
                    result = (isSuccess) ? true : false;
                }
            }
            return result;
        }
    }
}
