namespace Abstractions;

public static class DomainErrorFactory
{
    // Error categories
    private const string ExistenceCategory = "Existence";
    private const string ValidationCategory = "Validation";
    private const string StateCategory = "State";
    private const string RelationshipCategory = "Relationship";

    public static Error NotFound(string entityType) =>
        new($"{entityType}.{ExistenceCategory}.NotFound", $"{entityType} was not found");



    public static Error PropertyNotUnique(string entityType, string propertyName) =>
                new($"{entityType}.{ValidationCategory}.{propertyName}.Required", $"{entityType} {propertyName} is not unique");

}
