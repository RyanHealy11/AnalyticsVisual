using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class FindFile : MonoBehaviour
{
    private string folderPath;

    public List<IntEvent> yes  = new List<IntEvent>();
    public List<FloatEvent> no = new List<FloatEvent>();
    public List<FloatEvent> maybe  = new List<FloatEvent>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(no.Count);
        if (no.Count>0)
        {
            for (int i = 0; i < no.Count; i++)
            {
                for (int k = 0; k < no[i].values.Count; k++)
                {
                    Debug.Log(no[i].eventName + " " + no[i].values[k]);
                }
            }
        }
        if (yes.Count>0)
        {
            for (int i = 0; i < yes.Count; i++)
            {
                for (int k = 0; k < yes[i].values.Count; k++)
                {
                    Debug.Log(yes[i].eventName + " " + yes[i].values[k]);
                }
            }
        }
        if (maybe.Count>0)
        {
            for (int i = 0; i < maybe.Count; i++)
            {
                for (int k = 0; k < maybe[i].values.Count; k++)
                {
                    Debug.Log(maybe[i].eventName + " " + maybe[i].values[k]);
                }
            }
        }
       
        
    }
    public void ShowExplorer()
    {

        //itemPath = itemPath.Replace(@"/", @"\");   // explorer doesn't like front slashes
        //iusbdjf = System.Diagnostics.Process.Start("explorer.exe", "/select," + itemPath).ToString();
        //System.Diagnostics.Process.Start()
        //iusbdjf = EditorUtility.OpenFilePanel("woooooooo", "", "txt");
        folderPath = EditorUtility.OpenFolderPanel("wooooo", "%appdata%", "");

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
                    //string[] lines = File.ReadAllLines(filePath);

                    int curEntry = 0;
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
                            no.Add(new FloatEvent( values[0], float.Parse(values[1])));
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
            //Debug.Log(fun);
        }
        
    }
}
