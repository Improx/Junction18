using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.U2D;

public class Player : NetworkBehaviour
{
    public PlayerType Team;

    public CinemachineVirtualCamera RobberCameraPrefab;

    private Robber _robber;

    private void Start()
    {
        if (Team == PlayerType.Robber)
        {
            _robber = gameObject.GetComponent<Robber>();
        }

        //If we're not local player:
        if (!isLocalPlayer)
        {
            if (Team == PlayerType.Guard)
            {
                Transform robberLight = GetComponentInChildren<FlashlightAreaRobbers>().transform;
                robberLight.position = new Vector3(robberLight.position.x, robberLight.position.y, 0);
            }
            return;
        }
        //We are local player:
        if (Team == PlayerType.Robber)
        {
            var mainCamera = FindObjectOfType<Camera>();

            var vmCam = Instantiate(RobberCameraPrefab, new Vector3(0, 0, -200), new Quaternion());
            var nightVision = vmCam.gameObject.AddComponent<DeferredNightVisionEffect>();
            nightVision.m_LightSensitivityMultiplier = 0.1f;

            vmCam.GetComponent<Camera>().orthographic = true;
            vmCam.Follow = transform;
            vmCam.m_Lens.OrthographicSize = 3;
            mainCamera.GetComponent<CinemachineBrain>().enabled = true;
            mainCamera.GetComponent<PixelPerfectCamera>().enabled = true;

            BigStencilMask.Instance.transform.position = new Vector3(BigStencilMask.Instance.transform.position.x, BigStencilMask.Instance.transform.position.y, -10);
        }

        else
        {
            //We are Guard

        }
    }

    public void ScoreItem(Item currentItem)
    {
        if (!isLocalPlayer) return;

        CmdScoreItem(currentItem.gameObject);
    }

    [Command]
    public void CmdScoreItem(GameObject item)
    {
        GameManager.Instance.RobberPoints += 1;
        RpcScoreItem(item);
    }

    [ClientRpc]
    public void RpcScoreItem(GameObject item)
    {
        Destroy(item.gameObject);
    }

    public void GrabItem(Item item)
    {
        if (!isLocalPlayer) return;

        CmdGrab(item.gameObject);
    }

    [Command]
    public void CmdGrab(GameObject item)
    {

        RpcGrab(item);
    }

    [ClientRpc]
    public void RpcGrab(GameObject item)
    {
        item.transform.parent = GetComponent<CollectItems>().carryLocation;
    }

    public void UnGrab(Item item)
    {
        if (!isLocalPlayer) return;

        CmdUnGrab(item.gameObject);
    }

    [Command]
    public void CmdUnGrab(GameObject item)
    {

        RpcUnGrab(item);
    }

    [ClientRpc]
    public void RpcUnGrab(GameObject item)
    {
        item.transform.parent = null;
        item.transform.position = GetComponent<Transform>().position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!isLocalPlayer) return;
        var otherPlayer = other.rigidbody.GetComponent<Player>();

        if (!otherPlayer) return;

        if (otherPlayer &&
            otherPlayer.Team == PlayerType.Robber &&
            Team == PlayerType.Guard)
        {
            GameManager.Instance.CmdCapture(this.gameObject, otherPlayer.gameObject);
        }
    }

    [ClientRpc]
    public void RpcGetCaptured()
    {
        if (isLocalPlayer)
        {
            GetComponent<Robber>().Detain();
        }
        GetComponent<Robber>().Detained = true;
        GetComponent<Robber>().ShowDetainedSprite();
    }

    [ClientRpc]
    public void RpcDisplayEndSCreen()
    {
        GameEndScreen.Instance.Display();
    }

    private void Update()
    {
        if (isLocalPlayer && Team == PlayerType.Robber)
        {
            FlashScreenOverlay.SetAmount(_robber.FlashlightRadiance);
        }
    }
}