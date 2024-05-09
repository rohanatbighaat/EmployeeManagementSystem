namespace EmployeeManagementSystem.Exceptions
{
    [Serializable]
    public class InvalidPhoneNumberException : Exception
    {
        public string PhoneNumber { get; }

        public InvalidPhoneNumberException() { }

        public InvalidPhoneNumberException(string message)
            : base(message) { }

        public InvalidPhoneNumberException(string message, Exception inner)
            : base(message, inner) { }

        public InvalidPhoneNumberException(string message, string phonenumber)
            : this(message)
        {
            PhoneNumber = phonenumber;
        }
    }
}
