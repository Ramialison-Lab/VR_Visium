﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceCollider : MonoBehaviour
{
    int l_slice;
    int r_slice;
    int top_slice;
    int btm_slice;
    int depth;
    public List<GameObject> sliceColliders;
    public List<int> zcoords;
    public void setSliceCollider(int lslice, int rslice, int topslice, int btmslice, int d)
    {

        sliceColliders = new List<GameObject>();
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        var centerx = lslice + (rslice - lslice) / 2;
        var centery = btmslice + (topslice - btmslice) / 2;
        cube.transform.position = new Vector3(centerx, centery, d);
        cube.transform.localScale = new Vector3(rslice - lslice, topslice - btmslice, 1 );
        cube.GetComponent<MeshRenderer>().enabled = false;
        sliceColliders.Add(cube);
        zcoords.Add(d);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clicked();
        }
    }

    private void clicked()
    {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
 
      RaycastHit hit = new RaycastHit();

      if (Physics.Raycast (ray, out hit))
      {
          if(hit.collider.gameObject.name == "Cube")
            {
                Debug.Log(Mathf.Round(hit.point.x) +", "+ Mathf.Round(hit.point.y));
                GameObject.Find("ScriptHolder").GetComponent<SpotDrawer>().identifySpot((int)hit.point.x, (int)hit.point.y, zcoords[0]);
            }
      }
    }
}
