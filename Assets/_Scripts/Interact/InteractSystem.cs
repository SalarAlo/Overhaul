using UnityEngine;

public class InteractSystem : MonoBehaviour
{
    private void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(!Physics.Raycast(ray, out RaycastHit hit)) return;
        if(!hit.transform.TryGetComponent(out Interactable interactable)) return;
        interactable.OnHover();
        if(!Input.GetMouseButtonDown(0)) return;
        interactable.Interact();
    }
}
