using UnityEngine;
using UnityEngine.EventSystems;

public class BuildSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private TileAnimator tileAnim;
    private Vector3 defaultPosition;
    private BuildManager buildManager;
    private MeshRenderer meshRenderer;

    [SerializeField] private Material emissionBlueMaterial; // Reference to Emission_blue material
    private Material defaultMaterial;

    private bool tileCanBeMoved = true;

    private Coroutine currentMovementUpCo;

    private void Awake()
    {
        tileAnim = FindFirstObjectByType<TileAnimator>();
        buildManager = FindFirstObjectByType<BuildManager>();
        defaultPosition = transform.position;

        meshRenderer = GetComponent<MeshRenderer>();
        defaultMaterial = meshRenderer.material;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button != PointerEventData.InputButton.Left || !tileCanBeMoved) 
            return;

        if (buildManager.GetSelectedSlot() == this)
            return;

        buildManager.EnableBuildMenu();
        buildManager.SelectBuildSlot(this);
        meshRenderer.material = emissionBlueMaterial;

        tileCanBeMoved = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Input.GetKey(KeyCode.Mouse1) || Input.GetKey(KeyCode.Mouse2)) return;
        
        if (!tileCanBeMoved) return;

        MoveTileUp();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!tileCanBeMoved) return;

        if (currentMovementUpCo != null)
            Invoke(nameof(MoveToDefaultPosition), tileAnim.GetTravelDuration());
        else
            MoveToDefaultPosition();
    }

    public void UnselectTile()
    {
        MoveToDefaultPosition();
        meshRenderer.material = defaultMaterial;
        tileCanBeMoved = true;
    }

    private void MoveTileUp()
    {
        Vector3 targetPosition = transform.position + new Vector3(0, tileAnim.GetBuildOffset(), 0);
        currentMovementUpCo = StartCoroutine(tileAnim.MoveTileCo(transform, targetPosition));
    }

    private void MoveToDefaultPosition()
    {
       tileAnim.MoveTile(transform, defaultPosition);
        meshRenderer.material = defaultMaterial;
    }
}
