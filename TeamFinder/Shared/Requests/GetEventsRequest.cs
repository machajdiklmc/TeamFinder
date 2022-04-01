namespace TeamFinder.Shared.Requests;

public class GetEventsRequest
{
    public GetEventsRequest(GetEventsRequestOrderBy orderBy)
    {
        OrderBy = orderBy;
    }

    public GetEventsRequestOrderBy OrderBy { get; set; }
    public string? Name { get; set; }
    
}

public enum GetEventsRequestOrderBy
{
    Name, Date
}