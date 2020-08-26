using Cinemachine;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab = null;
    [SerializeField] private CinemachineFreeLook playerCamera = null;

    void Start()
    {
        var player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        playerCamera.Follow = player.transform;
        playerCamera.LookAt = player.transform;
    }
}
