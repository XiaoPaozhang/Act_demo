using UnityEngine;

public class Test : MonoBehaviour
{
  public RenderTexture renderTexture;
  public new Camera camera;

  void Start()
  {
    int textureSize = 256;
    renderTexture = new RenderTexture(textureSize, textureSize, 24)
    {
      filterMode = FilterMode.Bilinear,
      wrapMode = TextureWrapMode.Clamp
    };

    camera.targetTexture = renderTexture;

    CreateDisplayQuad();
  }

  void CreateDisplayQuad()
  {
    GameObject quadGameObject = new GameObject("DisplayQuad");
    quadGameObject.transform.parent = transform;
    quadGameObject.transform.localPosition = Vector3.zero;
    quadGameObject.transform.localRotation = Quaternion.identity;
    quadGameObject.transform.localScale = new Vector3(1f, 1f, 1f);

    MeshRenderer meshRenderer = quadGameObject.AddComponent<MeshRenderer>();
    MeshFilter meshFilter = quadGameObject.AddComponent<MeshFilter>();
    MeshCollider meshCollider = quadGameObject.AddComponent<MeshCollider>();

    Mesh mesh = new Mesh
    {
      name = "DisplayQuadMesh"
    };

    Vector3[] vertices = new Vector3[]
    {
            new Vector3(-1f, -1f, 0f),
            new Vector3(1f, -1f, 0f),
            new Vector3(-1f, 1f, 0f),
            new Vector3(1f, 1f, 0f)
    };

    int[] triangles = new int[]
    {
            0, 1, 2,
            2, 1, 3
    };

    Vector2[] uvs = new Vector2[]
    {
            new Vector2(0f, 0f),
            new Vector2(1f, 0f),
            new Vector2(0f, 1f),
            new Vector2(1f, 1f)
    };

    mesh.vertices = vertices;
    mesh.triangles = triangles;
    mesh.uv = uvs;
    mesh.RecalculateNormals();

    meshFilter.mesh = mesh;
    meshCollider.sharedMesh = mesh;

    Material material = new Material(Shader.Find("Unlit/Texture"))
    {
      mainTexture = renderTexture
    };

    meshRenderer.material = material;
  }

  void Update()
  {
    if (camera != null)
    {
      camera.targetTexture = renderTexture;
    }
  }
}