using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleManager : MonoBehaviour
{
    public ScreenCover cover;
    public Image playerImage;
    public TextMeshProUGUI playerNameText;
    public Button prevChoiceButton;
    public Button nextChoiceButton;

    public Sprite[] playerIcons;
    public string[] playerNames;

    private void OnEnable()
    {
        ChoiceManager.OnChoiceChange += UpdateChoiceUI;
    }

    private void OnDisable()
    {
        ChoiceManager.OnChoiceChange -= UpdateChoiceUI;
    }

    void Start()
    {
        StartCoroutine(cover.SetCoverValue(0, 0.5f));
        prevChoiceButton.onClick.AddListener(ChoiceManager.instance.PreviousChoice);
        nextChoiceButton.onClick.AddListener(ChoiceManager.instance.NextChoice);
        UpdateChoiceUI(ChoiceManager.instance.choiceIndex);
    }

    public void UpdateChoiceUI(int choiceIndex)
    {
        playerImage.sprite = playerIcons[choiceIndex];
        playerNameText.text = playerNames[choiceIndex];
        StartCoroutine(CO_Pop(playerImage.gameObject));
    }

    public void LoadGame()
    {
        StartCoroutine(CO_LoadGame());
    }

    public IEnumerator CO_LoadGame()
    {
        StartCoroutine(cover.SetCoverValue(1, 0.5f));
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("GameScene");
    }

    public IEnumerator CO_Pop(GameObject obj)
    {
        float scale = 1.1f;

        while (scale > 1)
        {
            obj.transform.localScale = Vector3.one * scale;
            scale -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        obj.transform.localScale = Vector3.one;
    }
}
