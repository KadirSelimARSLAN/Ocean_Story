using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CanvasController : Singleton<CanvasController>
{

    [SerializeField] private GameObject win_Panel,lose_Panel;
    [SerializeField] private TextMeshProUGUI level_Text;
    
    private void OnEnable()
    {
       
       
        GameManager.ActionGameStart += SetUIPanel;
        GameManager.ActionLevelLosed += SetLosePanel;
        GameManager.ActionLevelPassed += SetWinPanel;
        GameManager.ActionGameStart.Invoke();
    }
   
    private void SetUIPanel()
    {
        level_Text.text = "LEVEL " +  (GameManager.Instance.gameData.playerLevel+1).ToString();

    }
    private void SetWinPanel()
    {
        win_Panel.SetActive(true);
    }
    private void SetLosePanel()
    {
        lose_Panel.SetActive(true);

    }

    public void ButtonNextLevelPressed()
    {
        GameManager.Instance.LoadNextLevel();

    }
    public void ButtonTryAgainPressed()
    {

        GameManager.Instance.LoadNextLevel();
    }

    public void ButtonSoundPressed()
    {
        if (GameManager.Instance.gameData.soundON == true)
        {
            GameManager.Instance.gameData.soundON = false;
        }
        else
        {
            GameManager.Instance.gameData.soundON = true;
        }
       
    }
    private void OnDisable()
    {
        GameManager.ActionGameStart -= SetUIPanel;
        GameManager.ActionLevelLosed -= SetLosePanel;
        GameManager.ActionLevelPassed -= SetWinPanel;
    }
}
