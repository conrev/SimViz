using System;
using FLOW;
using UnityEngine;
using System.Threading.Tasks;

public class FluidSimRunManager : MonoBehaviour
{

    [SerializeField]
    FlowSnapshot snapshotTool;

    [SerializeField]
    FlowSimulation simulation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public async void LoadSimulationStateAsync(String prefix)
    {
        string filenameFormat = prefix + "{0}.json";

        string finalPath = System.IO.Path.Combine(Application.persistentDataPath, string.Format(filenameFormat, simulation.name));

        FlowSnapshotData snapshot = new FlowSnapshotData();

        string json = "";

        await Task.Run(() =>
        {
            if (simulation != null)
            {
                json = System.IO.File.ReadAllText(finalPath);
                JsonUtility.FromJsonOverwrite(json, snapshot);
            }

        });

        snapshot.ConvertCompressedToReadable();
        snapshot.ConvertReadableToRaw();
        snapshot.RawToSimulation(simulation);
        simulation.Simulating = true;
    }

    public void LoadSimulationState(String prefix)
    {
        string filenameFormat = prefix + "{0}.json";
        snapshotTool.Title = filenameFormat;
        snapshotTool.LoadFromFile();
        simulation.Simulating = true;

    }
}
