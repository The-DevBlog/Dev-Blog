namespace devblog.Interfaces
{
    public interface IEmailService
    {
        /// <summary>
        /// Emails a welcome message to a newly registered user
        /// </summary>
        /// <param name="email">User's email</param>
        /// <returns>Successful completion of task</returns>
        Task Welcome(string email);
    }
}
