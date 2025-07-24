using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject characterSelectPanel;

    private void Start()
    {
        mainMenuPanel.SetActive(true);
        characterSelectPanel.SetActive(false);
    }

    // 시작 버튼에 연결
    public void OnStartButton()
    {
        mainMenuPanel.SetActive(false);
        characterSelectPanel.SetActive(true);
    }

    // 캐릭터 선택 버튼에 연결 (예: 남캐면 "0", 여캐면 "1")
    public void OnCharacterSelect(int characterId)
    {
        // 선택한 캐릭터 정보를 GameManager 등에 저장

        // 게임 씬으로 이동 (예: "GameScene" 이름의 씬)
        SceneManager.LoadScene("GameScene");
    }
}
