using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Modified interactor that has everything XRDirectIndicator but includes a highlighter
/// when selecting objects.
/// </summary>

namespace GW2021
{
    [DisallowMultipleComponent]
    [AddComponentMenu("XR/XR Direct Interactor")]
    public class ModifiedInteractor : XRDirectInteractor
    {
        [SerializeField] Material highlightMat;
        public void HighlightSelection()
        {
            Debug.Log("WORKSSSSSS");
        }
    }
}