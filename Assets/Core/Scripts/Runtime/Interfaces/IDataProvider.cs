
/// <summary>
/// Interface for data persistence.
/// </summary>
public interface IDataProvider
{
    /// <summary>
    /// Saves the given data under the specified key.
    /// </summary>
    /// <param name="key">The key to save the data under.</param>
    /// <param name="data">The data to save.</param>
    void SaveData<T>(string key, T data);

    /// <summary>
    /// Loads the data stored under the specified key.
    /// </summary>
    /// <typeparam name="T">The type of data to load.</typeparam>
    /// <param name="key">The key to load the data from.</param>
    /// <returns>The loaded data, or the default value of type T if no data is found.</returns>
    T LoadData<T>(string key);
}
