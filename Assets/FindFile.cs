using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FindFile : MonoBehaviour
{
    private string folderPath;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(folderPath);
    }
    public void ShowExplorer(string itemPath)
    {

        //itemPath = itemPath.Replace(@"/", @"\");   // explorer doesn't like front slashes
        //iusbdjf = System.Diagnostics.Process.Start("explorer.exe", "/select," + itemPath).ToString();
        //System.Diagnostics.Process.Start()
        //iusbdjf = EditorUtility.OpenFilePanel("woooooooo", "", "txt");
        folderPath = EditorUtility.OpenFolderPanel("wooooo", "%appdata%", "");
    }
}
