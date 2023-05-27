using System;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Realtime;

public class PlayerManager : MonoBehaviourPunCallbacks, IShootObserver
{
    Hashtable props = new Hashtable();

    private StartMenu startControler;
    private UI_Controller uiConrtoller;
    private Generator generator;

    new private PhotonView photonView;

    public override void OnEnable()
    {
        var levelManager = GameObject.Find("LevelManager");
        startControler = levelManager.GetComponent<StartMenu>();
        uiConrtoller = levelManager.GetComponent<UI_Controller>();
        generator = levelManager.GetComponent<Generator>();

        Invoke(nameof(InitPhotonView), 1.1f);

        props.Add(SettingsManager.Team, startControler.MyPlayer.GetType().ToString());
        props.Add(SettingsManager.Kills, 0);
        props.Add(SettingsManager.Deaths, 0);
        props.Add(SettingsManager.Health, 0);
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);
    }

    private void InitPhotonView()
    {
        photonView = generator.instantiatedPlayer.GetComponent<PhotonView>();
    }

    public void ShootUpdate(GameObject hittedPlayerObj, int damage)
    {
        Player hittedPlayer = hittedPlayerObj.GetComponent<PhotonView>().Owner;
        Hashtable hittedPlayerProps = hittedPlayer.CustomProperties;
        int hittedPlayerHealth = (int)hittedPlayerProps[SettingsManager.Health];
        hittedPlayerHealth -= damage;
        print(damage);
        hittedPlayerProps[SettingsManager.Health] = hittedPlayerHealth;
        hittedPlayer.CustomProperties = hittedPlayerProps;
        hittedPlayer.SetCustomProperties(hittedPlayerProps);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if ((int)changedProps[SettingsManager.Health] <= 0)
        {
            int playersInMyTeam = 0;
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                if((string)player.CustomProperties[SettingsManager.Team] == startControler.MyPlayer.GetType().ToString())
                {
                    playersInMyTeam++;
                }
            }
            if(playersInMyTeam == 0)
            {
                photonView.RPC("RestartLevel", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    public void RestartLevel()
    {
        print("Restart");
        uiConrtoller.UpdateLevel(startControler.MyPlayer);
        generator.GenerateLevel(startControler.MyPlayer);
    }
}