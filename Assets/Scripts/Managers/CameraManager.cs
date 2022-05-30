using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera mainCamera;
    public float screenTop, screenLeft, screenBot, screenRight;

    public static CameraManager Instance { get; private set; }

    public Vector3 MousePosition { get; private set; }
    public Ray MouseRay { get; private set; }

    [SerializeField] GameManager gm;

    void Awake()
    {
        Instance = this;

        var playerPos = gm.player.transform.position;
        var topRightCorner = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.transform.position.y - playerPos.y));
        var botLeftCorner = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.transform.position.y - playerPos.y));
        screenTop = topRightCorner.z;
        screenRight = topRightCorner.x;
        screenBot = botLeftCorner.z;
        screenLeft = botLeftCorner.x;
    }

    void Update()
    {
        MousePosition = Input.mousePosition;
        MouseRay = mainCamera.ScreenPointToRay(MousePosition);
    }
}
