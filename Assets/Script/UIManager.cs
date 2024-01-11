using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField] private HealthBar healthBarPrefab = default;
    [SerializeField] private Transform healthBarContainer = default;

    public void AddHealthUI(Player p)
    {
        var healthBar = Instantiate(healthBarPrefab, healthBarContainer);
        p.healthBar = healthBar;
        healthBar.Initialize(p);
    }

    public void RemoveHealthUI(Player p)
    {
        if (p.healthBar)
        {
            Destroy(p.healthBar.gameObject);
        }
    }

}
