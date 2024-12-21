using UnityEngine;
using UnityEngine.Events;

public class HexCellButton : MonoBehaviour
{
    public HexCell Cell { get; set; }

    // Define un UnityEvent que podemos configurar desde el Inspector
    public UnityEvent<HexCell> OnCellClicked;

    // Este método se llamará cuando se haga clic en el botón
    public void Click()
    {
        OnCellClicked.Invoke(Cell);
    }
}