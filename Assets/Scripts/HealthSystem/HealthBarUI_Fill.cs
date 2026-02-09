using UnityEngine;
using UnityEngine.UI;
public class HealthBarUI_Fill : MonoBehaviour
{
    public Health target;
    public Image fillImage;

    private void Awake()
    {
        if (fillImage == null) fillImage = GetComponentInChildren<Image>();
    }

    private void OnEnable()
    {
        if (target != null)
        {
            target.OnHealthChanged += Handle;
            Handle(target.Current, target.Max);
        }
    }

    private void OnDisable()
    {
        if (target != null)
        {
            target.OnHealthChanged -= Handle;
        }
    }

    void Handle(float current, float max)
    {
        fillImage.fillAmount = (max <= 0f) ? 0f : current / max;
    }
}
