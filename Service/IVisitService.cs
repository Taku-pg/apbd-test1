using apbd_test1.Model;

namespace apbd_test1.Service;

public interface IVisitService
{
    Task<ResultService<VisitDTO>> GetVisitAsync(int visitId);
    
    Task<ResultService<int>> AddVisitAsync(NewVisitDTO newVisit);
}