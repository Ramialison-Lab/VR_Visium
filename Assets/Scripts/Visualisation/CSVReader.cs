﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using LumenWorks.Framework.IO.Csv;

// Prototype using CSV file to read in col, row information of visium slide

public class CSVReader : MonoBehaviour
{
    private string datapath;
    public GameObject datapointPrefab;
    public GameObject parent;
    public string[] header;
    private GameObject tempObj;
    public Material Material1;
    public InputField input;
    public List<float> ev;
    //private void Start()
    //{
    //    setDatapath();
    //    readHeader("Assets/Datasets/testCsv.csv");
    //    //UpdateCSV("Assets/Datasets/tissue_positions_list.csv", "Assets/Datasets/UpdatedCSV.csv", "0","1");
    //    readDatapointPositions();
    //   // StartCoroutine(readGeneExpressionLevel());
    //}

    private async void Start()
    {
      //  setDatapath();
        
        //readDatapointPositions();


        //readHeader("Assets/Datasets/var.csv");

        //var count = await CountNonZeroEntries();
        //var count = await WriteSparse();

        //Debug.Log($"{count} cells are != \"0\".");
        //searchGeneExpression();

    }

    public void searchGene()
    {
        string gn = "NOC2L";
        datapath = "Assets/Datasets/testCsv2b.csv";


        StartCoroutine(searchLumen(gn, datapath));


    }

    IEnumerator searchLumen(string gn, string dp)
    {
        var row = new List<string>();
        using (CsvReader csv =
          new CsvReader(new StreamReader(dp), false))
        {
            int fieldCount = csv.FieldCount;

            //for (int i = 0; i < fieldCount; i++)
            //{
            //    row.Add(csv[i]);
            //}
            Debug.Log(csv[0]);


        }

        yield return null;
    }

    IEnumerator search(string gn, string dp)
    {
        
        string[] lines = File.ReadAllLines(dp);
        foreach (string line in lines)
        {
            var values = line.Split(',').ToList();

            {
                ev.Add(float.Parse(values[8]));
            }
        }

        yield return null;
    }

    private string getName()
    {
        return input.text.ToString();
    }

    public void searchGeneExpressionAsync()
    {
        string genename = getName();
        var temp = Time.time;
        var ind = Array.IndexOf(header, genename);
        if (ind == -1) Debug.Log("Gene not found...");

        searchExpressionBased(ind);
    }

    public void setDatapath()
    {
        datapath = "Assets/Datasets/tissue_positions_list.csv";
    }

    private void readHeader(string path)
    {
        header = File.ReadAllLines(path).First<string>().Split(',');        
    }

    private void searchExpressionBased(int ind)
    {
        string[] lines = File.ReadAllLines("Assets/Datasets/var.csv");
        foreach (string line in lines)
        {
            List<string> values = new List<string>();
            values = line.Split(',').ToList();

            if (values[ind] != "0")
            {
                try
                {
                    GameObject temp = GameObject.Find(values[0]);
                    temp.GetComponent<MeshRenderer>().material = Material1;
                }
                catch (Exception) { }
            }

        }
    }


    IEnumerator readGeneExpressionLevel()
    {
        string[] lines = File.ReadAllLines("Assets/Datasets/var.csv");
        foreach (string line in lines)
        {
            List<string> values = new List<string>();
            values = line.Split(',').ToList();
            try
            {
                tempObj = GameObject.Find(values[0]);
            }
            catch (Exception) { }
            int x = 1;
            List<string> temp = new List<string>();

            foreach (string val in values)
            {
                if (val != "0")
                {
                    temp.Add(header[1]);
                    temp.Add(val);
                }

                x++;
            }

            yield return new WaitForSeconds(0f);
            }
    }

    // Reads positions and names of all spots from tissue_positions_list.csv → creates datapoint for each spot using the array and rows and sets spot name
    private void readDatapointPositions()
    {
        string[] lines = File.ReadAllLines("Assets/Datasets/UpdatedCSV.csv");
        foreach (string line in lines)
        {
            List<string> values = new List<string>();
            values = line.Split(',').ToList();

            GameObject myobject = Instantiate(datapointPrefab, new Vector3(int.Parse(values[1]), int.Parse(values[2]), 0), Quaternion.identity);
            myobject.transform.SetParent(parent.transform);
            myobject.transform.name = values[0];

        }

    }

    // reads csv file path, checks for column pos for value val and deletes all that are not val into new csv file output
    public void UpdateCSV(string path, string output, string val, string pos)
    {
        string[] lines = File.ReadAllLines(path);
        StringBuilder ObjStringBuilder = new StringBuilder();
        foreach (string line in lines)
        {
            List<string> values = new List<string>();
            values = line.Split(',').ToList();
            if (values[1] != val)
            {
                //removes all values from pos 1 → not needed anymore
                values.Remove(pos);

                string newLine = string.Join(",", values);
                ObjStringBuilder.Append(newLine + Environment.NewLine);
            }
        }

        File.WriteAllText(output, ObjStringBuilder.ToString());
    }



}
