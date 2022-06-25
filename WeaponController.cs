using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Weapons")]
    [Header("Tiro (A Distancia)")]
    public Transform fireSpotPosition;
    public GameObject bulletPrefab;
    public float forceIntensity = 30f;
    public float maxDistance = 120f;
    private bool weaponPressed = false;

    [Header("Melee")]
    public float distanceMelee = 1.8f;
    public bool meleeAtk = false;
    void Start()
    {
        
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            weaponPressed = true;
        }
    }
    private void FixedUpdate() {
        if(weaponPressed)
        {
            Shoot();
            weaponPressed = false;
        }
    }
    void Shoot()
    {
        //1º de onde vai sair o raio // 2º Direção // 3º variavel que recebe info do que foi atingido // 4º distancia
        //out pode ser alterado de dentro da função, então estamos passando uma referencia
        //raycast é true ou false
        RaycastHit hitInfo;
        if(Physics.Raycast(fireSpotPosition.position, fireSpotPosition.forward, out hitInfo, maxDistance))
        {
            Rigidbody rbOther = hitInfo.collider.GetComponent<Rigidbody>();
            if(rbOther == null)
            {
                //instanciar o bulletprefab
                //1º Qual objeto? 2º Onde? Na posição que foi atingido 3º rotação em relação ao padrão do mundo
                //sem rotação = rotação padrão

                //queremos que a bullet esteja perpendicular ao plano atingido = normal
                // - pra ser em direção ao jogador
                Vector3 holePosition = hitInfo.point + hitInfo.normal * 0.01f;
                GameObject bulletHole = Instantiate(bulletPrefab,holePosition, Quaternion.identity);
                bulletHole.transform.forward = -hitInfo.normal;
            }
            else{
                //aplicar força
                //1º tamanho da força // direção da força //ponto do mundo onde o raycast atingiu
                //AddForceAtPosition na posição em que o raycast atingiu, aplica força em uma determinada posição
                rbOther.AddForceAtPosition(forceIntensity * fireSpotPosition.forward, hitInfo.point, ForceMode.Impulse);
            }
        }
    }
    void meleeAtack()
    {
        RaycastHit hitInfo2;
        //Debug.DrawLine(fireSpot.forward, Vector3.forward * 1000f, Color.yellow);
        if (Physics.Raycast(fireSpotPosition.position, fireSpotPosition.forward, out hitInfo2))
        {
            Rigidbody rbOther2 = hitInfo2.collider.GetComponent<Rigidbody>();
            Transform transformOther2 = hitInfo2.collider.GetComponent<Transform>();
            if (rbOther2 != null)
            {
                //print("melee combat");
                if(Vector3.Distance(transform.position, transformOther2.position) <= distanceMelee)
                {
                    rbOther2.AddForceAtPosition(forceIntensity * fireSpotPosition.forward, hitInfo2.point, ForceMode.Impulse);
                    //print("hello world");
                }
            }
        }
    }
}
