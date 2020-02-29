using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MainMenuEnviroment : MonoBehaviour
{
    public List<GameObject> _cars;
    public Transform _startPoint;
    public Transform _endPoint;
    public float _carSpeed = 5;

    
    private Transform _currentCar;
    private bool _isCar;

    private void Awake()
    {
        _isCar = false;
    }

    private void Update()
    {
        SpawnCar();
        MoveTo();
        DestroyCar();
    }


    private void SpawnCar()
    {
        if (_cars.Count == 0 || _isCar == true) return;
        _isCar = true;
        var car = Instantiate(_cars[Random.Range(0, _cars.Count)]);
        _currentCar = car.transform;
        _currentCar.transform.position = _startPoint.position;
        _currentCar.transform.LookAt(_endPoint.position);
    }

    private void MoveTo()
    {
        if (_currentCar == null) return;
        float step = _carSpeed * Time.deltaTime;
        _currentCar.position = Vector3.MoveTowards(_currentCar.position, _endPoint.position, step);
        
    }

    private void DestroyCar()
    {
        if (_currentCar == null) return;
        if (Vector3.Distance(_currentCar.position, _endPoint.position) < 1f)
        {    
            Destroy(_currentCar.gameObject);
            _isCar = false;
        }
    }
}
