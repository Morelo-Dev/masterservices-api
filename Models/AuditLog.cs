namespace MasterServicesAPI.Models
{
    public class AuditLog
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string UserName { get; set; }
        public string TableName { get; set; }
        public string Action { get; set; }
        public string Changes { get; set; }
        public DateTime Timestamp { get; set; }
        public string? AdditionalInfo { get; set; }

        public virtual Usuario Usuario { get; set; }
    }

}
