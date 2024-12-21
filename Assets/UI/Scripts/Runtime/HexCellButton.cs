using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class HexCellButton : MonoBehaviour
{
    public HexCell Cell { get; set; }

    // Referencia al componente TextMeshProUGUI del botón
    public TextMeshProUGUI Label;

    // Define un UnityEvent que podemos configurar desde el Inspector
    public UnityEvent<HexCell> OnCellClicked;

    private void Awake()
    {
        // Obtiene la referencia al componente TextMeshProUGUI
        Label = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Este método se llamará cuando se haga clic en el botón
    public void Click()
    {
        OnCellClicked.Invoke(Cell);
    }
}