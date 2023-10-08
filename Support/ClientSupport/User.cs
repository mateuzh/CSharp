class User
{
    public int Id {get; set;}
    public string Name {get; set;}
    public string Email {get; set;}
    public int Cellphone {get; set;}

    public User(int id, string name, string email, int cellphone)
    {
        Id = id;
        Name = name;
        Email = email;
        Cellphone = cellphone;
    }
}