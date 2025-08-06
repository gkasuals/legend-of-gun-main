using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Health }
    public InfoType type;

    Text Text;
    Slider Slider;

    private void Awake()
    {
        Text = GetComponent<Text>();
        Slider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Health:
                float curHealth = GameManager.instance.Health;
                float maxHealth = GameManager.instance.MaxHealth;
                Slider.value = curHealth / maxHealth;
                break;
        }
    }
}
