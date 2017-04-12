namespace TrialApp.ServiceClient.ServiceResponse
{
    public class GetTrialTokenBackResponse
    {

        public tuple tuple { get; set; }

    }

    public class tuple
    {

        public old old { get; set; }
    }
    public class old
    {
        public GetTrialTokeBack GetTrialTokenBack { get; set; }
    }
    public class GetTrialTokeBack
    {
        public string GetTrialTokenBack { get; set; }
    }
}
