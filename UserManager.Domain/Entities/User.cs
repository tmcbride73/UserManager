namespace UserManager.Domain
{
    public class User
    {
        public long? SeqNum { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Activity { get; set; }
    }
}
