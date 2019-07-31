namespace TaiChiVR.Welcome
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class ImageWallManager : MonoBehaviour
    {
        [SerializeField] Image imageTemplate = null;
        [SerializeField] Sprite[] spriteList = null;
        [SerializeField] float totalMoveDistance = 210f; // total moving distance for each sprite 
        [SerializeField] float moveSpeed = 12f;

        private Image[] imageList; // for change alpha of image
        private RectTransform[] rectList; // for move sprite position
        private int totalSpriteNum;
        private float distance; // distance between two center of sprite
        private float halfDistance;

        void Start()
        {
            totalSpriteNum = spriteList.Length;
            distance = totalMoveDistance / totalSpriteNum;
            halfDistance = distance / 2;
            ListInit();
        }

        private void ListInit()
        {
            imageList = new Image[totalSpriteNum];
            rectList = new RectTransform[totalSpriteNum];

            for (int i = 0; i < totalSpriteNum; i++)
            {
                int tmp = i; //prevent multithreading mess up index

                Image image = Instantiate(imageTemplate, this.transform);
                image.sprite = spriteList[tmp];

                RectTransform rect = image.GetComponent<RectTransform>();
                rect.localPosition += new Vector3(distance * tmp + halfDistance, 0, 0); //assign right X value

                image.gameObject.SetActive(true);

                imageList[tmp] = image;
                rectList[tmp] = rect;
            }
        }

        void Update()
        {
            for (int i = 0; i < totalSpriteNum; i++)
            {
                int tmp = i;
                float nextX = NextX(rectList[tmp].localPosition.x);
                MoveIt(nextX, tmp);
                FadeIt(nextX, tmp);
            }
        }

        private float NextX(float currentX)
        {
            float x = currentX + moveSpeed * Time.deltaTime;

            if (-halfDistance < x && x <= totalMoveDistance - halfDistance)
            {
                return x;
            }
            else
            {
                return -halfDistance;
            }
        }

        private void MoveIt(float x, int index)
        {
            rectList[index].localPosition = new Vector3(x, 0, 0);
        }

        private void FadeIt(float x, int index)
        {
            //img.color = new Color(1f, 1f, 1f, ((Mathf.Pow(((2 * (Mathf.Abs((x - totalMoveDistance) / totalMoveDistance))) - 1), fadeSpeed * Time.deltaTime)) + 1));

            float alpha = 0;

            if (-halfDistance <= x && x <= halfDistance)
            {
                alpha = 0;
            }
            else if (halfDistance < x && x <= distance)
            {
                alpha = SinIt(x, halfDistance, distance);//
            }
            else if (distance < x && x <= totalMoveDistance - distance)
            {
                alpha = 1;
            }
            else
            {
                alpha = SinIt(x, totalMoveDistance - distance, totalMoveDistance - halfDistance);//
            }

            imageList[index].color = new Color(1, 1, 1, alpha);
        }

        private float SinIt(float x, float start, float end)
        {
            float nor = (x - start) / (end - start);
            float sinX = 0;
            if (x <= distance)
            {
                sinX = Mathf.Lerp(0, Mathf.PI / 2, nor);
            }
            else
            {
                sinX = Mathf.Lerp(Mathf.PI / 2, Mathf.PI, nor);
            }

            return Mathf.Sin(sinX);
        }
    }
}