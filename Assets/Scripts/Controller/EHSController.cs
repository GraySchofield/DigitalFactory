using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DGFactory
{
    public class EHSController : MonoBehaviour
    {
        private EHSData _currentData;

        private GameObject _leftButton;
        private GameObject _rightButton;
        private GameObject _emergencyButton;
        private GameObject _lightSensor;
        private List<GameObject> _doors;
 
        // Start is called before the first frame update
        void Start()
        {
            _leftButton = transform.Find("LeftButton").gameObject;
            _rightButton = transform.Find("RightButton").gameObject;
            _emergencyButton = transform.Find("EmergencyButon").gameObject;
            _lightSensor = transform.Find("LightSensor").gameObject;
            _doors = new List<GameObject>();
            var i = 1;
            while(transform.Find("Door" + i) != null)
            {
                _doors.Add(transform.Find("Door" + i).gameObject);
                i++;
            }

        }

        public void Refresh(EHSData data)
        {
            _currentData = data;
        }

        // Update is called once per frame
        void Update()
        {
            if(_currentData != null)
            {
                setSensorState(_leftButton, _currentData.ButtonLeft);
                setSensorState(_rightButton, _currentData.ButtonRight);
                setSensorState(_emergencyButton, _currentData.ButtonEmergency);
                setSensorState(_lightSensor, _currentData.LightData);

                //开关门检测
                for(int i = 0; i < _doors.Count; i++)
                {
                    if (i < _currentData.DoorData.Count) {
                        _doors[i].SetActive(true);
                        setDoorState(_doors[i], _currentData.DoorData[i]);
                    }
                    else
                    {
                        _doors[i].SetActive(false);
                    }
                }
            }
        }
        
        private void setDoorState(GameObject target, bool isClosed)
        {
            GameObject red = target.transform.Find("IndicatorRedCube").gameObject;
            GameObject blue = target.transform.Find("IndicatorBlueCube").gameObject;

            if (isClosed)
            {
                red.SetActive(false);
                blue.SetActive(true);
            }
            else
            {
                red.SetActive(true);
                blue.SetActive(false);
            }
        }

        private void setSensorState(GameObject target, bool isNormal)
        {
            GameObject red = target.transform.Find("IndicatorRed").gameObject;
            GameObject green = target.transform.Find("IndicatorGreen").gameObject;

            if (isNormal)
            {
                red.SetActive(false);
                green.SetActive(true);
            }
            else
            {
                red.SetActive(true);
                green.SetActive(false);
            }
        }


    }
}
