using static ShortWeb.Utility.StaticData;

namespace ShortWeb.Model.Models.Dtos
{
    // General purpose model that is passed to all the api services.
    public class RequestDto
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }
    }
}
