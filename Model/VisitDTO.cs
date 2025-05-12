namespace apbd_test1.Model;

public class VisitDTO
{
    public DateTime Date { get; set; }
    public ClientDTO Client { get; set; }
    public MechanicDTO Mechanic { get; set; }
    public List<VisitServiceDTO> VisitServices { get; set; }=new List<VisitServiceDTO>();
}

public class ClientDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
}

public class MechanicDTO
{
    public int MechanicID { get; set; }
    public string LicenceNumber { get; set; }
}

public class VisitServiceDTO
{
    public string Name { get; set; }
    public decimal ServicePrice { get; set; }
}