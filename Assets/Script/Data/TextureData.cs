﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class TextureData : UpdatableObject {

    public Color[] BaseColours;
    [Range(0 , 1)]
    public float[] BaseStartHeights;

    float _savedMinHeight;
    float _savedMaxHeight;

    public void ApplyToMaterial( Material material ) {

        material.SetInt("baseColourCount" , BaseColours.Length);
        material.SetColorArray("baseColours" , BaseColours);
        material.SetFloatArray("baseStartHeights" , BaseStartHeights);

        UpdateMeshHeights(material , _savedMinHeight , _savedMaxHeight);
    }

    public void UpdateMeshHeights( Material material , float minHeight , float maxHeight ) {
        _savedMinHeight = minHeight;
        _savedMaxHeight = maxHeight;

        material.SetFloat("minHeight" , minHeight);
        material.SetFloat("maxHeight" , maxHeight);
    }
}