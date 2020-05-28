using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MotherDot : DotBase
{
    public Vector2 startPosition;
    public float childGenerationSpeed = 0.5f;
    public float childRadius = 2f;
    [SerializeField]
    int hp = 10;
    public Text text;
    public List<ChildrenDot> ChildrenDots = new List<ChildrenDot>();
    public GameObject childPrefab;
    public int maxChildNumber = 5;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {

            yield return new WaitForSeconds(childGenerationSpeed);

            if (ChildrenDots.Count < maxChildNumber)
            {
                

                GameObject childrenDotGO = Instantiate(childPrefab, new Vector3(0, 0, 0), Quaternion.identity);

                float a = Random.Range(0f, Mathf.PI * 2);
                Vector2 childPosition =
                    (Vector2) transform.position + new Vector2(Mathf.Cos(a), Mathf.Sin(a)) * childRadius;
                childrenDotGO.transform.position = childPosition;

                ChildrenDot childrenDot = childrenDotGO.GetComponent<ChildrenDot>();
                childrenDot.Init(this);

                ChildrenDots.Add(childrenDot);
            }
        }
    }

    public void Init()
    {
        if (startPosition != null)
        {
            transform.position = startPosition;
        }

        Selected = false;
        UpdateHpText();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SelectMother(bool pSelect)
    {
        this.Selected = pSelect;
        foreach (ChildrenDot child in ChildrenDots)
        {
            child.Selected = this.Selected;
        }
    }

    public void Attack(Vector3 attackedPosition)
    {
        foreach (ChildrenDot childrenDot in ChildrenDots)
        {
            childrenDot.attack = true;
            childrenDot.directionPoint = attackedPosition;
        }
    }


    private void OnMouseDown()
    {
        if (this.Selected)
        {
            this.SelectMother(false);
            
        }

        else
        {
            MotherDot opponentMotherDot = GetSelectedMotherDot();
            if (opponentMotherDot != null)
            {
                opponentMotherDot.Attack(transform.position);
                opponentMotherDot.SelectMother(false);
            }
            else
            {
                this.SelectMother(true);
            }
        }
    }


    private void UpdateHpText()
    {
        this.text.text = hp.ToString();
    }
    private static MotherDot GetSelectedMotherDot()
    {
        MotherDot[] motherDots = GameObject.FindObjectsOfType<MotherDot>();
        foreach (MotherDot motherDot in motherDots)
        {
            if (motherDot.Selected)
            {
                return motherDot;
            }
        }

        return null;
    }

    public void Hit()
    {
        hp--;
        UpdateHpText();
        if (hp <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}