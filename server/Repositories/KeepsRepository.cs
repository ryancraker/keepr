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

            SELECT * FROM keeps JOIN accounts ON keeps.creator_id = accounts.id WHERE keeps.id = LAST_INSERT_ID();";
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
            @"SELECT * FROM keeps JOIN accounts ON keeps.creator_id = accounts.id ORDER BY keeps.id DESC;";
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
      SELECT * FROM keeps JOIN accounts ON keeps.creator_id = accounts.id WHERE keeps.id = @keepId
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
}
