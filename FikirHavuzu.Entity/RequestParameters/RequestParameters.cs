namespace FikirHavuzu.Entity.RequestParameters
{
    public abstract class RequestParameters
    {
        // Güvenlik: Bir sayfada en fazla 20 kayıt çekilebilir
        protected const int maxPageSize = 20;

        // Varsayılan olarak 1. sayfadan başlar
        public int PageNumber { get; set; } = 1;
    }
}
