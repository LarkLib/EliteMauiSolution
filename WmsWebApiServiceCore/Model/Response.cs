namespace Wms.Web.Api.Service.Model
{
    public class Response<T>
    {
        public int Code { get; set; } = HttpStatus.SUCCESS;
        public string Msg { get; set; } = "success";
        public T? Data { get; set; }
        public void Success(T data, string msg = "")
        {
            Code = HttpStatus.SUCCESS;
            Msg = msg;
            Data = data;
        }
    }
}
