namespace keepr.Controllers;

[ApiController]
[Route("api/profiles")]
public class ProfilesController : ControllerBase
{
    private readonly ProfilesService _profilesService;
    private readonly KeepsService _keepsService;
    private readonly VaultsService _vaultsService;
    private readonly Auth0Provider _auth;

    public ProfilesController(
        ProfilesService profilesService,
        KeepsService keepsService,
        VaultsService vaultsService,
        Auth0Provider auth
    )
    {
        _profilesService = profilesService;
        _keepsService = keepsService;
        _vaultsService = vaultsService;
        _auth = auth;
    }

    [HttpGet("{profileId}")]
    public ActionResult<Profile> GetProfileById(string profileId)
    {
        try
        {
            Profile profile = _profilesService.GetProfileById(profileId);
            return Ok(profile);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{profileId}/keeps")]
    public ActionResult<List<Keep>> GetKeepsByProfileId(string profileId)
    {
        try
        {
            List<Keep> keeps = _keepsService.GetKeepsByProfileId(profileId);
            return Ok(keeps);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{profileId}/vaults")]
    public async Task<ActionResult<List<Vault>>> GetVaultsByProfileId(string profileId)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            List<Vault> vaults = _vaultsService.GetVaultsByProfileId(profileId, userInfo);
            return Ok(vaults);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}
