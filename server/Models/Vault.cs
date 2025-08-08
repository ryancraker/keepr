using System.ComponentModel.DataAnnotations;

namespace keepr.Models;

public class Vault : DbItem<int>
{
    public string Name { get; set; }
    public string CreatorId { get; set; }
    public string Description { get; set; }

    [Url]
    public string Img { get; set; }
    public bool? IsPrivate { get; set; }
    public Profile Creator { get; set; }
}
