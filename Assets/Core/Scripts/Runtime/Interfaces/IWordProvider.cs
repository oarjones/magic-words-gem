/// <summary>
/// Interface for providing a list of valid words.
/// </summary>
public interface IWordProvider
{
    /// <summary>
    /// Checks if the given word is valid.
    /// </summary>
    /// <param name="word">The word to check.</param>
    /// <returns>True if the word is valid, false otherwise.</returns>
    bool IsValidWord(string word);
}