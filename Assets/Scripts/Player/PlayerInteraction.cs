using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactDistance = 3f;
    public LayerMask interactableLayer;

    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
        if (mainCam == null)
        {
            // Fallback: search any active camera
            mainCam = FindObjectOfType<Camera>();
        }
    }

    void Update()
    {
        if (mainCam == null) return;

        if (Input.GetKeyDown(KeyCode.E))
            TryInteract();

        if (Input.GetMouseButtonDown(0))
            OnLeftClick();

        if (Input.GetMouseButtonDown(1))
            OnRightClick();
    }

    void TryInteract()
    {
        if (mainCam == null) return;

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

    public void SetCamera(Camera cam)
    {
        mainCam = cam;
    }
}

public interface IInteractable
{
    void Interact(PlayerInteraction player);
}
