namespace Dev_Blog.Models.Interfaces
{
    public interface IValidator
    {
        /// <summary>
        /// Checks if username already exists in database
        /// </summary>
        /// <param name="userName">Username to check for</param>
        /// <returns>boolean</returns>
        bool UserNameExists(string userName);

        /// <summary>
        /// Checks if email already exists in database
        /// </summary>
        /// <param name="email">Email to check for</param>
        /// <returns>boolean</returns>
        bool EmailExists(string email);

        /// <summary>
        /// Ensures a user cannot create CSS attacks
        /// </summary>
        /// <param name="comment">The comment to validate</param>
        /// <returns>String</returns>
        string ValidateComment(string comment);
    }
}