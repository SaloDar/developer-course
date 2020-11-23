namespace DeveloperCourse.SecondLesson.Common.Identity.Interfaces
{
    public interface IUserContext
    {
        public IIdentityDto Identity { get; }

        public bool IsAuthenticated { get; }
    }
}