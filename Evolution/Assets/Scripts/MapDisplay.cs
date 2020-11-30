﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MapDisplay class is used to draw Color or Textures to the 2D noise map
public class MapDisplay : MonoBehaviour
{
    public Renderer textureRender;
    public void DrawTexture(Texture2D texture) {
  
        textureRender.sharedMaterial.mainTexture = texture;
        textureRender.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }


}