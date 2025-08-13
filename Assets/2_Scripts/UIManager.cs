using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public Text ammoText;

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
            ammoText.text = "¡Ä / " + max.ToString();
        else
            ammoText.text = current + " / " + max;
    }
}