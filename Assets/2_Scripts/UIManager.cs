using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public TextMeshProUGUI AmmoText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void UpdateAmmoText(int current, int max)
    {
        if (current < 0)
            AmmoText.text = "¡Ä / " + max.ToString();
        else
            AmmoText.text = current + " / " + max;
    }
}