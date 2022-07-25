using System.Linq;
using UnityEngine;

namespace ddg {
    public class CurvatureFlow : MonoBehaviour {
        [SerializeField, Range(0.001f, 0.1f)] protected float delta = 0.001f;
        [SerializeField] protected MeanCurvatureFlow.Type type;
        HalfEdgeGeom geom;
        MeanCurvatureFlow flow;
        MeshFilter filt;
        Mesh mesh;

        void Start() {
            filt = GetComponentInChildren<MeshFilter>();
            var rend = GetComponentInChildren<MeshRenderer>();
            mesh = MeshUtils.Weld(filt.sharedMesh);
            filt.sharedMesh = mesh;
            geom = new HalfEdgeGeom(mesh);
            flow = new MeanCurvatureFlow(geom, type);
        }

        void Update() {
            if(Input.GetKeyDown(KeyCode.Space)){
                flow.Integrate(delta);
                mesh.SetVertices(geom.mesh.verts.Select(v => v.pos).ToArray());
                mesh.RecalculateNormals();
            }
        }
    }
}


