namespace keepr.Services;

public class KeepsService
{
    private readonly KeepsRepository _keepsRepository;

    public KeepsService(KeepsRepository keepsRepository)
    {
        _keepsRepository = keepsRepository;
    }

    public Keep CreateKeep(Keep keepData)
    {
        Keep keep = _keepsRepository.CreateKeep(keepData);
        return keep;
    }

    public List<Keep> GetAllKeeps()
    {
        List<Keep> keeps = _keepsRepository.GetAllKeeps();
        return keeps;
    }

    public Keep GetKeepById(int keepId)
    {
        Keep keep = _keepsRepository.GetKeepById(keepId);
        if (keep == null)
            throw new Exception($"Invalid Keep ID: {keepId}");
        return keep;
    }

    public Keep UpdateKeep(int keepId, Keep keepdata, Account userInfo)
    {
        Keep updatedKeep = GetKeepById(keepId);
        if (updatedKeep.CreatorId != userInfo.Id)
            throw new Exception($"You may not edit another's keep, {userInfo.Name.ToUpper()}!");
        updatedKeep.Name = keepdata.Name ?? updatedKeep.Name;
        updatedKeep.Description = keepdata.Description ?? updatedKeep.Description;
        _keepsRepository.UpdateKeep(updatedKeep);
        return updatedKeep;
    }

    public string DeleteKeep(int keepId, Account userInfo)
    {
        Keep keepToDelete = GetKeepById(keepId);
        if (keepToDelete.CreatorId != userInfo.Id)
            throw new Exception($"You may not delete another's keep, {userInfo.Name.ToUpper()}!");
        _keepsRepository.DeleteKeep(keepId);
        return $"{keepToDelete.Name} has been delete";
    }
}
