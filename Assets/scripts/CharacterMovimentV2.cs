using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovimentV2 : MonoBehaviour
{
    public float movimentSpeed;
    public float maxSpeed;
    public float minSpeed;
    public float gravedad;
    public float inicioCaida;


    public float FactorRotacionpitch;
    public float FactorRotacionYao;
    public float maxpitch;
    public Transform head;
    private float pitch;


    //test

    public float distancia;
    public float distanciaV;
    public float altura;

    Vector3 MoveVector=Vector3.zero;

    public void moverse(Vector3 dis)
    {
        
        //transform.position += dis * movimentSpeed;
        RaycastHit hit;
        RaycastHit hitV;
        Debug.DrawRay(transform.position, transform.TransformDirection(dis) * distancia, Color.red);

        //Indica si estas chocando con algo
        if (Physics.Raycast(transform.position + (Vector3.up / 2), transform.TransformDirection(dis), out hit, distancia))
        {
            print("tocaste Algo");
            MoveVector.x = 0;
            MoveVector.z = 0;
        }
        else
        {
            MoveVector.x = dis.x*movimentSpeed;
            MoveVector.z = dis.z*movimentSpeed;
            Debug.DrawRay(transform.position, Vector3.zero, Color.black);
            //Debug.DrawRay(transform.position, transform.TransformDirection(dis) * distancia, Color.red);
        }

        //gravedad mal aplicada
        if (Physics.Raycast(transform.position, -Vector3.up, out hitV, distanciaV))
        {
            MoveVector.y = 0;

            transform.position = new Vector3(hitV.point.x, hitV.point.y + altura, hitV.point.z);
        }
        else
        {
            if (MoveVector.y == 0)
            {
                MoveVector.y = inicioCaida;
            }
            else
            {
                MoveVector.y = -Mathf.Abs(MoveVector.y* gravedad);
            }
        }
        transform.Translate(MoveVector*Time.deltaTime);
      


        //Giro control
        // control.Rotate(new Vector3(dis.x,0, 0),0.5f);
    }
    public void Rotar(Vector3 RotateVector)
    {
        transform.Rotate(Vector3.up * RotateVector.x * FactorRotacionYao);
        pitch += FactorRotacionpitch * RotateVector.y;


        pitch = Mathf.Clamp(pitch, -maxpitch, maxpitch);
        var eulerAngles = head.localEulerAngles;
        eulerAngles.x = pitch;
        eulerAngles.z = 0f;
        head.localEulerAngles = eulerAngles;
    }


    void Update()
    {
        //funcion de cambio de velocidad, capaz deberia hacerlo en el otro lado   
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movimentSpeed = Mathf.Lerp(movimentSpeed, maxSpeed, 0.005f);
        }
        else
        {
            movimentSpeed = Mathf.Lerp(movimentSpeed, minSpeed, 0.005f);
        }
    }
}