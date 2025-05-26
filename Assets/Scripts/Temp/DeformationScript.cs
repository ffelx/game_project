using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Graphic))]
public class DeformationScript : BaseMeshEffect
{
    [Range(0f, 1f)]
    public float taperAmount = 0.5f;

    public override void ModifyMesh(VertexHelper vh)
    {
        if (!IsActive()) return;

        Rect rect = graphic.rectTransform.rect;
        float height = rect.height;
        float yMin = rect.yMin;

        UIVertex vert = new UIVertex();

        for (int i = 0; i < vh.currentVertCount; i++)
        {
            vh.PopulateUIVertex(ref vert, i);

            float normalizedY = (vert.position.y - yMin) / height;
            float taper = 1.0f - (normalizedY * taperAmount);
            vert.position.x *= taper;

            vh.SetUIVertex(vert, i);
        }
    }
}
