using FikirHavuzu.Entity.Entities;

namespace FikirHavuzu.Repository.Extensions
{
    public static class EvaluationRepositoryExtension
    {
        public static IQueryable<Evaluation> FilteredByIdeaId(this IQueryable<Evaluation> evaluations, int ideaId)
        {
            return evaluations.Where(e => e.IdeaId == ideaId);
        }

        public static IQueryable<Evaluation> FilteredByScore(this IQueryable<Evaluation> evaluations, int? score)
        {
            if (score is null)
                return evaluations;

            return evaluations.Where(e => e.Score == score);
        }

        public static IQueryable<Evaluation> FilteredByIsApproved(this IQueryable<Evaluation> evaluations, bool? isApproved)
        {
            if (isApproved is null)
                return evaluations;

            return evaluations.Where(e => e.IsApproved == isApproved);
        }

        public static IQueryable<Evaluation> FilteredByComment(this IQueryable<Evaluation> evaluations, string? comment)
        {
            if (string.IsNullOrWhiteSpace(comment))
                return evaluations;

            var lowerCaseTerm = comment.Trim().ToLower();
            return evaluations.Where(e => e.Comment.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<Evaluation> FilteredByFullName(this IQueryable<Evaluation> evaluations, string? fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return evaluations;

            var lowerCaseName = fullName.Trim().ToLower();
            return evaluations.Where(e => (e.EvaluatedByUser.FirstName + " " + e.EvaluatedByUser.LastName).ToLower().Contains(lowerCaseName));
        }

        public static IQueryable<Evaluation> FilteredByDateRange(this IQueryable<Evaluation> evaluations, DateTime? startDate, DateTime? endDate)
        {
            if (startDate.HasValue)
                evaluations = evaluations.Where(e => e.EvaluationDate.Date >= startDate.Value.Date);

            if (endDate.HasValue)
                evaluations = evaluations.Where(e => e.EvaluationDate.Date <= endDate.Value.Date);

            return evaluations;
        }

        public static IQueryable<Evaluation> FilteredByEmail(this IQueryable<Evaluation> evaluations, string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return evaluations;

            var lowerCaseTerm = email.Trim().ToLower();

            return evaluations.Where(e => (e.EvaluatedByUser.Email).ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<Evaluation> ToPaginate(this IQueryable<Evaluation> evaluations, int pageNumber, int pageSize)
        {
            return evaluations
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
        }
    }
}