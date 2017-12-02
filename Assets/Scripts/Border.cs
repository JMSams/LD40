using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FallingSloth.LD40
{
    public class Border : MonoBehaviour
    {
        public Sides side;

        public float thickness = 1f;

        void Start()
        {
            Camera cam = Camera.main;
            switch (side)
            {
                case Sides.Top:
                    transform.position = new Vector2(0f, cam.orthographicSize + (thickness / 2f));
                    transform.localScale = new Vector3(cam.orthographicSize * cam.aspect * 2.5f, thickness, 1f);
                    break;
                case Sides.Bottom:
                    transform.position = new Vector2(0f, -cam.orthographicSize - (thickness / 2f));
                    transform.localScale = new Vector3(cam.orthographicSize * cam.aspect * 2.5f, thickness, 1f);
                    break;
                case Sides.Left:
                    transform.position = new Vector2((-cam.orthographicSize * cam.aspect) - (thickness / 2f), 0f);
                    transform.localScale = new Vector3(thickness, cam.orthographicSize * 2.5f, 1f);
                    break;
                case Sides.Right:
                    transform.position = new Vector2((cam.orthographicSize * cam.aspect) + (thickness / 2f), 0f);
                    transform.localScale = new Vector3(thickness, cam.orthographicSize * 2.5f, 1f);
                    break;
            }
        }

        public enum Sides
        {
            Top,
            Bottom,
            Left,
            Right
        }
    }
}