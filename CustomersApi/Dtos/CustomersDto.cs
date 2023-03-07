namespace CustomersApi.Dtos;

public class CustomersDto // un objeto que utilizas para trnasferir datos
{
    public long Id { get; set; }//para acceder directo desde clq lado
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    
}