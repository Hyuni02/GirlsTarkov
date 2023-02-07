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
        //�ɼ� ǥ��
        Interaction_Pick.SetActive(pick);
        Interaction_Open.SetActive(open);
    }

    public void HighlightIndex() {
        if(index < 0) index = transform.childCount - 1;
        if (index >= transform.childCount) index = 0;
        //���� �ʱ�ȭ
        for(int i=0;i <transform.childCount;i++) { transform.GetChild(i).GetComponent<TMP_Text>().color = Color.white; }
        //���� �ɼ� ����
        transform.GetChild(index).GetComponent<TMP_Text>().color = Color.yellow;
        option = transform.GetChild(index).GetComponent<TMP_Text>().text;

        # region �� �κ��� ����� ���׸� ��� ���� ���İ�Ƽ �κ�
        if (!transform.GetChild(index).gameObject.activeSelf) {
            index++;
            HighlightIndex();
            return;
        }
        #endregion
    }
}
