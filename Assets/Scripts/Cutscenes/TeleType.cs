using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleType : MonoBehaviour
{
    public TMPro.TMP_Text _mTextMeshPro;

    public int currentPiece;

    public string[] switchPieces;

    [HideInInspector]
    public int[] switchPos;

    public GameObject dialogueManager;

    public GameObject namePlate;

    private float teleSpeed = 0.02f;

    void Update()
    {
        if(Input.GetKey(KeyCode.Space)) teleSpeed = 0.005f;
        else teleSpeed = 0.02f;
    }

    public void Initiate()
    {
        switchPieces = _mTextMeshPro.text.Split("<switch>");
        _mTextMeshPro.text = string.Join("", switchPieces);
        switchPos = new int[switchPieces.Length];

        for (int i = 0; i < switchPieces.Length; i++)
        {
            switchPos[i] = switchPieces[i].Replace("<line-indent=1em>", "").Replace("<page>", "").Length;
        }

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
        int switchFinder = 0;

        while (true)
        {
            int currentPageLength = _mTextMeshPro.textInfo.pageInfo[_mTextMeshPro.pageToDisplay - 1].lastCharacterIndex - _mTextMeshPro.textInfo.pageInfo[_mTextMeshPro.pageToDisplay - 1].firstCharacterIndex + 1;

            int visibleCount = counter % (currentPageLength + 1);

            _mTextMeshPro.maxVisibleCharacters = _mTextMeshPro.textInfo.pageInfo[_mTextMeshPro.pageToDisplay - 1].firstCharacterIndex + visibleCount;


            if (visibleCount >= currentPageLength)
            {
                switchFinder += visibleCount;
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

                if (dialogueManager.GetComponent<Turns>())
                {
                    dialogueManager.GetComponent<Turns>().currentTurn++;
                    dialogueManager.GetComponent<Turns>().Check();
                }
                if (_mTextMeshPro.pageToDisplay < _mTextMeshPro.textInfo.pageCount && switchFinder < switchPos[pieceNum - 1])
                {
                    counter = -1;
                    _mTextMeshPro.pageToDisplay++;
                }
                else
                {
                    if (pieceNum < switchPos.Length)
                    {
                        switchFinder = 0;
                        dialogueManager.GetComponent<SpeakerSelect>().SwitchSpeaker();
                    }
                    else
                    {
                        dialogueManager.GetComponent<Animator>().SetTrigger("End");
                    }
                    break;
                }
            }

            counter++;
            yield return new WaitForSeconds(teleSpeed);
        }
    }
}