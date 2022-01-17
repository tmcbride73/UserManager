namespace UserManager.Domain
{
    public class UserFilter
    {
        public long? SeqNum { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Activity { get; set; }
        public string Subactivity { get; set; }

    }
}
