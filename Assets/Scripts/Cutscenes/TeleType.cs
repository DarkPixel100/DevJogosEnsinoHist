using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleType : MonoBehaviour
{
    public TMPro.TMP_Text m_textMeshPro;

    private bool next;

    void Start()
    {
        m_textMeshPro.ForceMeshUpdate();
        StartCoroutine(TypeWriter());
    }

    IEnumerator TypeWriter()
    {

        int totalVisibleCharacters = m_textMeshPro.textInfo.characterCount;
        int counter = 0;

        while (true)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);

            m_textMeshPro.maxVisibleCharacters = visibleCount;

            if (visibleCount >= totalVisibleCharacters)
            {
                Debug.Log(m_textMeshPro.textInfo.pageCount);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
                if (m_textMeshPro.pageToDisplay < m_textMeshPro.textInfo.pageCount)
                {
                    m_textMeshPro.pageToDisplay += 1;
                    counter = 0;
                }
                else
                    break;
            }

            counter += 1;

            yield return new WaitForSeconds(0.05f);
        }
        yield return next = true;
    }
}
// selecao de qual texto por id