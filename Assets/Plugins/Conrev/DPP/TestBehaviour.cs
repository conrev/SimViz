using UnityEngine;
using DroneToolbox.FlightPath;
using DroneToolbox;

public class TestBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(DroneFileReader.ReadSRT("Test_Flight.SRT"));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
