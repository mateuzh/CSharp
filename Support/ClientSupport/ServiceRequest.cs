class ServiceRequest
{
    public int Id {get; set;}
    public int UserId {get; set;}
    public int AgentId {get; set;}
    public DateTime OpeningTime {get; set;}
    public DateTime CloseTime {get; set;}
    
    public ServiceRequest(int id, int userId, int agentId, DateTime openingTime)
    {
        Id = id;
        UserId = userId;
        AgentId = agentId;
        OpeningTime = openingTime;
    }
}