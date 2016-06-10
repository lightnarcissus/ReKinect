/*
	CSVReader by Dock. (24/8/11)
	http://starfruitgames.com
 
	usage: 
	CSVReader.SplitCsvGrid(textString)
 
	returns a 2D string array. 
 
	Drag onto a gameobject for a demo of CSV parsing.
*/

using UnityEngine;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
public class CSVReader : MonoBehaviour
{
    public TextAsset csvFile;
    private bool found = false;
    public string filePath = "";
    public void Start()
    {
       // CSVWrite();
        string[,] grid = SplitCsvGrid(csvFile.text);
        Debug.Log("size = " + (1 + grid.GetUpperBound(0)) + "," + (1 + grid.GetUpperBound(1)));

        DebugOutputGrid(grid);
    }

    // outputs the content of a 2D array, useful for checking the importer
    static public void DebugOutputGrid(string[,] grid)
    {
        string textOutput = "";
        for (int y = 0; y < grid.GetUpperBound(1); y++)
        {
            for (int x = 0; x < grid.GetUpperBound(0); x++)
            {

                textOutput += grid[x, y];
                textOutput += "|";
            }
            textOutput += "\n";
        }
        Debug.Log(textOutput);
    }

    // splits a CSV file into a 2D string array
    static public string[,] SplitCsvGrid(string csvText)
    {
        string[] lines = csvText.Split("\n"[0]);

        // finds the max width of row
        int width = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            string[] row = SplitCsvLine(lines[i]);
            width = Mathf.Max(width, row.Length);
        }

        // creates new 2D string grid to output to
        string[,] outputGrid = new string[width + 1, lines.Length + 1];
        for (int y = 0; y < lines.Length; y++)
        {
            string[] row = SplitCsvLine(lines[y]);
            for (int x = 0; x < row.Length; x++)
            {
                outputGrid[x, y] = row[x];

                // This line was to replace "" with " in my output. 
                // Include or edit it as you wish.
                outputGrid[x, y] = outputGrid[x, y].Replace("\"\"", "\"");
            }
        }

        return outputGrid;
    }

    // splits a CSV row 
    static public string[] SplitCsvLine(string line)
    {
        return (from System.Text.RegularExpressions.Match m in System.Text.RegularExpressions.Regex.Matches(line,
        @"(((?<x>(?=[,\r\n]+))|""(?<x>([^""]|"""")+)""|(?<x>[^,\r\n]+)),?)",
        System.Text.RegularExpressions.RegexOptions.ExplicitCapture)
                select m.Groups[1].Value).ToArray();
    }
    public void CSVWrite(string name, string age, int focusSide)
    {
        var csv = new StringBuilder();
        string focusString = "";
        if (focusSide == 0)
            focusString = "Left Hand";
        else
            focusString = "Right Hand";
   //     var newLine = string.Format("{0},{1},{2},{3}", System.DateTime.Now, name, age, focusString);
      //  string[] ok = File.ReadAllLines(filePath);
        string[,] nice = SplitCsvGrid(File.ReadAllText(filePath));
        for(int i=0;i<nice.GetUpperBound(1);i++)
        {
           // Debug.Log(nice[1, i]);
            if(nice[1,i]!=null)
             if(nice[1,i].Contains(name))
            {
                    found = true;
                nice[0, i] = System.DateTime.Now.ToString();
                nice[1, i] = name;
                nice[2, i] = age;
                nice[3, i] = focusString;
                SceneManager.totalTimePlayed =float.Parse(nice[4, i]);
                SceneManager.levelTime[0] = float.Parse(nice[5, i]);
                SceneManager.levelTime[1] = float.Parse(nice[6, i]);
                SceneManager.levelTime[2] = float.Parse(nice[7, i]);
                SceneManager.levelTime[3] = float.Parse(nice[8, i]);
                SceneManager.levelTime[4] = float.Parse(nice[9, i]);
                SceneManager.levelTime[5] = float.Parse(nice[10, i]);
                SceneManager.levelTime[6] = float.Parse(nice[11, i]);
                Debug.Log("total time played is " + SceneManager.totalTimePlayed);
             }
            
        }
        if(!found)
        {
            var newLine = string.Format("{0},{1},{2},{3}, {4}, {5},{6},{7},{8},{9},{10},{11}", System.DateTime.Now.ToString(),name,age,focusString,0,0,0,0,0,0,0,0);
            csv.AppendLine(newLine);
        }
        else
        {
            found = false;
        }

        for (int i = 0; i < nice.GetUpperBound(1); i++)
        {
            var newLine = string.Format("{0},{1},{2},{3}, {4}, {5},{6},{7},{8},{9},{10},{11}", nice[0, i], nice[1, i], nice[2, i], nice[3, i], nice[4, i], nice[5, i], nice[6, i], nice[7, i], nice[8, i], nice[9, i], nice[10, i], nice[11, i]);
            csv.AppendLine(newLine);
        }

            //after your loop
            File.WriteAllText(filePath, csv.ToString());
        }
    public void CSVRewrite(string name, string age, int focusSide)
    {
        var csv = new StringBuilder();
        string focusString = "";
        if (focusSide == 0)
            focusString = "Left Hand";
        else
            focusString = "Right Hand";

        float totalTime = SceneManager.totalTimePlayed;
        float draw1 = SceneManager.levelTime[0];
        float draw2 = SceneManager.levelTime[1];
        float draw3 = SceneManager.levelTime[2];
        float card1 = SceneManager.levelTime[3];
        float card2 = SceneManager.levelTime[4];
        float card3 = SceneManager.levelTime[5];
        float orch = SceneManager.levelTime[6];
        //in your loop
        //  var first = "Ansh";
        // var second = "Patel";
        //Suggestion made by KyleMit
        string[,] nice = SplitCsvGrid(File.ReadAllText(filePath));
        for (int i = 0; i < nice.GetUpperBound(1); i++)
        {
          //  Debug.Log(nice[1, i]);
            if (nice[1, i] != null)
                if (nice[1, i].Contains(name))
                {
                    found = true;
                    nice[0, i] = System.DateTime.Now.ToString();
                    nice[1, i] = name;
                    nice[2, i] = age;
                    nice[3, i] = focusString;
                    nice[4, i] = totalTime.ToString("F2");
                    nice[5, i] = draw1.ToString("F2");
                    nice[6, i] = draw2.ToString("F2");
                    nice[7, i] = draw3.ToString("F2");
                    nice[8, i] = card1.ToString("F2");
                    nice[9, i] = card2.ToString("F2");
                    nice[10, i] = card3.ToString("F2");
                    nice[11, i] = orch.ToString("F2");

                }

        }
        if (!found)
        {
            var newLine = string.Format("{0},{1},{2},{3}, {4}, {5},{6},{7},{8},{9},{10},{11}", System.DateTime.Now.ToString(), name, age, focusString, 0, 0, 0, 0, 0, 0, 0, 0);
            csv.AppendLine(newLine);
        }
        else
        {
            found = false;
        }

        for (int i = 0; i < nice.GetUpperBound(1); i++)
        {
            var newLine = string.Format("{0},{1},{2},{3}, {4}, {5},{6},{7},{8},{9},{10},{11}", nice[0, i], nice[1, i], nice[2, i], nice[3, i], nice[4, i], nice[5, i], nice[6, i], nice[7, i], nice[8, i], nice[9, i], nice[10, i], nice[11, i]);
            csv.AppendLine(newLine);
        }

        //after your loop
        File.WriteAllText(filePath, csv.ToString());
    }
}