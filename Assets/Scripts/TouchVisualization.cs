using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Visualization;

namespace Assets.Scripts
{
    public class TouchVisualization : MonoBehaviour
    {
        [SerializeField]
        Object _swipeProcessor;

        IPointsVisualizable Observant => _swipeProcessor as IPointsVisualizable;

        [SerializeField]
        LineRenderer line;

        [SerializeField]
        float lineWidth;

        [SerializeField]
        GameObject point;

        [SerializeField]
        bool drawPoint;

        [SerializeField]
        Camera cam;

        float depth => -cam.transform.position.z;

        void OnEnable()
        {
            Observant.LineUpdated += DrawOnScreen;
            Observant.PointUpdated += ShowPointOnWorld;
        }

        void OnDisable()
        {
            Observant.LineUpdated -= DrawOnScreen;
            Observant.PointUpdated -= ShowPointOnWorld;
        }

        private void DrawOnScreen(List<Vector2> points)
        {
            var pos = points.Select(point => cam.ScreenToWorldPoint(new Vector3(point.x, point.y, depth))).ToArray();
            line.positionCount = pos.Length;
            line.startWidth = lineWidth;
            line.endWidth = lineWidth;
            line.SetPositions(pos);
            point.SetActive(drawPoint);
        }

        private void ShowPointOnWorld(Vector2 point)
        {
            if(!drawPoint)
            {
                return;
            }

            if(this.point == null)
            {
                Debug.LogError("No pin object assigned.");
                return;
            }

            this.point.transform.position = cam.ScreenToWorldPoint(new Vector3(point.x, point.y, depth));
            this.point.SetActive(true);
        }
    }
}