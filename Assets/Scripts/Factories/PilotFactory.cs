using System;
using UnityEngine;
public class PilotFactory
{
    public static PlayerPilotModelView CreatePlayerPilotModelView(Transform parentShip)
    {
        GameObject testPlayerPilotPrefab = Resources.Load<GameObject>("Prefabs/Pilot/TestPlayer");
        PlayerPilotModelView modelView = UnityEngine.Object.Instantiate(testPlayerPilotPrefab, parentShip)
            .GetComponent<PlayerPilotModelView>();
        return modelView;
    }

    public static PlayerPilotController CreatePlayerPilotController(PlayerPilotModelView playerMV, ShipModelView shipMV)
    {
        return new PlayerPilotController(playerMV, shipMV);
    }

    public static EnemyPilotModelView CreateEnemyPilotModelView(Transform parentShip)
    {
        GameObject testEnemyPilotPrefab = Resources.Load<GameObject>("Prefabs/Pilot/TestEnemy");
        EnemyPilotModelView modelView = UnityEngine.Object.Instantiate(testEnemyPilotPrefab,parentShip)
            .GetComponent<EnemyPilotModelView>();
        return modelView;
    }
    
    public static EnemyPilotController CreateEnemyPilotController(EnemyPilotModelView enemyMV, ShipModelView shipMV, TrackPath checkpoints)
    {
        return new EnemyPilotController(enemyMV, shipMV, checkpoints);
    }
    
}
