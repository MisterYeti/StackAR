using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Place : MonoBehaviour
{
    public GameObject Prefab;

    public Transform Base;

    public Transform Current;
    public Bloc CurrentBloc;

    public Transform Last;
    public Bloc LastBloc;

    public bool isGameOver = false;
    public float arrondi = 0.01f;

    private Limit _limit;


    private void Start()
    {
        _limit = FindObjectOfType<Limit>();
    }


    public void PlaceCube()
    {
        if (!isGameOver)
        {
            CurrentBloc._moving = false;

            float distanceBetweenBoth;
            distanceBetweenBoth = CheckingDistance();
            if (isGameOver = CheckingGameOver(distanceBetweenBoth))
            {
                CurrentBloc._moving = false;
                //_limit.EndGame();
                Current.gameObject.AddComponent<Rigidbody>();
                return;
            }
            ScaleAndReplace(distanceBetweenBoth);
            InstanciateNewCube(distanceBetweenBoth);
        }
    }

    private void InstanciateNewCube(float distanceBetweenBoth)
    {
        if (!CurrentBloc._movingInZ)
        {
            
            GameObject go = Instantiate(Prefab,_limit.transform);
            //_limit.CheckAndAct();

            if (distanceBetweenBoth == 0)
            {
                go.transform.localScale = new Vector3(Current.localScale.x, Current.localScale.y, Current.localScale.z);
                go.transform.position = new Vector3(Current.position.x, Current.position.y + LastBloc._height, Base.position.z + CurrentBloc._maxDistance);
            }
            else
            {
                go.transform.localScale = new Vector3(Last.localScale.x - distanceBetweenBoth, Last.localScale.y, Last.localScale.z);
                go.transform.position = new Vector3(Current.position.x, Current.position.y + LastBloc._height, Base.position.z + CurrentBloc._maxDistance);
            }

            Last = Current;
            LastBloc = CurrentBloc;
            Current = go.transform;
            CurrentBloc = Current.GetComponent<Bloc>();
            CurrentBloc.Base = Base;
            CurrentBloc._movingInZ = !LastBloc._movingInZ;
        }

        else
        {
            GameObject go = Instantiate(Prefab, _limit.transform);
            //_limit.CheckAndAct();

            if (distanceBetweenBoth == 0)
            {
                go.transform.localScale = new Vector3(Current.localScale.x, Current.transform.localScale.y, Current.localScale.z);
                go.transform.position = new Vector3(Base.position.x + CurrentBloc._maxDistance, Current.position.y + LastBloc._height, Current.position.z);
            }
            else
            {
                go.transform.localScale = new Vector3(Last.localScale.x, Last.localScale.y, Last.localScale.z - distanceBetweenBoth);
                go.transform.position = new Vector3(Base.position.x + CurrentBloc._maxDistance, Current.position.y + LastBloc._height, Current.transform.position.z);
            }

            Last = Current;
            LastBloc = CurrentBloc;
            Current = go.transform;
            CurrentBloc = Current.GetComponent<Bloc>();
            CurrentBloc.Base = Base;
            CurrentBloc._movingInZ = !LastBloc._movingInZ;
        }
    }


    private void ScaleAndReplace(float distanceBetweenBoth)
    {
        if (CurrentBloc._movingInZ)
        {
            if (distanceBetweenBoth == 0)
            {
                Current.position = new Vector3(Last.position.x, Current.position.y, Last.position.z);
            }
            else
            {
                Current.localScale = new Vector3(Last.localScale.x, Last.localScale.y, Last.localScale.z - distanceBetweenBoth);
                Current.position = new Vector3(Last.position.x, Current.position.y, (Current.position.z + Last.position.z) / 2);

                GameObject Falling = Instantiate(Current.gameObject);
                Falling.GetComponent<Bloc>().isFalling = true;
                Falling.transform.localScale = new Vector3(Last.localScale.x, Last.transform.localScale.y, distanceBetweenBoth);


                if (Last.position.z - Current.position.z > 0)
                {
                    Falling.transform.position = new Vector3(Last.position.x, Current.position.y, ((Last.position.z) - (Falling.transform.localScale.z + (Current.localScale.z / 2))));
                }
                else
                {
                    Falling.transform.position = new Vector3(Last.position.x, Current.position.y, ((Last.position.z) + (Falling.transform.localScale.z + (Current.localScale.z / 2))));
                }
                Falling.AddComponent<Rigidbody>();
            }
        }
        else
        {
            if (distanceBetweenBoth == 0)
            {
                Current.position = new Vector3(Last.position.x, Current.position.y, Last.position.z);
            }
            else
            {
                Current.localScale = new Vector3(Last.localScale.x - distanceBetweenBoth, Last.localScale.y, Last.localScale.z);
                Current.position = new Vector3((Current.position.x + Last.position.x) / 2, Current.position.y, Last.position.z);

                GameObject Falling = Instantiate(Current.gameObject);
                Falling.GetComponent<Bloc>().isFalling = true;
                Falling.transform.localScale = new Vector3(distanceBetweenBoth, Last.transform.localScale.y, Last.localScale.z);


                if (Last.position.x - Current.position.x > 0)
                    Falling.transform.transform.position = new Vector3(((Last.position.x) - (Falling.transform.localScale.x + (Current.localScale.x / 2))), Current.position.y, Last.position.z);
                else
                    Falling.transform.transform.position = new Vector3(((Last.position.x) + (Falling.transform.localScale.x + (Current.localScale.x / 2))), Current.position.y, Last.position.z);
                Falling.AddComponent<Rigidbody>();
            }
        }
    }

    private bool CheckingGameOver(float distanceBetweenBoth)
    {
        if (CurrentBloc._movingInZ)
        {
            if (distanceBetweenBoth >= Last.localScale.z)
                return true;
        }
        else
        {
            if (distanceBetweenBoth >= Last.localScale.x)
                return true;
        }

        return false;
    }


    private float CheckingDistance()
    {
        float distance;

        if (CurrentBloc._movingInZ)
        {
            distance = Mathf.Abs(Last.position.z - Current.position.z);
            if (distance <= arrondi)
            {
                return 0;
            }
            else
            {
                return distance;
            }
        }
        else
        {
            distance = Mathf.Abs(Last.position.x - Current.position.x);
            if (distance <= arrondi)
            {
                return 0;
            }
            else
            {
                return distance;
            }
        }
    }



}
