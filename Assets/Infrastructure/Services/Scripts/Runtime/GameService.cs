
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
