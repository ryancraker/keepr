namespace keepr.Services;

public class VaultKeepsService
{
    private readonly VaultKeepsRepository _repo;
    private readonly VaultsService _vaultsService;
    private readonly KeepsService _keepsService;

    public VaultKeepsService(
        VaultKeepsRepository repo,
        VaultsService vaultsService,
        KeepsService keepsService
    )
    {
        _repo = repo;
        _vaultsService = vaultsService;
        _keepsService = keepsService;
    }

    public VaultKeep CreateVaultKeep(VaultKeep vaultKeepData, Account userInfo)
    {
        Vault vault = _vaultsService.GetVaultById(vaultKeepData.VaultId, userInfo);
        if (vaultKeepData.CreatorId != vault.CreatorId)
            throw new Exception("You cannot add keeps to another's vault");
        _keepsService.GetKeepById(vaultKeepData.KeepId);
        VaultKeep vaultKeep = _repo.CreateVaultKeep(vaultKeepData);
        return vaultKeep;
    }

    public void DeleteVaultKeep(int vaultKeepId, Account userInfo)
    {
        VaultKeep vaultKeep = GetVaultKeepById(vaultKeepId);
        if (vaultKeep.CreatorId != userInfo.Id)
            throw new Exception("You cannot remove keeps from another's vault");
        _repo.DeleteVaultKeep(vaultKeepId);
    }

    private VaultKeep GetVaultKeepById(int vaultKeepId)
    {
        VaultKeep vaultKeep = _repo.GetVaultKeepById(vaultKeepId);
        if (vaultKeep == null)
            throw new Exception($"No vault keep with ID: {vaultKeepId}");
        return vaultKeep;
    }
}
