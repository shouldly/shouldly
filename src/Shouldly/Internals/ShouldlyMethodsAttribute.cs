namespace Shouldly;

/// <summary>
/// Should be used on any class which contains shouldly methods
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class ShouldlyMethodsAttribute : Attribute { }