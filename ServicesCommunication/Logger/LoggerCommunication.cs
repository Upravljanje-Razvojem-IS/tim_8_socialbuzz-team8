using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Text;

namespace ServicesCommunication.Logger
{
    public class LoggerCommunication
    {
        private readonly HttpWebRequest _httpWebRequest;
        private readonly string _url = "https://localhost:44302/api/Logger";

        public LoggerCommunication()
        {
            this._httpWebRequest = WebRequest.CreateHttp(this._url);
            this._httpWebRequest.Method = "POST";
        }

        public bool logAction(string description)
        {
            MessageDto messageDTO = new MessageDto();
            messageDTO.message = description;
            string json = JsonConvert.SerializeObject(messageDTO);

            byte[] byteArray = Encoding.UTF8.GetBytes(json);

            this._httpWebRequest.ContentType = "application/json";
            this._httpWebRequest.ContentLength = byteArray.Length;

            Stream dataStream = this._httpWebRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse response = this._httpWebRequest.GetResponse();

            HttpWebResponse httpWebResponse = (HttpWebResponse)response;

            return httpWebResponse.StatusCode == HttpStatusCode.Created;
        }
    }
}
