using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TaiChiVR.Utility.Scriptables;

public class TestManager : MonoBehaviour
{
    public InstructorListScriptable instructorListScriptable;
    public TerrainListScriptable terrainListScriptable;

    GameObject[] instructorObjList;
    RuntimeAnimatorController[] animControllerList; // 0 is cleared, 1 is nor cleared
    GameObject[] terrainObjList;

    int totalInstructorNum;
    int totalTerrainNum;

    int instructorIndex;
    int terrainIndex;

    bool isShowAll = false;

    void Awake()
    {
        totalTerrainNum = terrainListScriptable.terrainList.Length;
        terrainObjList = new GameObject[totalTerrainNum];
        for (int i = 0; i < totalTerrainNum; i++)
        {
            GameObject terrainObj = Instantiate(terrainListScriptable.terrainList[i].terrainPrefab, this.transform);

            terrainObjList[i] = terrainObj;
        }
        ///
        animControllerList = new RuntimeAnimatorController[2];
        for (int i = 0; i < 2; i++)
        {
            animControllerList[i] = instructorListScriptable.animControllerList[i];
        }
        ///
        totalInstructorNum = instructorListScriptable.instructorList.Length * 2;
        instructorObjList = new GameObject[totalInstructorNum];
        for (int i = 0; i < totalInstructorNum; i += 2)
        {
            GameObject instructorObj = Instantiate(instructorListScriptable.instructorList[i / 2].instructorPrefab, this.transform);
            instructorObj.transform.rotation = Quaternion.Euler(0, 180, 0);
            instructorObj.transform.position = new Vector3(-1, 0, 0);
            instructorObj.GetComponent<Animator>().runtimeAnimatorController = animControllerList[0];
            instructorObjList[i] = instructorObj;

            GameObject instructorObj2 = Instantiate(instructorListScriptable.instructorList[i / 2].instructorPrefab, this.transform);
            instructorObj2.transform.rotation = Quaternion.Euler(0, 180, 0);
            instructorObj2.transform.position = new Vector3(1, 0, 0);
            instructorObj2.GetComponent<Animator>().runtimeAnimatorController = animControllerList[1];
            instructorObjList[i + 1] = instructorObj2;
        }
    }

    void Start()
    {
        instructorObjList[0].SetActive(true);
        instructorObjList[1].SetActive(true);

        terrainObjList[0].SetActive(true);
    }

    public void NextInstructor()
    {
        for (int i = 0; i < totalInstructorNum; i++)
        {
            instructorObjList[i].SetActive(false);
        }

        instructorIndex += 2;
        if (instructorIndex == totalInstructorNum)
        {
            instructorIndex = 0;
        }
        instructorObjList[instructorIndex].SetActive(true);
        instructorObjList[instructorIndex + 1].SetActive(true);
    }

    public void NextTerrain()
    {
        terrainObjList[terrainIndex].SetActive(false);
        if (++terrainIndex == totalTerrainNum)
        {
            terrainIndex = 0;
        }
        terrainObjList[terrainIndex].SetActive(true);
    }

    public void ShowAllCleared()
    {
        for (int i = 0; i < totalInstructorNum; i++)
        {
            instructorObjList[i].SetActive(false);
        }

        for (int i = 0; i < totalInstructorNum; i += 2)
        {
            instructorObjList[i].SetActive(true);
        }
    }

    public void ShowAllNotCleared()
    {
        for (int i = 0; i < totalInstructorNum; i++)
        {
            instructorObjList[i].SetActive(false);
        }

        for (int i = 1; i < totalInstructorNum; i += 2)
        {
            instructorObjList[i].SetActive(true);
        }
    }
}
