using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FigureNameWobble : MonoBehaviour
{
    TextMeshProUGUI textMesh;
    Mesh mesh;
    Vector3[] vertices;

    private void OnEnable()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        StartCoroutine(WobbleText());
    }

    private IEnumerator WobbleText()
    {
        while(!Managers.Game.isGameOver)
        {
            textMesh.ForceMeshUpdate();
            mesh = textMesh.mesh;
            vertices = mesh.vertices;

            for (int i = 0; i < textMesh.textInfo.characterCount; i++)
            {
                TMP_CharacterInfo c = textMesh.textInfo.characterInfo[i];

                int index = c.vertexIndex;

                Vector3 offset = Wobble(Time.time + i);
                vertices[index] += offset;
                vertices[index + 1] += offset;
                vertices[index + 2] += offset;
                vertices[index + 3] += offset;
            }

            mesh.vertices = vertices;
            textMesh.canvasRenderer.SetMesh(mesh);

            yield return null;
        }
    }

    Vector2 Wobble(float time)
    {
        return new Vector2(Mathf.Sin(time * 3.3f), Mathf.Cos(time * 2.5f));
    }
}
