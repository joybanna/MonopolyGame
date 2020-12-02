using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOrderTag : MonoBehaviour
{
    public Sprite myturn, loading, win;
    public Image bg_img;
    public Image status_img;
    public Text name_text;
    public Text order_text;

    public void SettingStart(string _name, string _order)
    {
        name_text.text = _name;
        order_text.text = _order;
    }
    public void SettingStatus(string _status)
    {
        status_img.enabled = false;
        switch (_status)
        {
            case "myturn":
                status_img.enabled = true;
                status_img.sprite = myturn;
                break;
            case "loading":
                status_img.enabled = true;
                status_img.sprite = loading;
                break;
            case "win":
                status_img.enabled = true;
                status_img.sprite = win;
                break;
            default:
                status_img.enabled = false;
                break;
        }
    }
    public void SettingColor(bool _isMine)
    {
        if (_isMine)
        {
            Color temp = new Color(0, 172f / 255f, 1, 1);
            bg_img.color = temp;
        }
        else
        {
            Color temp = new Color(168f / 255f, 168f / 255f, 168f / 255f, 123f / 255f);
            bg_img.color = temp;
        }
    }
}
