using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> motherDots;
    
    // Start is called before the first frame update
    void Start()
    {
        GenerateMotherDots();
    }

    private void GenerateMotherDots()
    {
        foreach (GameObject motherDotPrefab in motherDots)
        {
            GameObject motherDotGO = Instantiate(motherDotPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            motherDotGO.GetComponent<MotherDot>().Init();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
