namespace N5.Permissions.Domain.Interfaces.Infraestructure.Services;

/// <summary>
/// Interface for the Indexation Service.
/// </summary>
public interface IIndexationService
{
    /// <summary>
    /// Asynchronously indexes a document of type T.
    /// </summary>
    /// <typeparam name="T">The type of the document to index.</typeparam>
    Task IndexDocumentAsync<T>(T document) where T : class;
}
