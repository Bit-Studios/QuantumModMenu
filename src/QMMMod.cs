using UnityEngine;
using System.Collections;
using KSP.Modding;
using KSP.Game;
using KSP.Messages;
using TMPro;
using I2.Loc;
using KSP.Sim.ResourceSystem;

public class QMMMod : Mod
{
    static void Log(string message)
    {
        Debug.Log($"QMM: {message}");
    }
    void Awake()
    {
        Log("GameManager.Instance.Game.Messages.Subscribe<GameStateChangedMessage>(GameStateChanged);");
        GameManager.Instance.Game.Messages.Subscribe<GameStateChangedMessage>(GameStateChanged);

    }
    void GameStateChanged(MessageCenterMessage messageCenterMessage)
    {
        GameStateChangedMessage gameStateChangedMessage = messageCenterMessage as GameStateChangedMessage;
        if (gameStateChangedMessage.CurrentState == GameState.MainMenu)
        {
            Log("Start StartCoroutine");
            StartCoroutine(SetMainMenu());
        }
    }
    public IEnumerator SetMainMenu()
    {
        Log("Setting MainMenu Button");
        yield return new WaitForSeconds(1.5f); Log("Instantiate");
        GameObject ModsMenuButton = Instantiate(GameObject.Find("GameManager/Default Game Instance(Clone)/UI Manager(Clone)/Main Canvas/MainMenu(Clone)/MenuItemsGroup/Multiplayer"), GameObject.Find("GameManager/Default Game Instance(Clone)/UI Manager(Clone)/Main Canvas/MainMenu(Clone)/MenuItemsGroup").transform);
        ModsMenuButton.name = "ModsMenuButton";
        GameObject.Find("GameManager/Default Game Instance(Clone)/UI Manager(Clone)/Main Canvas/MainMenu(Clone)/MenuItemsGroup/ModsMenuButton/Content/Text (TMP)").GetComponent<Localize>().mTerm = string.Empty;
        GameObject.Find("GameManager/Default Game Instance(Clone)/UI Manager(Clone)/Main Canvas/MainMenu(Clone)/MenuItemsGroup/ModsMenuButton/Content/Text (TMP)").GetComponent<Localize>().FinalTerm = string.Empty;
        LocalizationManager.LocalizationList.Remove(GameObject.Find("GameManager/Default Game Instance(Clone)/UI Manager(Clone)/Main Canvas/MainMenu(Clone)/MenuItemsGroup/ModsMenuButton/Content/Text (TMP)").GetComponent<Localize>());
        yield return new WaitForSeconds(1.5f);
        GameObject.Find("GameManager/Default Game Instance(Clone)/UI Manager(Clone)/Main Canvas/MainMenu(Clone)/MenuItemsGroup/ModsMenuButton/Content/Text (TMP)").GetComponent<TextMeshProUGUI>().GetTextInfo("Mods - Quantum");
        ModsMenuButton.SetActive(true);
        ModsMenuButton.transform.SetSiblingIndex(4);
        ModsMenuButton.GetComponent<UIAction_Void_Button>().button.onClick.AddListener(() =>
        {
            GameObject.Find("GameManager/Default Game Instance(Clone)/UI Manager(Clone)/Main Canvas/ModManagerDialog(Clone)").SetActive(true);
        });
    }
}
