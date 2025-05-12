using apbd_test1.Model;

namespace apbd_test1.Repository;

public interface IVisitRepository
{
    Task<bool> DoesVisitExist(int visitId);
    Task<bool> DoesClientExist(int clientId);
    Task<int> DoesMechanicExist(string licenceNumber);

    Task<bool> DoesServiceExist(string serviceName);
    Task<VisitDTO> GetVisit(int visitId);
    Task<int> AddVisit(NewVisitDTO newVisit,int mechanicId);
}