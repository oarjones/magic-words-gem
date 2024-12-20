
/// <summary>
/// Interface for localization services.
/// </summary>
public interface ILocalization
{
    /// <summary>
    /// Gets the localized string for the given key.
    /// </summary>
    /// <param name="key">The key to get the localized string for.</param>
    /// <returns>The localized string.</returns>
    string GetLocalizedString(string key);
}
