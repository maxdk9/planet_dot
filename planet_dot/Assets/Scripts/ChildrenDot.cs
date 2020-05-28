using System;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace DefaultNamespace
{
    public class ChildrenDot: DotBase
    {

        private MotherDot mother;
        public Vector2 directionPoint;
        public float speed = .5f;

        public bool attack;
        
        private void Start()
        {
            
        }

        public void Init(MotherDot motherDot)
        {
            Selected = motherDot.Selected;
            this.side = motherDot.side;
            this.mother = motherDot;
        }


        private void Update()
        {
            if (attack)
            {
                transform.position = Vector3.MoveTowards(transform.position, directionPoint, speed*Time.deltaTime);    
            }
            
        }

        public void Kill()
        {
            this.mother.ChildrenDots.Remove(this);
            GameObject.Destroy(this.gameObject);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {

            ChildrenDot otherDot = other.gameObject.GetComponent<ChildrenDot>();
            MotherDot otherMother = other.gameObject.GetComponent<MotherDot>();
            
            
            if (otherDot != null)
            {

                if (this.side != otherDot.side)
                {
                    otherDot.Kill();
                    this.Kill();
                }

                return;
            }

            if (otherMother != null)
            {
                if (otherMother.side != this.side)
                {
                    this.Kill();
                    otherMother.Hit();


                }
            }
            
            
        }
    }
}