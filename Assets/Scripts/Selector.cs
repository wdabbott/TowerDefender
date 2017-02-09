using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Selector<T> : MonoBehaviour
        where T : ISelectable
    {
        public string Tag;
        private GameObject selectedTarget;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                SelectTarget();
            }
        }

        private void SelectTarget()
        {
            var rayPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero);

            if (hit && hit.transform.gameObject.tag == Tag)
            {
                DelesectCurrent();
                hit.transform.gameObject.GetComponent<T>().SetSelected(true);
                selectedTarget = hit.transform.gameObject;
            }
            else
            {
                DelesectCurrent();
            }
        }

        private void DelesectCurrent()
        {
            if (selectedTarget)
            {
                selectedTarget.GetComponent<T>().SetSelected(false);
                selectedTarget = null;
            }
        }
    }
}
