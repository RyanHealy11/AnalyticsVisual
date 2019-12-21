using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;


public class FindFile : MonoBehaviour
{
    private string folderPath;

    public List<IntEvent> yes = new List<IntEvent>();
    public List<FloatEvent> no = new List<FloatEvent>();
    public List<FloatEvent> maybe = new List<FloatEvent>();
    public List<float> so = new List<float>();
    public Text totalTimes;
    public Text SceneTimes;
    public Text FloatEvents;
    public Text IntEvents;

    // Start is called before the first frame update
    void Start()
    { }
       

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(no.Count);
        //if (no.Count>0)
        //{
        //    for (int i = 0; i < no.Count; i++)
        //    {
        //        for (int k = 0; k < no[i].values.Count; k++)
        //        {
        //            Debug.Log(no[i].eventName + " " + no[i].values[k]);
        //        }
        //    }
        //}
        //if (yes.Count>0)
        //{
        //    for (int i = 0; i < yes.Count; i++)
        //    {
        //        for (int k = 0; k < yes[i].values.Count; k++)
        //        {
        //            Debug.Log(yes[i].eventName + " " + yes[i].values[k]);
        //        }
        //    }
        //}
        //if (maybe.Count>0)
        //{
        //    for (int i = 0; i < maybe.Count; i++)
        //    {
        //        for (int k = 0; k < maybe[i].values.Count; k++)
        //        {
        //            Debug.Log(maybe[i].eventName + " " + maybe[i].values[k]);
        //        }
        //    }
        //}

        //if (so.Count > 0)
        //{
        //    for (int i = 0; i < so.Count; i++)
        //    {
        //        Debug.Log(so[i]);
        //    }
        //}
    }
    public void LoadData()
    {
        //FileBrowser.WaitForLoadDialog(false, null, "Load File", "Load");
        //StartCoroutine(ShowLoadDialogCoroutine());
        //folderPath = EditorUtility.OpenFolderPanel("wooooo", "%appdata%", "");
        folderPath = "%UserProfile%/AppData/LocalLow/DefaultCompany/LiveWallpaper-master";
        //folderPath = "%UserProfile%";
        folderPath = System.Environment.ExpandEnvironmentVariables(folderPath);
        string[] filePaths = Directory.GetFiles(folderPath, "*.dat");

        foreach (string fun in filePaths)
        {
            FileStream file;
            if (fun.Contains("Scenetimes_"))
            {
                if (File.Exists(fun))
                {
                    file = File.OpenRead(fun);


                    BinaryFormatter bf = new BinaryFormatter();

                    string[] lines = (string[])bf.Deserialize(file);
                    file.Close();

                    foreach (string line in lines)
                    {
                        string[] values = line.Split(',');
                        if (no.Count != 0)
                        {
                            string temp = values[0];
                            bool found = false;
                            for (int i = 0; i < no.Count; i++)
                            {
                                if (no[i].eventName == temp)
                                {
                                    no[i].values.Add(float.Parse(values[1]));
                                    found = true;
                                    break;
                                }
                            }
                            if (!found)
                            {
                                no.Add(new FloatEvent(values[0], float.Parse(values[1])));
                            }
                        }
                        else
                        {
                            no.Add(new FloatEvent(values[0], float.Parse(values[1])));
                        }
                    }
                }
            }
            else if (fun.Contains("FloatEvents_"))
            {
                if (File.Exists(fun))
                {
                    file = File.OpenRead(fun);

                    BinaryFormatter bf = new BinaryFormatter();

                    string[] lines = (string[])bf.Deserialize(file);
                    file.Close();

                    foreach (string line in lines)
                    {
                        string[] values = line.Split(',');
                        if (maybe.Count != 0)
                        {
                            string temp = values[0];
                            bool found = false;
                            for (int i = 0; i < maybe.Count; i++)
                            {
                                if (maybe[i].eventName == temp)
                                {
                                    maybe[i].values.Add(float.Parse(values[1]));
                                    found = true;
                                    break;
                                }
                            }
                            if (!found)
                            {
                                maybe.Add(new FloatEvent(values[0], float.Parse(values[1])));
                            }
                        }
                        else
                        {
                            maybe.Add(new FloatEvent(values[0], float.Parse(values[1])));
                        }
                    }
                }
            }
            else if (fun.Contains("IntEvents_"))
            {
                if (File.Exists(fun))
                {
                    file = File.OpenRead(fun);

                    BinaryFormatter bf = new BinaryFormatter();

                    string[] lines = (string[])bf.Deserialize(file);
                    file.Close();

                    foreach (string line in lines)
                    {
                        string[] values = line.Split(',');
                        if (yes.Count != 0)
                        {
                            string temp = values[0];
                            bool found = false;
                            for (int i = 0; i < yes.Count; i++)
                            {
                                if (yes[i].eventName == temp)
                                {
                                    yes[i].values.Add(int.Parse(values[1]));
                                    found = true;
                                    break;
                                }
                            }
                            if (!found)
                            {
                                yes.Add(new IntEvent(values[0], int.Parse(values[1])));
                            }
                        }
                        else
                        {
                            yes.Add(new IntEvent(values[0], int.Parse(values[1])));
                        }
                    }
                }
            }
            else
            {
                Debug.Log("this file is not tracked " + fun);
            }
        }

        string[] times = File.ReadAllLines(folderPath + "/TotalTime.txt");
        foreach (string time in times)
        {
            string[] values = time.Split(',');
            foreach (string value in values)
            {
                so.Add(float.Parse(value));
            }
        }

        SetTextFeilds();
    }
    private void SetTextFeilds()
    {
        float totalTime = 0.0f;
        float totalTimeAverage = 0.0f;
        for (int i = 0; i < so.Count; i++)
        {
            totalTime += so[i];            
        }
        totalTimeAverage = totalTime / so.Count;

        totalTimes.text = "Total Time Open: " + totalTime.ToString() + "\n" +
                          "Average Time Open: " + totalTimeAverage.ToString();

        for (int i = 0; i < no.Count; i++)
        {
            float sceneTotalTime = 0.0f;
            float sceneAverageTime = 0.0f;

            for (int k = 0; k < no[i].values.Count; k++)
            {
                sceneTotalTime += no[i].values[k];
            }
            sceneAverageTime = sceneTotalTime / no[i].values.Count;


            if (SceneTimes.text.Length == 0)
            {
                SceneTimes.text = SceneTimes.text + no[i].eventName + "\n" + "Scene Total Time: " + sceneTotalTime + "\n" + "Scene Average Time: " + sceneAverageTime;
            }
            else
            {
                SceneTimes.text = SceneTimes.text + "\n" + no[i].eventName + "\n" + "Scene Total Time: " + sceneTotalTime + "\n" + "Scene Average Time: " + sceneAverageTime;
            }
            SceneTimes.text = SceneTimes.text + "\n";
        }
        Debug.Log(maybe.Count);
        for (int i = 0; i < maybe.Count; i++)
        {
            float sceneTotalTime = 0.0f;
            float sceneAverageTime = 0.0f;

            for (int k = 0; k < maybe[i].values.Count; k++)
            {
                sceneTotalTime += maybe[i].values[k];
            }
            sceneAverageTime = sceneTotalTime / maybe[i].values.Count;

            
            if (FloatEvents.text.Length == 0)
            {
                FloatEvents.text = FloatEvents.text + maybe[i].eventName + "\n" + "Event Total: " + sceneTotalTime + "\n" + "Event Average: " + sceneAverageTime;
            }
            else
            {
                FloatEvents.text = FloatEvents.text + "\n" + maybe[i].eventName + "\n" + "Event Total: " + sceneTotalTime + "\n" + "Event Average: " + sceneAverageTime;
            }
            FloatEvents.text = FloatEvents.text + "\n";
        }
        Debug.Log(yes.Count);
        for (int i = 0; i < yes.Count; i++)
        {
            int sceneTotalTime = 0;
            float sceneAverageTime = 0.0f;

            for (int k = 0; k < yes[i].values.Count; k++)
            {
                sceneTotalTime += yes[i].values[k];
            }
            sceneAverageTime = sceneTotalTime / yes[i].values.Count;


            if (SceneTimes.text.Length == 0)
            {
                IntEvents.text = IntEvents.text + yes[i].eventName + "\n" + "Event Total: " + sceneTotalTime + "\n" + "Event Average: " + sceneAverageTime;
            }
            else
            {
                IntEvents.text = IntEvents.text + "\n" + yes[i].eventName + "\n" + "Event Total: " + sceneTotalTime + "\n" + "Event Average: " + sceneAverageTime;
            }
            IntEvents.text = IntEvents.text + "\n";
        }
    }
   
}
