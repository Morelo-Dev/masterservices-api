
namespace MasterServicesAPI.Models
{
    public class MsgResult
    {
        private List<object> notificaciones;

        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; } // Para incluir datos adicionales si es necesario

        // Constructor para respuestas de éxito
        public MsgResult(string message, object data = null)
        {
            Success = true;
            Message = message;
            Data = data;
        }

        // Constructor para respuestas de error
        public MsgResult(string message)
        {
            Success = false;
            Message = message;
            Data = null;
        }

    }
}
