using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractionOptionViewer : MonoBehaviour
{
    public GameObject Interaction_Pick;
    public GameObject Interaction_Open;

    public int index = 0;
    public int beforeindex = 0;
    public string option = "";

    public void UpdateInteractionOption(bool pick, bool open) {
        //옵션 표시
        Interaction_Pick.SetActive(pick);
        Interaction_Open.SetActive(open);
    }

    public void HighlightIndex() {
        if(index < 0) index = transform.childCount - 1;
        if (index >= transform.childCount) index = 0;
        //색상 초기화
        for(int i=0;i <transform.childCount;i++) { transform.GetChild(i).GetComponent<TMP_Text>().color = Color.white; }
        //선택 옵션 강조
        transform.GetChild(index).GetComponent<TMP_Text>().color = Color.yellow;
        option = transform.GetChild(index).GetComponent<TMP_Text>().text;

        # region 이 부분은 사소한 버그를 잡기 위한 스파게티 부분
        if (!transform.GetChild(index).gameObject.activeSelf) {
            index++;
            HighlightIndex();
            return;
        }
        #endregion
    }
}
