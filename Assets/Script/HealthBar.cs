using System;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class HealthBar : MonoBehaviour
{
    private const string HiddenHealthBarStyleClass = "health-bar--hidden";
        
    // Prefab properties
    [SerializeField] private Vector3 unitAnchorPosition = new Vector3(0f, 0f, 0f);
    [SerializeField] private Vector3 nonUnitAnchorPosition = new Vector3(1.8f, 2f, 0f);
    [SerializeField] private Vector2 worldSize = new Vector2(2f, .5f);
    [SerializeField] private Color red = new Color32(252, 35, 13, 255);
    [SerializeField] private Color blue = new Color32(31, 132, 255, 255);

        
    [SerializeField] private bool isHidden = false;

    [HideInInspector] public float originalHealth;
    [HideInInspector] public float currentHealth;
    [HideInInspector] public Vector3 anchorPosition;
    [HideInInspector] public Color barColor;
    [HideInInspector] public Player playerToFollow;

    private VisualElement bar;
    private VisualElement wholeWidget;
        
    public void Initialize(Player p)
    {            
        currentHealth = originalHealth = p.hitPoints;
        //anchorPosition = p.pType == Placeable.PlaceableType.Unit ? unitAnchorPosition : nonUnitAnchorPosition;
        anchorPosition = unitAnchorPosition;
        //barColor = p.faction == Placeable.Faction.Player ? red : blue;
        barColor = red;
        playerToFollow = p;
    }

    void OnEnable()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        bar = rootVisualElement.Q("Bar");
        wholeWidget = rootVisualElement.Q("HealthBar");
    }

    private void Start()
    {
        bar.style.unityBackgroundImageTintColor = barColor;
        SetHealth(currentHealth);
    }

    public void SetHealth(float newHealth)
    {
        currentHealth = newHealth;
        //isHidden = newHealth >= originalHealth;

        float ratio = newHealth > 0f ? newHealth / originalHealth : 1e-5f;
        bar.transform.scale = new Vector3(ratio, 1, 1);
            
        // Hide the health bar after the position is set, otherwise it won't hide.
        wholeWidget.EnableInClassList(HiddenHealthBarStyleClass, isHidden);
    }

    private void Update()
    {
        wholeWidget.EnableInClassList(HiddenHealthBarStyleClass, isHidden);
    }

    // Wait for LateUpdate 1) to allow tracked object to move and
    //                     2) to leave time for wholeWidget.layout to be refreshed
    private void LateUpdate()
    {
        if (!isHidden && playerToFollow != null)
        {
            //MoveAndScaleToWorldPosition(wholeWidget, playerToFollow.transform.position + anchorPosition, worldSize);
        }
    }

    internal void MoveAndScaleToWorldPosition(Vector3 worldPosition)
    {
        if (isHidden) return;
        worldPosition += anchorPosition;
        Rect rect = RuntimePanelUtils.CameraTransformWorldToPanelRect(wholeWidget.panel, worldPosition, worldSize, Camera.main);
        Vector2 layoutSize = wholeWidget.layout.size;

        // Don't set scale to 0 or a negative number.
        Vector2 scale = layoutSize.x > 0 && layoutSize.y > 0 ? rect.size / layoutSize : Vector2.one * 1e-5f;

        wholeWidget.transform.position = rect.position;
        wholeWidget.transform.scale = new Vector3(scale.x, scale.y, 1);
    }
}