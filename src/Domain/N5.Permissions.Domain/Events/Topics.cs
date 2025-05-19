namespace N5.Permissions.Domain;

/// <summary>
/// This class contains the topics used in the application.
/// These topics are used to publish and subscribe to events.
/// </summary>
public static class Topics
{
    /// <summary>
    /// Topic used to publish and subscribe to permission events.
    /// </summary>
    public const string GetAllPermissions = "n5.permissions.get.all.permissions";

    /// <summary>
    /// Topic used to publish and subscribe to permission events.
    /// </summary>
    public const string ModifyPermission = "n5.permissions.modify.permission";

    /// <summary>
    /// Topic used to publish and subscribe to permission events.
    /// </summary>
    public const string RequestPermission = "n5.permissions.request.permission";
}
