namespace keepr.Services;

public class VaultsService
{
    private readonly VaultsRepository _vaultsRepository;

    public VaultsService(VaultsRepository vaultsRepository)
    {
        _vaultsRepository = vaultsRepository;
    }

    public Vault CreateVault(Vault vaultData)
    {
        Vault vault = _vaultsRepository.CreateVault(vaultData);
        return vault;
    }

    public List<Vault> GetVaultsByProfileId(string profileId, Account userInfo)
    {
        List<Vault> vaults = null;
        if (profileId != userInfo?.Id)
        {
            vaults = _vaultsRepository.GetPublicVaultsByProfileId(profileId);
        }
        else
        {
            vaults = _vaultsRepository.GetPrivateVaultsByProfileId(profileId);
        }
        return vaults;
    }

    private Vault GetVaultById(int vaultId)
    {
        Vault vault = _vaultsRepository.GetVaultById(vaultId);
        if (vault == null)
            throw new Exception($"Invalid Vault ID:{vaultId}");
        return vault;
    }

    public Vault GetVaultById(int vaultId, Account userInfo)
    {
        Vault vault = GetVaultById(vaultId);
        if (vault.IsPrivate == true && vault.CreatorId != userInfo?.Id)
            throw new Exception($"Invalid Vault ID:{vaultId}");
        return vault;
    }

    public Vault UpdateVault(int vaultId, Vault vaultData, Account userInfo)
    {
        Vault vaultToUpdate = GetVaultById(vaultId);
        if (vaultToUpdate.CreatorId != userInfo.Id)
            throw new Exception($"You may not edit another's vault, {userInfo.Name.ToUpper()}!");

        vaultToUpdate.Name = vaultData.Name ?? vaultToUpdate.Name;
        vaultToUpdate.IsPrivate = vaultData.IsPrivate ?? vaultToUpdate.IsPrivate;
        vaultToUpdate.Description = vaultData.Description ?? vaultToUpdate.Description;
        vaultToUpdate.Img = vaultData.Img ?? vaultToUpdate.Img;

        _vaultsRepository.UpdateVault(vaultToUpdate);
        return vaultToUpdate;
    }

    public string DeleteVault(int vaultId, Account userInfo)
    {
        Vault vaultToDelete = GetVaultById(vaultId);
        if (vaultToDelete.CreatorId != userInfo.Id)
            throw new Exception($"You may not delete another's vault, {userInfo.Name.ToUpper()}!");
        _vaultsRepository.DeleteVault(vaultId);
        return $"{vaultToDelete.Name} has been deleted";
    }
}
