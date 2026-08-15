using FikirHavuzu.Entity.Entities;

namespace FikirHavuzu.Repository.Extensions
{
    public static class IdeaRepositoryExtension
    {
        // 1. Kategoriye Göre Filtreleme
        public static IQueryable<Idea> FilteredByCategoryId(this IQueryable<Idea> ideas, int? categoryId)
        {
            if (categoryId is null)
                return ideas;

            return ideas.Where(i => i.CategoryId == categoryId);
        }

        // 2. Fikir Başlığı veya Açıklamasında Arama
        public static IQueryable<Idea> FilteredBySearchQuery(this IQueryable<Idea> ideas, string? searchQuery)
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
                return ideas;

            var lowerCaseTerm = searchQuery.Trim().ToLower();

            // Hem başlıkta (Title) hem de açıklamada (Description) arar
            return ideas.Where(i => i.Title.ToLower().Contains(lowerCaseTerm) ||
                                    i.Description.ToLower().Contains(lowerCaseTerm));
        }

        // 3. Ekleyen Kişinin Adı ve Soyadına Göre Arama
        public static IQueryable<Idea> FilteredByFullName(this IQueryable<Idea> ideas, string? fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return ideas;

            var lowerCaseName = fullName.Trim().ToLower();

            // EF Core arkada FirstName ve LastName'i birleştirip arama yapar
            return ideas.Where(i => (i.User.FirstName + " " + i.User.LastName).ToLower().Contains(lowerCaseName));
        }

        // 4. Tarihe Göre Filtreleme
        public static IQueryable<Idea> FilteredByDateRange(this IQueryable<Idea> ideas, DateTime? startDate, DateTime? endDate)
        {
            if (startDate.HasValue)
                ideas=ideas.Where(i=>i.CreatedAt.Date>=startDate.Value.Date);

            if (endDate.HasValue)
                ideas=ideas.Where(i=>i.CreatedAt.Date<=endDate.Value.Date);

            return ideas;
        }

        // 5. Sayfalama (Pagination)
        public static IQueryable<Idea> ToPaginate(this IQueryable<Idea> ideas, int pageNumber, int pageSize)
        {
            return ideas
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
        }

        // 6. Emaile göre filtreleme
        public static IQueryable<Idea> FilteredByEmail(this IQueryable<Idea> ideas, string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return ideas;

            var lowerCaseTerm = email.Trim().ToLower();

            return ideas.Where(i => (i.User.Email).ToLower().Contains(lowerCaseTerm));
        }

    }
}
