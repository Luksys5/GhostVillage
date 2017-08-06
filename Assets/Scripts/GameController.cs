using UnityEngine;
namespace LEGProductions
{
    public class GameController : Photon.PunBehaviour {

        private void Start()
        {
            if(PhotonNetwork.connected == false)
            {
                Debug.Log("Connected to Photon to spawn player");
                return;
            }
            Transform spawnPoint = GameObject.FindGameObjectWithTag(Tags.Spawn).transform;
            PhotonNetwork.Instantiate("PlayerCube", spawnPoint.position, Quaternion.identity, 0);
        }



    }

}