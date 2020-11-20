namespace DeveloperCourse.SecondTask.Infrastructure.Identity
{
    public interface IUserContext
    {
        public IIdentityDto Identity { get; }

        public bool IsAuthenticated { get; }
    }
}