namespace keepr.Repositories;

public class VaultKeepsRepository
{
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    public VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
    {
        string sql =
            @"
      INSERT INTO vault_keeps (vault_id, keep_id, creator_id)
      VALUES (@vaultId, @keepId, @creatorId);

      SELECT * FROM vault_keeps WHERE id = LAST_INSERT_ID()
    ;";
        VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, vaultKeepData).SingleOrDefault();
        return vaultKeep;
    }

    public VaultKeep GetVaultKeepById(int vaultKeepId)
    {
        string sql = "SELECT * FROM vault_keeps WHERE id = @vaultKeepId LIMIT 1;";
        VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, new { vaultKeepId }).SingleOrDefault();
        return vaultKeep;
    }

    public void DeleteVaultKeep(int vaultKeepId)
    {
        string sql = "DELETE FROM vault_keeps WHERE id = @vaultKeepId LIMIT 1;";
        _db.Execute(sql, new { vaultKeepId });
    }
}
