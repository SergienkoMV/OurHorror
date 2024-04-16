using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Lamppost : MonoBehaviour
{
    [Tooltip("������ �� ������� ������ ��� ����")]
    [SerializeField] ParticleSystem _sparks;
    [Tooltip("������ �� ����")]
    [SerializeField] GameObject _spotLight;
    [Tooltip("������ �� ��������")]
    [SerializeField] GameObject _spotLight2;

    [Tooltip("������������ ����� �� �������� ������")]
    [SerializeField] float _maxActiveTime; //������������ ����� ������ �������� ������

    private bool _active = false; //�������� ���������� ������

    // Start is called before the first frame update
    void Start()
    {
        SetFlickeringLamppost(false);
    }

    //�������� �����
    //��������� �������� ������
    public void ActivateObject()
    {
        //_active = true;
        SparksSpread(); //������ ����
        //FlickeringLamppost(); 
        StartCoroutine(FlickeringLamppostOn());
        //SetFlickeringLamppost(_active); //�������� ������
        //StartCoroutine(FlickeringLamppostOff()); // ��������� �������� ������
    }

    //���������� ������
    //public void DeactivateObject()
    //{
    //    _active = false;
    //}

    //������ ���� ������
    private void SparksSpread()
    {
        _sparks.Play();
    }

    //�������� ������
    IEnumerator FlickeringLamppostOn()
    {
        yield return new WaitForSeconds(Random.Range(0, _maxActiveTime));
        SetFlickeringLamppost(true);
        StartCoroutine(FlickeringLamppostOff());
        if (_active)
        {
            
        }
    }

    IEnumerator FlickeringLamppostOff()
    {
        yield return new WaitForSeconds(Random.Range(0, _maxActiveTime));
        SetFlickeringLamppost(false); //�������� ������
        
        _active = true;
        if (_active)
        {

        }
    }



    //���������/��������� �������
    private void SetFlickeringLamppost(bool set)
    {
        _spotLight.gameObject.SetActive(set);
        _spotLight2.gameObject.SetActive(set);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<LeshiyController>())
        {
            ActivateObject();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<LeshiyController>() && _active) 
        {
            StartCoroutine(FlickeringLamppostOn());
            _active = false;
        } 
    }
}
