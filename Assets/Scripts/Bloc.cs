using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloc : MonoBehaviour
{

    public Transform Base;

    public bool isFirst = false;
    [HideInInspector] public bool isFalling = false;

    //Variables about movements
    public bool _goingRight;
    public bool _movingInZ;
    public bool _moving;
    public float _speed = 1.0f;
    public float _maxDistance = 0.5f;
    public float _height;

    private ColorManager _colorManager;


    private void OnEnable()
    {
        if (isFirst)
        {
            transform.position = new Vector3(Base.position.x, Base.position.y + Base.GetComponent<Bloc>()._height, Base.position.z + _maxDistance);
        }
    }

    private void Awake()
    {
        _height = transform.localScale.y;
    }

    void Start()
    {       
        //Always going from left to right when it spawns
        _goingRight = true;

        _colorManager = FindObjectOfType<ColorManager>();
        if (!isFalling)
            _colorManager.ChangeColor(GetComponent<Renderer>().material);
        else
            _colorManager.KeepColor(GetComponent<Renderer>().material);
    }


    void Update()
    {
        if (_moving)
        {
            Move();
        }
    }


    private void Move()
    {
        //Moving in z axis
        if (_movingInZ)
        {
            if (_goingRight)
            {
                transform.Translate(-Vector3.forward * Time.deltaTime * _speed);
                if (transform.position.z <= Base.position.z - _maxDistance)
                {
                    _goingRight = false;
                }

            }
            else if (!_goingRight)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * _speed);
                if (transform.position.z >= Base.position.z + _maxDistance)
                {
                    _goingRight = true;
                }
            }
        }

        //Moving in x axis
        else
        {
            if (_goingRight)
            {
                transform.Translate(-Vector3.right * Time.deltaTime * _speed);
                if (transform.position.x <= Base.position.x - _maxDistance)
                {
                    _goingRight = false;
                }

            }
            else if (!_goingRight)
            {
                transform.Translate(Vector3.right * Time.deltaTime * _speed);
                if (transform.position.x >= Base.position.x + _maxDistance)
                {
                    _goingRight = true;
                }
            }
        }
    }

   
}
