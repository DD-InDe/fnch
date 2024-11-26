using EAS_Hub.DbModels;

namespace EAS_Hub.Services;

public class Db
{
    public static EasFullDbContext Context = new EasFullDbContext();
}