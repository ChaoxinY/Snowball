using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FocusDropDown : Dropdown, IFocusUIElement
    {
        private bool focused;

        protected override GameObject CreateDropdownList(GameObject template)
        {
            focused = true;
            GameObject dropDownList = base.CreateDropdownList(template);
            StartCoroutine(CheckIfItemsAreStillSelected());
            return dropDownList;
        }

        private IEnumerator CheckIfItemsAreStillSelected()
        {
            //Prevent item search when the items arent created.
            yield return new WaitUntil(() => transform.Find("Dropdown List") != null);
            //New list is created on each new drop down list.
            List<GameObject> itemsToTrack = new List<GameObject>();
            for (int i = 0; i < options.Count; i++)
            {
                //Add gameobjects(items) that needs to be tracked
                Transform itemTransform = transform.Find("Dropdown List").transform.Find("Viewport").transform.Find("Content")
                    .transform.Find("Item " + i + ": " + options[i].text);
                itemsToTrack.Add(itemTransform.gameObject);
            }
            while (focused)
            {
                bool itemIsSelected = false;
                foreach (GameObject item in itemsToTrack)
                {
                    if (EventSystem.current.currentSelectedGameObject == item)
                    {
                        itemIsSelected = true;
                    }
                }
                if (itemIsSelected == false)
                {
                    Hide();
                    focused = false;
                }
                yield return null;
            }
            yield break;
        }

        public bool IsThisElementInFocus()
        {
            return focused;
        }
    }
