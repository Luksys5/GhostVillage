using Photon;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

namespace LEGProductions
{
    public class NetworkManager : PunBehaviour {

        public InputField playerName;
        private InputField Room;
        private string connectState = "";

        private const string appId = "24d3a1f4-57b0-46d1-8e62-a96a1aa64df8";
        private const string ROOM_NAME = "GuessNumber";
        private const string SERVER_VERSION = "v1.0";


        // Use this for initialization
        void Start()
        {
            Room = GameObject.FindGameObjectWithTag(Tags.RoomName)?.GetComponent<InputField>();
        }

        private void OnGUI()
        {
            GUI.TextField(new Rect(0, 0, 200, 30), connectState);
        }

        private void Update()
        {
            if (connectState != PhotonNetwork.connectionStateDetailed.ToString())
                connectState = PhotonNetwork.connectionStateDetailed.ToString();
        }

        public void Connect()
        {
            //PhotonNetwork.ConnectToMaster("127.0.0.1", 5055, string.Empty, "1.0");
            PhotonNetwork.ConnectToRegion(CloudRegionCode.eu, SERVER_VERSION);
            PhotonNetwork.logLevel = PhotonLogLevel.Full;
            PhotonNetwork.automaticallySyncScene = true;
        }

        public void Host()
        {
            if (!PhotonNetwork.connectedAndReady)
            {
                Debug.Log("Couldn't host room. Try relaunching photon server");
                return;
            }
            else if (string.IsNullOrEmpty(playerName.text))
            {
                Debug.Log("Player name input must be filled!");
                return;
            }
            else if (string.IsNullOrEmpty(Room.text))
            {
                Debug.Log("Set game room name!");
                return;
            }

            RoomOptions roomOpts = new RoomOptions() { IsVisible = true, MaxPlayers = 2 };
            PhotonNetwork.CreateRoom(Room.text, roomOpts, TypedLobby.Default);
        }


        public void Join()
        {
            if (!PhotonNetwork.connectedAndReady)
            {
                Debug.Log("Couldn't join room. Try relaunching photon server");
                return;
            }
            else if (string.IsNullOrEmpty(playerName.text))
            {
                Debug.Log("Player name input must be filled!");
                return;
            } 
            else if (string.IsNullOrEmpty(Room.text))
            {
                Debug.Log("Room name must be filled");
                return;
            }

            bool roomExist = PhotonNetwork.GetRoomList().Count(room => room.Name == Room.text) > 0;
            if(roomExist)
                PhotonNetwork.JoinRoom(Room.text);
            else
            {
                Debug.Log("Specified room doesn't exist");
            }
        }

        public override void OnFailedToConnectToPhoton(DisconnectCause cause)
        {
            Debug.Log("Couldn't connect to server check if it's running. Adress: " + PhotonNetwork.ServerAddress);
            
            return;
        }


        public override void OnJoinedRoom()
        {
            
            SceneManager.LoadScene(BuildIndex.Level1);
        }

        public void OnBackPresed()
        {
            SceneManager.LoadScene(BuildIndex.MainMenu);
        }

        public void OnQuitGame()
        {
            PhotonNetwork.Disconnect();
            Application.Quit();
        }

    }
}