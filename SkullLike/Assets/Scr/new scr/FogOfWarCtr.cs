using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWarCtr : MonoBehaviour
{

    public GameObject m_fogOfWarPlane;
    public Transform m_player;
    public LayerMask m_fogLayer;
    public float m_radious = 5f;
    private float m_radiousSqr { get { return m_radious * m_radious; } }

    private Mesh m_mesh;
    private Vector3[] m_vertices;
    private Color[] m_colors;
    [Multiline(3000)]
    public string mesh_info;

    // Use this for initialization
    void Start()
    {
        Initialize();
    }
    private void Update()
    {
        Ray r = new Ray(transform.position, m_player.position - transform.position);
        RaycastHit hit = Physics.Raycast(transform.position, transform.forward)
        if (Physics.Raycast(r, out hit, 1000, m_fogLayer, QueryTriggerInteraction.Collide))
        {
            for (int i = 0; i < m_vertices.Length; i++)
            {
                Vector3 v = m_fogOfWarPlane.transform.TransformPoint(m_vertices[i]);
                float dist = Vector3.SqrMagnitude(v - hit.point);
                if (dist < m_radiousSqr)
                {
                    float alpha = Mathf.Min(m_colors[i].a, dist / m_radiousSqr);
                    m_colors[i].a = alpha;
                }
            }
            UpdateColor();
        }
    }

    void Initialize()
    {
        m_mesh = m_fogOfWarPlane.GetComponent<MeshFilter>().mesh;
        m_vertices = m_mesh.vertices;
        m_colors = new Color[m_vertices.Length];
        for (int i = 0; i < m_colors.Length; i++)
        {
            m_colors[i] = Color.black;
        }
        UpdateColor();
    }

    void UpdateColor()
    {
        m_mesh.colors = m_colors;
        mesh_info = meshInfoUpdate();
    }
    string meshInfoUpdate()
    {
        string rtn = "";
        foreach(var i in m_mesh.colors)
        {
            rtn += i.ToString() + "\n";
        }
        return rtn;
    }
}