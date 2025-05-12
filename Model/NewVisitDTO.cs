namespace apbd_test1.Model;

public class NewVisitDTO
{
    public int VisitId { get; set; }
    public int ClientId { get; set; }
    public string MechanicLicenceNumber { get; set; }
    public List<NewVisitServiceDTO> Services { get; set; }
}

public class NewVisitServiceDTO
{
    public string ServicialName { get; set; }
    public decimal ServiceFee { get; set; }
}