namespace FikirHavuzu.Service.Contracts
{
    public interface IServiceManager
    {
        IAuthService AuthService { get; }

        ICategoryService CategoryService { get; }

        IIdeaService IdeaService { get; }
    }
}
