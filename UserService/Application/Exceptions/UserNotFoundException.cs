namespace Application.Exceptions
{
    public class UserNotFoundException : ApplicationException
    {
        public UserNotFoundException(string name, object key) : base($"Entity {name} - {key} is not found.")
        {

        }
    }
}
