using System.ComponentModel.DataAnnotations;

namespace apbd_test1.Model;

public class NewVisitDTO
{
    [Required]
    public int VisitId { get; set; }
    [Required]
    public int ClientId { get; set; }
    [Required]
    [MaxLength(14)]
    public string MechanicLicenceNumber { get; set; }
    [Required]
    public List<NewVisitServiceDTO> Services { get; set; }
}

public class NewVisitServiceDTO
{
    public string ServicialName { get; set; }
    public decimal ServiceFee { get; set; }
}