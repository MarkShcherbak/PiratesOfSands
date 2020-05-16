using System;
using UnityEngine;

// Ship factory, creates different ships and their controllers
public class ShipFactory
{
    public static ShipModelView CreateShipModelView()
    {
            GameObject testShipPrefab = Resources.Load<GameObject>("Prefabs/Ship/TestShip");
            ShipModelView modelView = UnityEngine.Object.Instantiate(testShipPrefab)
                .GetComponent<ShipModelView>();
        modelView.transform.position = new Vector3(60, 0, 220);
            return modelView;
    }

    public static ShipController CreateShipController(ShipModelView shipMV)
    {
        return new ShipController(shipMV);
    }
}
