using UnityEngine;
using UnityEngine.UI;

public class HealthSystemUI : MonoBehaviour
{
    [SerializeField] private Image fillImage;

    // Reference to our HealthSystem script
    [SerializeField] private HealthSystem healthSystem;

    public void UpdateHealthBar(float current, float max)
    {
        if (fillImage != null)
        {
            fillImage.fillAmount = max > 0 ? current / max : 0f;
        }
    }
}
