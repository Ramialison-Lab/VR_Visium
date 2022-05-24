﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

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
    public List<int> resultExpression;
    public List<double> normalised;

    // search Function for gene
    public void searchGene(string datapath, int pos, string gn, SpotDrawer sp)
    {
        //pos = UnityEngine.Random.Range(0,1000);
        datapath = datapath.Replace(datapath.Split('\\').Last(), "") + "TransposedTest.csv";
        StartCoroutine(search(datapath, pos, gn));
        sp.setColors(normalised);
    }

    IEnumerator search(string dp, int pos, string gn)
    {
        string[] lines = File.ReadAllLines(dp);
        // Removing the string with the genename from the CSV list before parsing each entry into a int value for the list
        resultExpression = lines[pos].Remove(0, lines[pos].Split(',').First().Length + 1).Split(',').ToList().Select(int.Parse).ToList();

        var max = resultExpression.Max();
        var min = resultExpression.Min();
        var range = (double)(max - min);
        normalised
            = resultExpression.Select(i => 1 * (i - min) / range)
                .ToList();

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
