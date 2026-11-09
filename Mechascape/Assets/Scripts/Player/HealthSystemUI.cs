using UnityEngine;
using UnityEngine.UI;

public class HealthSystemUI : MonoBehaviour
{
    public Image fillImage;
    
    public void UpdateHealthBar(int current, int max)
    {
        fillImage.fillAmount = (float)current / max;
    }
}
