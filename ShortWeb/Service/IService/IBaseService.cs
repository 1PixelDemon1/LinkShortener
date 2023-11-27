using ShortWeb.Model.Models.Dtos;

namespace ShortWeb.Service.IService
{
    public interface IBaseService
    {
        public Task<ResponseDto?> SendAsync(RequestDto requestDto);
    }
}
