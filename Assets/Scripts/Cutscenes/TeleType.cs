using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleType : MonoBehaviour
{
    public TMPro.TMP_Text _mTextMeshPro;

    public int currentPiece;

    public string[] switchPieces;

    // [HideInInspector]
    public int[] switchPos;

    public GameObject dialogueManager;

    public GameObject stateManager;

    public void Initiate()
    {
        // _mTextMeshPro.ForceMeshUpdate();
        switchPieces = _mTextMeshPro.text.Split("<switch>");
        _mTextMeshPro.text = string.Join("", switchPieces);
        switchPos = new int[switchPieces.Length];
        switchPos[0] = switchPieces[0].Length - "<line-indent=1em>".Length;
        for (int i = 1; i < switchPieces.Length; i++)
        {
            switchPos[i] = string.Join("", switchPieces[i].Split("<page>")).Length;
        }
        // currentPiece = 0;
        _mTextMeshPro.maxVisibleCharacters = 0;
    }

    public void Speak(bool firstLine)
    {
        if (!firstLine && _mTextMeshPro.maxVisibleCharacters != 0)
        {
            _mTextMeshPro.maxVisibleCharacters = 0;
            _mTextMeshPro.pageToDisplay++;
        }
        StartCoroutine(TypeWriter(currentPiece));
    }

    public IEnumerator TypeWriter(int pieceNum)
    {
        int counter = 0;

        while (true)
        {
            int currentPageLength = _mTextMeshPro.textInfo.pageInfo[_mTextMeshPro.pageToDisplay - 1].lastCharacterIndex - _mTextMeshPro.textInfo.pageInfo[_mTextMeshPro.pageToDisplay - 1].firstCharacterIndex + 1;

            int visibleCount = counter % (currentPageLength + 1);

            _mTextMeshPro.maxVisibleCharacters = _mTextMeshPro.textInfo.pageInfo[_mTextMeshPro.pageToDisplay - 1].firstCharacterIndex + visibleCount;

            if (visibleCount >= currentPageLength)
            {
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
                if (_mTextMeshPro.pageToDisplay < _mTextMeshPro.textInfo.pageCount && visibleCount < switchPos[pieceNum - 1])
                {
                    counter = -1;
                    _mTextMeshPro.pageToDisplay++;
                }
                else
                {
                    if (pieceNum < switchPos.Length)
                    {
                        dialogueManager.GetComponent<SpeakerSelect>().switchSpeaker();
                        break;
                    }
                    else
                    {
                        stateManager.GetComponent<SceneManage>().ChangeScene("NextLevel");
                    }
                }
            }

            counter++;
            yield return new WaitForSeconds(0.025f);
        }
    }
}