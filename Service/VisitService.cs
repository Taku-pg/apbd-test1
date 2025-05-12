using apbd_test1.Model;
using apbd_test1.Repository;

namespace apbd_test1.Service;

public class VisitService: IVisitService
{
    private readonly IVisitRepository _visitRepository;

    public VisitService(IVisitRepository visitRepository)
    {
        _visitRepository = visitRepository;
    }

    public async Task<ResultService<VisitDTO>> GetVisitAsync(int visitId)
    {
        var check=await _visitRepository.DoesVisitExist(visitId);
        if(!check)
            return ResultService<VisitDTO>.Fail("Visit not found");
        var visit=await _visitRepository.GetVisit(visitId);
        if (visit == null)
            return ResultService<VisitDTO>.Fail("Visit not found");
        return ResultService<VisitDTO>.Ok(visit);
    }

    public async Task<ResultService<int>> AddVisitAsync(NewVisitDTO newVisit)
    {
        var check= await _visitRepository.DoesVisitExist(newVisit.VisitId);
        if(!check)
            return ResultService<int>.Fail("Visit not found");
        check=await _visitRepository.DoesClientExist(newVisit.ClientId);
        if(!check)
            return ResultService<int>.Fail("Client not found");
        var mechanicId = await _visitRepository.DoesMechanicExist(newVisit.MechanicLicenceNumber);
        if(mechanicId==0)
            return ResultService<int>.Fail("Mechanic not found");
        var newId=await _visitRepository.AddVisit(newVisit,mechanicId);
        return ResultService<int>.Ok(newId);
    }
}