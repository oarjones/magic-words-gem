
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Validates words formed by the player.
/// </summary>
public class WordValidator : MonoBehaviour
{
    private IWordProvider _wordProvider;

    public WordValidator(IWordProvider wordProvider)
    {
        _wordProvider = wordProvider;
    }

    /// <summary>
    /// Validates a given word.
    /// </summary>
    /// <param name="word">The word to validate.</param>
    /// <returns>True if the word is valid, false otherwise.</returns>
    public bool ValidateWord(string word)
    {
        return _wordProvider.IsValidWord(word);
    }
}
