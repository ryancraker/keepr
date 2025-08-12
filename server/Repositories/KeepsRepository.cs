namespace keepr.Repositories;

public class KeepsRepository
{
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    public Keep CreateKeep(Keep keepData)
    {
        string sql =
            @"INSERT INTO keeps
            (name, description, img, creator_id)
            VALUES (@name, @description, @img, @creatorId);

            SELECT * FROM keeps_with_kept AS keeps JOIN accounts ON keeps.creator_id = accounts.id WHERE keeps.id = LAST_INSERT_ID();";
        Keep keep = _db.Query(
                sql,
                (Keep keep, Profile profile) =>
                {
                    keep.Creator = profile;
                    return keep;
                },
                keepData
            )
            .SingleOrDefault();
        return keep;
    }

    public List<Keep> GetAllKeeps()
    {
        string sql =
            @"SELECT * FROM keeps_with_kept AS keeps JOIN accounts ON keeps.creator_id = accounts.id ORDER BY keeps.id DESC;";
        List<Keep> keeps = _db.Query(
                sql,
                (Keep keep, Profile profile) =>
                {
                    keep.Creator = profile;
                    return keep;
                }
            )
            .ToList();
        return keeps;
    }

    public Keep GetKeepById(int keepId)
    {
        string sql =
            @"
      SELECT * FROM keeps_with_kept AS keeps JOIN accounts ON keeps.creator_id = accounts.id WHERE keeps.id = @keepId
    ;";
        Keep keep = _db.Query(
                sql,
                (Keep keep, Profile profile) =>
                {
                    keep.Creator = profile;
                    return keep;
                },
                new { keepId }
            )
            .SingleOrDefault();
        return keep;
    }

    public List<Keep> GetKeepsByVaultId(int vaultId)
    {
        string sql =
            @"
        SELECT keeps.*, accounts.*, vault_keeps.* FROM keeps_with_kept AS keeps 
        JOIN accounts ON keeps.creator_id  = accounts.id 
        LEFT JOIN vault_keeps ON keeps.id = vault_keeps.keep_id 
        WHERE vault_keeps.vault_id = @vaultId
    ;";
        List<Keep> keeps = _db.Query(
                sql,
                (Keep keep, Profile profile, VaultKeep vaultKeep) =>
                {
                    keep.Creator = profile;
                    keep.VaultKeepId = vaultKeep.Id;
                    return keep;
                },
                new { vaultId }
            )
            .ToList();
        return keeps;
    }

    public void UpdateKeep(Keep updatedKeep)
    {
        string sql =
            @"UPDATE keeps SET name = @name, description = @description WHERE id = @id LIMIT 1;";
        _db.Execute(sql, updatedKeep);
    }

    public void DeleteKeep(int keepId)
    {
        string sql = @"DELETE FROM keeps WHERE id = @keepId LIMIT 1;";
        _db.Execute(sql, new { keepId });
    }

    public List<Keep> GetKeepsByProfileId(string profileId)
    {
        string sql =
            @"
        SELECT * FROM keeps_with_kept AS keeps JOIN accounts ON keeps.creator_id = accounts.id WHERE keeps.creator_id = @profileId ORDER BY keeps.id DESC;
    ;";
        List<Keep> keeps = _db.Query(
                sql,
                (Keep keep, Profile profile) =>
                {
                    keep.Creator = profile;
                    return keep;
                },
                new { profileId }
            )
            .ToList();
        return keeps;
    }

    public void ViewKeep(Keep keep)
    {
        string sql = "UPDATE keeps SET views = @Views WHERE id = @Id LIMIT 1;";
        _db.Execute(sql, keep);
    }
}
