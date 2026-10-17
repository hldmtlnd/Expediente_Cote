using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactDistance = 3f;
    public LayerMask interactableLayer;

    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            TryInteract();

        // Recording and flash handled by phone/camera scripts
        if (Input.GetMouseButtonDown(0))
            OnLeftClick();

        if (Input.GetMouseButtonDown(1))
            OnRightClick();
    }

    void TryInteract()
    {
        Ray ray = new Ray(mainCam.transform.position, mainCam.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactableLayer))
        {
            var interactable = hit.collider.GetComponent<IInteractable>();
            interactable?.Interact(this);
        }
    }

    void OnLeftClick()
    {
        // Record / stop record — delegated to active camera device
    }

    void OnRightClick()
    {
        // Toggle flash — delegated to active camera device
    }
}

public interface IInteractable
{
    void Interact(PlayerInteraction player);
}
