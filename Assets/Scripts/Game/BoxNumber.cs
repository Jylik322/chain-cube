using UnityEngine;

public class BoxNumber : MonoBehaviour
{
    TextMesh[] textMeshs;
    MeshRenderer mesh;
    [SerializeField] public long instantiateNumber;
    [SerializeField] private long number;
    public long Number
    {
        get => number;
        set
        {
            number = value;
            var str = number.ToString();
            foreach (TextMesh textMesh in textMeshs)
            {
                textMesh.text = str;
            }
            mesh.material = Resources.Load("Materials/Box" + (int)number) as Material;
        }
    }
    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        textMeshs = GetComponentsInChildren<TextMesh>();
    }
}
