using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractiveShadow : MonoBehaviour
{
    [SerializeField] private Transform shadowTransform; //Añadir un transform que este en el 0,0,0 (shadow) que será despues el objeto sombra con colision

    [SerializeField] private Transform lightTransform;
    private LightType lightType;

    [SerializeField] private LayerMask targetLayerMask;  //Hay que añadir la Layer que colisionara para representar las sombras y la mesh encima

    [SerializeField] private Vector3 extrusionDirection = Vector3.zero;

    private Vector3[] objectVertices;

    private Mesh shadowColliderMesh;
    private MeshCollider shadowCollider;

    //Variables para determinar posiciones (optimización)
    private Vector3 previousPosition;
    private Quaternion previousRotation;
    private Vector3 previousScale;
    private bool updateColliderShadow = true;
    [SerializeField][Range(0.02f, 1f)] private float shadowColliderUpdateTime = 0.08f;

    private void Awake()
    {

        //Inicializar colisión de sombras
        InitializeShadowCollider();

        lightType = lightTransform.GetComponent<Light>().type;

        //Vertices del objeto para crear mesh de triger
        objectVertices = transform.GetComponent<MeshFilter>().mesh.vertices.Distinct().ToArray(); //--> Distinc nos permite eliminar duplicados de una matriz (necesario System.Linq) AHORRO MUCHA MEMORIA

        shadowColliderMesh = new Mesh();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shadowTransform.position = transform.position; //Actualizar la posición de la sombra
    }

    private void FixedUpdate()
    {
        if (TransformChanged() && updateColliderShadow) //Solo se actualiza la mesh si se ha cambiado el transform y además se puede actualizar (porque ha acabado de actualizarse la anterior de eso el bool)
        {
            Invoke("UpdateShadowCollider", shadowColliderUpdateTime); //LLamamos a esta función alcabo de un tiempo
            updateColliderShadow = false;
        }

        previousPosition = transform.position;
        previousRotation = transform.rotation;
        previousScale = transform.localScale;
        
    }

    private void UpdateShadowCollider()
    {
        shadowColliderMesh.vertices = ShadowColliderMeshVertices(); //Recalculamos los vertices de la shadow mesh
        shadowCollider.sharedMesh = shadowColliderMesh; //Actualizanos al nueva shadowColliderMesh a la mesh de la shadow
        updateColliderShadow = true;
    }

    private bool TransformChanged() //Funcion para determinar si ha cambiado el transform del gameobject que tiene este script
    {
        return previousPosition != transform.position || previousRotation != transform.rotation || previousScale != transform.localScale;
    }

    private void InitializeShadowCollider()
    {
        GameObject shadowGameObject = shadowTransform.gameObject;
        //shadowGameObject.hideFlags = HideFlags.HideInHierarchy;           //Esconde el objeto en la hierarchy
        shadowCollider = shadowGameObject.AddComponent<MeshCollider>();     //La sombra va a tener una mesh
        shadowCollider.convex = true;                                       //Tiene forma determinada a partir de los vertices
        shadowCollider.isTrigger = true;                                    //Necesario para que sea trigger y no colisione fisicamente
    }

    private Vector3[] ShadowColliderMeshVertices() //Calculo de los vertices para el collider mesh de la sombra
    {
        Vector3[] points = new Vector3[2 * objectVertices.Length];

        Vector3 raycastDirection = lightTransform.forward;

        int n = objectVertices.Length;

        for (int i = 0; i < n; i++)
        {
            Vector3 point = transform.TransformPoint(objectVertices[i]);

            if (lightType != LightType.Directional)  //¡Ahora solo esta configurado para UNA SOLA LUZ, se deben añadir las demás si hay más luces!
            {
                raycastDirection = point - lightTransform.position;
            }


            //Punto de intersección de luz a objeto
            points[i] = IntersectionPoint(point, raycastDirection);

            //Saber si hay un Punto de extrusión de la sombra
            points[n + i] = ExtrusionPoint(point, points[i]);
        }

        return points;
    }

    private Vector3 IntersectionPoint(Vector3 fromPosition, Vector3 direction)
    {
        RaycastHit hit;

        if (Physics.Raycast(fromPosition, direction, out hit, Mathf.Infinity, targetLayerMask))
        {
            return hit.point - transform.position;
        }

        return fromPosition + 100 * direction - transform.position; //Añadimos 100 como distancia muy lejana  para ver si hay alguna interseccion
    }

    private Vector3 ExtrusionPoint(Vector3 objectVertexPosition, Vector3 shadowPointPosition)
    {
        if (extrusionDirection.sqrMagnitude == 0) //Nos permite ahorrar memoria con sqrMagnitude
        {
            return objectVertexPosition - transform.position;
        }

        return shadowPointPosition + extrusionDirection;
    }
}
