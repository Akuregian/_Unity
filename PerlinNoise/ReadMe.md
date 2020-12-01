Setup Steps:

  1) Create an Empty GameObject, this will act as the MapGenerator. Attach the MapGenerator and MapDisplay scripts too this gameobject.
  2) Create a Plane. Add a Material that has the shader "Unlit/Texture" and attach it too the plane. 
        - Now add this plane into the slot titled Texture Render in the gameObject MapGenerator, inside the MapDisplay script inspector.
  3) Create an empty gameObject and add the components: Mesh Filter, Mesh Renderer and a Material. Attach these into the slots within the MapDisplay inspector in the mapGenerator gameObject.
