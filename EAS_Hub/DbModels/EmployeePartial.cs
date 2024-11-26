namespace EAS_Hub.DbModels;

public partial class Employee
{
    public string FullName => $"{LastName} {FirstName} {MiddleName}";
}