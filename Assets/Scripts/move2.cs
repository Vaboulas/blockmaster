using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class move2 : MonoBehaviour
{
    private bool isRotating = false;
    public BoxCollider col;
    private bool isstanding = true;
    private int dir = 0;
    public Text movesText; // Assign this in Inspector
    public int moveCount = 0;

    void Update()
    {
        if (!isRotating)
        {

            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                if (isstanding == true && dir == 0)
                {
                    dir = 2;
                }
                else if (isstanding == false && dir == 2)
                {
                    dir = 0;
                }
                if (dir == 0)
                {
                    transform.position = new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z);

                }
                else if (dir == 2)
                {
                    transform.position = new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z);

                }
                else if (dir == 1)
                {
                    transform.position = new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z);

                }
                StartCoroutine(RotateAndWait(90.0f, 1));
            }
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {

                if (isstanding == true && dir == 0)
                {
                    dir = 2;
                }
                else if (isstanding == false && dir == 2)
                {
                    dir = 0;
                }
                if (dir == 0)
                {
                    transform.position = new Vector3(transform.position.x - 1.5f, transform.position.y, transform.position.z);

                }
                else if (dir == 2)
                {
                    transform.position = new Vector3(transform.position.x - 1.5f, transform.position.y, transform.position.z);

                }
                else if (dir == 1)
                {
                    transform.position = new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z);

                }

                StartCoroutine(RotateAndWait(-90.0f, 1));
            }
            else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                if (isstanding == true && dir == 0)
                {
                    dir = 1;
                }
                else if (isstanding == false && dir == 1)
                {
                    dir = 0;
                }
                if (dir == 0)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1.5f);

                }
                else if (dir == 1)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1.5f);

                }
                else if (dir == 2)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1f);

                }
                StartCoroutine(RotateAndWait(90.0f, 2));

            }
            else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                if (isstanding == true && dir == 0)
                {
                    dir = 1;
                }
                else if (isstanding == false && dir == 1)
                {
                    dir = 0;
                }
                if (dir == 0)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1.5f);

                }
                else if (dir == 1)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1.5f);

                }
                else if (dir == 2)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1f);

                }
                StartCoroutine(RotateAndWait(90.0f, 2));
            }
        }

        var yHalfExtents = col.bounds.extents.y;
        //get the center
        var yCenter = col.bounds.center.y;

        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hitData;
        var raycasthit = Physics.Raycast(ray, out hitData, 13);


        Debug.DrawRay(transform.position, Vector3.down, Color.green);

        if (!raycasthit)
        {
            return;
        }

        var offset = hitData.distance - yHalfExtents;

        transform.position = new Vector3(transform.position.x, transform.position.y - offset, transform.position.z);

        if (transform.position.y > 10.7f)
        {
            isstanding = true;

        }
        else
        {
            isstanding = false;
        }

    }

    void UpdateMoveText()
    {
        if (movesText != null)
        {
            movesText.text = "Moves: " + moveCount;
        }
    }

    private IEnumerator RotateAndWait(float angle, int axis)
    {
        isRotating = true;
        if (axis == 1)
        {
            transform.Rotate(0.0f, 0.0f, angle, Space.World);
        }
        else if (axis == 2)
        {
            transform.Rotate(angle, 0.0f, 0.0f, Space.World);
        }



        moveCount++;
        UpdateMoveText();

        yield return new WaitForSeconds(1f);
        isRotating = false;

    }
}