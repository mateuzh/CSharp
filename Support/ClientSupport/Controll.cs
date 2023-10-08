class SupportControll
{
    public int UserId {get; set;}
    public int AgentId {get; set;}
    public int TicketId {get; set;}
    public List<User> Users {get; set;}
    public List<SupportAgent> Agents {get; set;}
    public List<ServiceRequest> ServicesRequest {get; set;}

    public SupportControll()
    {
        UserId = 0;
        AgentId = 0;
        Users = new List<User>();
        Agents = new List<SupportAgent>();
        ServicesRequest = new List<ServiceRequest>();
    }

    public string GetTicketsList(){
        string output = "";
        foreach (var sr in ServicesRequest)
        {
            var serviceRequest = GetTicketById(sr.Id);
            output += serviceRequest.Id + ", " + serviceRequest.UserId + ", " + serviceRequest.AgentId + "\n";
        }
        return output;
    }

    public string GetUsersList(){
        string output = "";
        foreach (var u in Users)
        {
            var user = GetUserById(u.Id);
            output += user.Id + ", " + user.Name + ", " + user.Email + ", " + user.Cellphone + "\n";
        }
        return output;
    }

    public string GetAgentsList(){
        string output = "";
        foreach (var a in Agents)
        {
            var agent = GetAgentById(a.Id);
            output += agent.Id + ", " + agent.Name + ", " + agent.Email + ", " + agent.Cellphone + "\n";
        }
        return output;
    }

    public string GetTicketList(){
        string output = "";
        foreach (var srs in ServicesRequest)
        {
            var serviceRequest = GetTicketById(srs.Id);
            output += serviceRequest.Id + ", " + GetUserById(serviceRequest.UserId).Name + ", " + GetAgentById(serviceRequest.AgentId).Name + ", " + serviceRequest.OpeningTime + "\n";
        }
        return output;
    }

    public int GetUserId()
    {
        UserId++;
        return UserId;
    }

    public int GetAgentId()
    {
        AgentId++;
        return AgentId;
    }

    public int GetTicketId()
    {
        TicketId++;
        return TicketId;
    }

    public User GetUserById(int id){
        foreach (var user in Users)
        {
            if(user.Id == id){
                return user;
            }
        }
        throw new Exception("User with Id: " + id + " is not exists.");
    }

    public string GetUserByEmail(string email)
    {
        foreach (var user in Users)
        {
            if(user.Email == email)
            {
                return user.Id + ", " + user.Name + ", " +  user.Email + ", " +  user.Cellphone;
            }
        }
        throw new Exception("User with E-mail: " + email + " is not exists.");
    }

    public SupportAgent GetAgentById(int id){
        foreach (var agent in Agents)
        {
            if(agent.Id == id){
                return agent;
            }
        }
        throw new Exception("Support Agent with Id: " + id + " is not exists.");
    }

    public ServiceRequest GetTicketById(int id){
        foreach (var sr in ServicesRequest)
        {
            if(sr.Id == id){
                return sr;
            }
        }
        throw new Exception("Service Request with Id: " + id + " is not exists.");
    }

    public void UserRegister(string name, string email, int cellphone)
    {
        var newUser = new User(GetUserId(), name, email, cellphone);
        Users.Add(newUser);
    }

    public void AgentRegister(string name, string email, int cellphone)
    {
        var newAgent = new SupportAgent(GetAgentId(), name, email, cellphone);
        Agents.Add(newAgent);
    }

    public void OpenTicket(int userId, int agentId, DateTime OpeningTime){
        var newTicket = new ServiceRequest(GetTicketId(), userId, agentId, OpeningTime);
        ServicesRequest.Add(newTicket);
    }
}