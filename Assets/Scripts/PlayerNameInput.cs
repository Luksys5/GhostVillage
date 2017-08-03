using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class PlayerNameInput : MonoBehaviour {

    #region Private Variables
    InputField inputField;
    static string playerNamePrefKey = "name";
    #endregion

    #region MonoBehaviour CallBacks
    
    void Start () {
        string defaultName = "";
        inputField = GetComponent<InputField>();
        if(inputField != null)
        {
            if(PlayerPrefs.HasKey(playerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                inputField.text = defaultName;
            }
        }
        PhotonNetwork.playerName = defaultName;
	}

    #endregion  

    #region Public Methods

    public void SetPlayerName(string value)
    {
        PhotonNetwork.playerName = inputField.text + " ";
        PlayerPrefs.SetString(playerNamePrefKey, inputField.text);
	}

    #endregion
}
