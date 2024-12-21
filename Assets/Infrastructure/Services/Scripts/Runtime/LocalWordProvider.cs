using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Provides a list of valid words from a local list.
/// </summary>
public class LocalWordProvider : MonoBehaviour, IWordProvider
{
    private HashSet<string> _validWords;

    private void Awake()
    {
        // Inicializar la lista de palabras válidas (por ahora, con una lista de ejemplo)
        _validWords = new HashSet<string>
        {
            "CASA", "MESA", "SILLA", "COCHE", "PERRO", "GATO", "ARBOL", "FLOR", "SOL", "LUNA"
            // Añade aquí más palabras válidas
        };
    }

    /// <summary>
    /// Checks if the given word is valid.
    /// </summary>
    /// <param name="word">The word to check.</param>
    /// <returns>True if the word is valid, false otherwise.</returns>
    public bool IsValidWord(string word)
    {
        return _validWords.Contains(word.ToUpper()); // Convertir a mayúsculas para una comparación insensible a mayúsculas/minúsculas
    }
}