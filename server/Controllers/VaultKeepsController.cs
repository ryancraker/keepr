namespace keepr.Controllers;

[ApiController]
[Route("api/vaultkeeps")]
public class VaultKeepsController : ControllerBase
{
    private readonly VaultKeepsService _vaultKeepsService;
    private readonly Auth0Provider _auth;

    public VaultKeepsController(VaultKeepsService vaultKeepsService, Auth0Provider auth)
    {
        _vaultKeepsService = vaultKeepsService;
        _auth = auth;
    }

    [HttpPost, Authorize]
    public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep vaultKeepData)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            vaultKeepData.CreatorId = userInfo.Id;
            VaultKeep vaultKeep = _vaultKeepsService.CreateVaultKeep(vaultKeepData, userInfo);
            return Ok(vaultKeep);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpDelete("{vaultKeepId}"), Authorize]
    public async Task<ActionResult<string>> DeleteVaultKeep(int vaultKeepId)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            _vaultKeepsService.DeleteVaultKeep(vaultKeepId, userInfo);
            return Ok($"Vault keep {vaultKeepId} deleted");
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}
