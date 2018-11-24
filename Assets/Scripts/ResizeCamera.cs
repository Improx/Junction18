using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ResizeCamera : MonoBehaviour {

  [SerializeField]
  private Tilemap _map;

	//Assign this Camera in the Inspector
  public Camera m_OrthographicCamera;
  //These are the positions and dimensions of the Camera view in the Game view
  float m_ViewPositionX, m_ViewPositionY, m_ViewWidth, m_ViewHeight;

  void Start()
  {
      m_ViewPositionX = 0;
      m_ViewPositionY = 0;

      //This sets the Camera view rectangle to be smaller so you can compare the orthographic view of this Camera with the perspective view of the Main Camera
      m_ViewWidth = 1f;
      m_ViewHeight = 1f;

      //This enables the Camera (the one that is orthographic)
      m_OrthographicCamera.enabled = true;

      //If the Camera exists in the inspector, enable orthographic mode and change the size
      if (m_OrthographicCamera)
      {
          //This enables the orthographic mode
          m_OrthographicCamera.orthographic = true;
          //Set the size of the viewing volume you'd like the orthographic Camera to pick up (5)
          m_OrthographicCamera.orthographicSize = _map.size.y / 2f;//(Mathf.Pow(((tilemapY * 16f/9f)/tilemapY), 2f) * 1.1f);
          //Set the orthographic Camera Viewport size and position
          m_OrthographicCamera.rect = new Rect(m_ViewPositionX, m_ViewPositionY, m_ViewWidth, m_ViewHeight);
      }
  }
}
