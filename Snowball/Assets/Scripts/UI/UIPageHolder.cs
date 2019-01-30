using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPageHolder : MonoBehaviour {

    public List<UIPage> initializedUIPages;
    public UIPage startPage = null;

    public void AddPage(UIPage page)
    {
        initializedUIPages.Add(page);
        if (startPage == null) {
            startPage = initializedUIPages[0];
        }
    }

    public void RemovePage(UIPage page)
    {
        initializedUIPages.Remove(page);
    }
}
