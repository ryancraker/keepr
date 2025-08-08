using System.ComponentModel.DataAnnotations;

namespace keepr.Models;

public class Profile : DbItem<string>
{
    public string Name { get; set; }

    [Url]
    public string Picture { get; set; }

    [Url]
    public string CoverImg { get; set; }
}
