namespace keepr.Services;

public class ProfilesService
{
    private readonly ProfilesRepository _repo;

    public ProfilesService(ProfilesRepository repo)
    {
        _repo = repo;
    }

    public Profile GetProfileById(string profileId)
    {
        Profile profile = _repo.GetProfileById(profileId);
        return profile;
    }
}
