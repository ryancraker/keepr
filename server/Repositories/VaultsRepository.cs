namespace keepr.Repositories;

public class VaultsRepository
{
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
        _db = db;
    }

    public Vault CreateVault(Vault vaultData)
    {
        string sql =
            @"
            INSERT INTO vaults (name, description, is_private, img, creator_id)
            VALUES (@name, @description, @isPrivate, @img, @creatorId);

        SELECT * FROM vaults JOIN accounts ON vaults.creator_id = accounts.id WHERE vaults.id = LAST_INSERT_ID();
    ;";
        Vault vault = _db.Query(
                sql,
                (Vault vault, Profile profile) =>
                {
                    vault.Creator = profile;
                    return vault;
                },
                vaultData
            )
            .SingleOrDefault();
        return vault;
    }

    public Vault GetVaultById(int vaultId)
    {
        string sql =
            @"
        SELECT * FROM vaults JOIN accounts ON vaults.creator_id = accounts.id WHERE vaults.id = @vaultId
    ;";
        Vault vault = _db.Query(
                sql,
                (Vault vault, Profile profile) =>
                {
                    vault.Creator = profile;
                    return vault;
                },
                new { vaultId }
            )
            .SingleOrDefault();
        return vault;
    }

    public void UpdateVault(Vault vaultToUpdate)
    {
        string sql =
            @"UPDATE vaults SET name = @name, description = @description, is_private = @isPrivate, img = @img WHERE id = @id LIMIT 1;";
        _db.Execute(sql, vaultToUpdate);
    }

    public void DeleteVault(int vaultId)
    {
        string sql = @"DELETE FROM vaults WHERE id = @vaultId LIMIT 1;";
        _db.Execute(sql, new { vaultId });
    }
}
