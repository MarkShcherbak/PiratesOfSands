using System;
using UnityEngine;
public class PilotFactory
{
    public static PlayerPilotModelView CreatePlayerPilotModelView()
    {
        GameObject testPlayerPilotPrefab = Resources.Load<GameObject>("Prefabs/Pilot/TestPlayer");
        PlayerPilotModelView modelView = UnityEngine.Object.Instantiate(testPlayerPilotPrefab)
            .GetComponent<PlayerPilotModelView>();
        return modelView;
    }

    public static PlayerPilotController CreatePlayerPilotController(PlayerPilotModelView playerMV, ShipModelView shipMV)
    {
        return new PlayerPilotController(playerMV, shipMV);
    }


    //TODO сделать фабрику для создания пилота противника
    //public static AIPilotModelView()
    //public static AIPilotController(ShipModelView shipMV, TrackModelView trackMV)
}
