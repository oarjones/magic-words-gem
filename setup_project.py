import os

def create_directory(path):
    """Crea un directorio si no existe."""
    if not os.path.exists(path):
        os.makedirs(path)
        print(f"Directorio creado: {path}")

def create_file(path, content):
    """Crea un archivo con el contenido especificado."""
    with open(path, 'w') as f:
        f.write(content)
    print(f"Archivo creado: {path}")

def setup_project(project_root):
    """Genera la estructura de carpetas y archivos para el proyecto."""

    # Define la estructura de carpetas
    directories = [
        "Assets/Core/Scripts/Runtime",
        "Assets/Core/Scripts/Tests",
        "Assets/Core/Prefabs",
        "Assets/Features/Gameplay/Scripts/Runtime",
        "Assets/Features/Gameplay/Scripts/Tests",
        "Assets/Features/Gameplay/Prefabs",
        "Assets/Features/Turns/Scripts/Runtime",
        "Assets/Features/Turns/Scripts/Tests",
        "Assets/Features/Turns/Prefabs",
        "Assets/Features/PowerUps/Scripts/Runtime",
        "Assets/Features/PowerUps/Scripts/Tests",
        "Assets/Features/PowerUps/Prefabs",
        "Assets/Features/Matchmaking/Scripts/Runtime",
        "Assets/Features/Matchmaking/Scripts/Tests",
        "Assets/Features/Matchmaking/Prefabs",
        "Assets/Features/Modes/Scripts/Runtime",
        "Assets/Features/Modes/Scripts/Tests",
        "Assets/Features/Modes/Prefabs",
        "Assets/Infrastructure/InputSystem/Scripts/Runtime",
        "Assets/Infrastructure/InputSystem/Scripts/Tests",
        "Assets/Infrastructure/InputSystem/Prefabs",
        "Assets/Infrastructure/Localization/Scripts/Runtime",
        "Assets/Infrastructure/Localization/Scripts/Tests",
        "Assets/Infrastructure/Localization/Prefabs",
        "Assets/Infrastructure/Persistence/Scripts/Runtime",
        "Assets/Infrastructure/Persistence/Scripts/Tests",
        "Assets/Infrastructure/Persistence/Prefabs",
        "Assets/Infrastructure/Network/Scripts/Runtime",
        "Assets/Infrastructure/Network/Scripts/Tests",
        "Assets/Infrastructure/Network/Prefabs",
        "Assets/Infrastructure/Services/Scripts/Runtime",
        "Assets/Infrastructure/Services/Scripts/Tests",
        "Assets/Infrastructure/Services/Prefabs",
        "Assets/UI/Scripts/Runtime",
        "Assets/UI/Scripts/Tests",
        "Assets/UI/Prefabs",
        "Assets/Art/Animations",
        "Assets/Art/Materials",
        "Assets/Art/Models",
        "Assets/Art/Prefabs",
        "Assets/Art/Sprites",
        "Assets/Art/Textures",
        "Assets/Scenes/Samples",
        "Assets/Scenes/Tests",
        "Assets/Audio/Music",
        "Assets/Audio/SFX",
        "Assets/Packages",
        "Assets/Core/Scripts/Runtime/Interfaces",
        "Assets/Infrastructure/InputSystem/Scripts/Runtime/Interfaces",
        "Assets/Infrastructure/Persistence/Scripts/Runtime/Interfaces",
        "Assets/Infrastructure/Network/Scripts/Runtime/Interfaces"
    ]

    # Crea las carpetas
    for directory in directories:
        create_directory(os.path.join(project_root, directory))

    # Define el contenido de los archivos
    files = {
        "Assets/Core/Scripts/Runtime/GameManager.cs": """
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameManager class orchestrates the overall game flow.
/// It should be implemented as a Singleton.
/// </summary>
public class GameManager : MonoBehaviour
{
    // Instance of the GameManager for the Singleton pattern
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        // Singleton pattern implementation
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
""",
        "Assets/Core/Scripts/Runtime/GameState.cs": """
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the current state of the game.
/// </summary>
public class GameState : MonoBehaviour
{

}
""",
        "Assets/Core/Scripts/Runtime/WordValidator.cs": """
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
""",
        "Assets/Core/Scripts/Runtime/Interfaces/IDataProvider.cs": """
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
""",
        "Assets/Core/Scripts/Runtime/Interfaces/IWordProvider.cs": """
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
""",
        "Assets/Infrastructure/InputSystem/Scripts/Runtime/Interfaces/IInputProvider.cs": """
using UnityEngine;
/// <summary>
/// Interface for the input system of the game
/// </summary>
public interface IInputProvider
{

}
""",
        "Assets/Infrastructure/Network/Scripts/Runtime/FirebaseNetworkService.cs": """
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Service for handling network interactions with Firebase.
/// </summary>
public class FirebaseNetworkService : MonoBehaviour, INetworkService
{

}
""",
        "Assets/Infrastructure/Network/Scripts/Runtime/Interfaces/INetworkService.cs": """
/// <summary>
/// Interface for network services.
/// </summary>
public interface INetworkService
{

}
""",
        "Assets/Infrastructure/Persistence/Scripts/Runtime/Interfaces/ILocalization.cs": """
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
""",
        "Assets/Infrastructure/Services/Scripts/Runtime/GameService.cs": """
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Service for game-related operations.
/// </summary>
public class GameService : MonoBehaviour
{
    private INetworkService _networkService;
    private IWordProvider _wordProvider;

    public GameService(INetworkService networkService, IWordProvider wordProvider)
    {
        _networkService = networkService;
        _wordProvider = wordProvider;
    }
}
"""
    }

    # Crea los archivos
    for file_path, content in files.items():
        create_file(os.path.join(project_root, file_path), content)

if __name__ == "__main__":
    project_root = os.getcwd()  # Obtiene el directorio actual como directorio raíz del proyecto
    setup_project(project_root)
    print("Proyecto configurado exitosamente.")