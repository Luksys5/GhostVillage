using UnityEngine;
namespace LEGProductions
{
    public class GameController : Photon.PunBehaviour {

        private void Start()
        {
            Transform spawnPoint = GameObject.FindGameObjectWithTag(Tags.Spawn).transform;
            PhotonNetwork.Instantiate("PlayerCube", spawnPoint.position, Quaternion.identity, 0);
        }



    }

}